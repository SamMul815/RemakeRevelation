using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    //손 타입 2개
    public enum HandType
    {
        Left,
        Right      
    };

    [Flags]
    public enum AttachmentFlags
    {
        SnapOnAttach = 1 << 0,
        // 객체가 손에있는 지정된 부착 점의 위치에 스냅되어야합니
        DetachOthers = 1 << 1,
        //이 손에 붙어있는 다른 오브젝트는 분리됩니다.
        DetachFromOtherHand = 1 << 2,
        //이 객체는 다른 손에서 분리됩니다.
        ParentToHand = 1 << 3,
        // 객체가 부모 객체가됩니다.
    };

    public const AttachmentFlags defaultAttachmentFlags = AttachmentFlags.ParentToHand |
                                                          AttachmentFlags.DetachOthers |
                                                          AttachmentFlags.DetachFromOtherHand |
                                                          AttachmentFlags.SnapOnAttach;

    //반대쪽 손
    public PlayerHand otherHand;

    //왼손 오른손 구분
    public HandType handType;

    public HandType GetHandType()
    {
        return handType;
    }

    //잡을수 기준점
    public Transform hoverSphereTransform
;
    public float hoverSphereRadius = 0.05f;
    public LayerMask hoverLayerMask = -1;
    public float hoverUpdateInterval = 0.1f;

    public SteamVR_Controller.Device controller;

    //초기 ViveControllerPrefab 그냥 기존꺼 가져다 사용하자
    public GameObject controllerPrefab;
    private GameObject controllerObject = null;

    //손에 잡히는 오브젝트 목록
    public struct AttachedObject
    {
        public GameObject attachedObject;
        public GameObject originalParent;
        public bool isParentedToHand;
    }

    private List<AttachedObject> attachedObjects = new List<AttachedObject>();

    public ReadOnlyCollection<AttachedObject> AttachedObjects
    {
        get { return attachedObjects.AsReadOnly(); }
    }

    public bool hoverLocked { get; private set; }

    private int prevOverlappingColliders = 0;
    private const int ColliderArraySize = 16;
    private Collider[] overlappingColliders;

    private Player playerInstance;

    private GameObject applicationLostFocusObject;

    SteamVR_Events.Action inputFocusAction;

    private Interactable _hoveringInteractable;

    public Interactable hoveringInteractable
    {
        get { return _hoveringInteractable; }
        set
        {
            if (_hoveringInteractable != value)
            {
                if (_hoveringInteractable != null)
                {
                    _hoveringInteractable.SendMessage("OnHandHoverEnd", this, SendMessageOptions.DontRequireReceiver);

                    //Note: The _hoveringInteractable can change after sending the OnHandHoverEnd message so we need to check it again before broadcasting this message
                    if (_hoveringInteractable != null)
                    {
                        this.BroadcastMessage("OnParentHandHoverEnd", _hoveringInteractable, SendMessageOptions.DontRequireReceiver); // let objects attached to the hand know that a hover has ended
                    }
                }
                _hoveringInteractable = value;

                if (_hoveringInteractable != null)
                {
                    _hoveringInteractable.SendMessage("OnHandHoverBegin", this, SendMessageOptions.DontRequireReceiver);

                    //Note: The _hoveringInteractable can change after sending the OnHandHoverBegin message so we need to check it again before broadcasting this message
                    if (_hoveringInteractable != null)
                    {
                        this.BroadcastMessage("OnParentHandHoverBegin", _hoveringInteractable, SendMessageOptions.DontRequireReceiver); // let objects attached to the hand know that a hover has begun
                    }
                }
            }
        }
    }

    public GameObject currentAttachedObject
    {
        get
        {
            CleanUpAttachedObjectStack();
            if(attachedObjects.Count > 0)
            {
                return attachedObjects[attachedObjects.Count - 1].attachedObject;
            }
            return null;
        }
    }

    public Transform GetAttachmentTransform(string attachmentPoint = "")
    {
        Transform attachmentTransform = null;

        if (!string.IsNullOrEmpty(attachmentPoint))
        {
            attachmentTransform = transform.Find(attachmentPoint);
        }

        if (!attachmentTransform)
        {
            attachmentTransform = this.transform;
        }

        return attachmentTransform;
    }

    private void Awake()
    {
        inputFocusAction = SteamVR_Events.InputFocusAction(OnInputFocus);

        //잡기 중심점이 없으면
        if(hoverSphereTransform == null)
        {
            //내가 중심점이 됨
            hoverSphereTransform = this.transform;
        }
        applicationLostFocusObject = new GameObject("_application_lost_focus");
        applicationLostFocusObject.transform.parent = transform;
        applicationLostFocusObject.SetActive(false);
    }

    IEnumerator Start()
    {
        // save off player instance
        playerInstance = Player.instance;
        if (!playerInstance)
        {
            Debug.LogError("No player instance found in Hand Start()");
        }
        // allocate array for colliders
        overlappingColliders = new Collider[ColliderArraySize];

        while(true)
        {
            yield return new WaitForSeconds(1.0f);

            if (controller != null)
                break;

            int leftIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
            int rightIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
            if (leftIndex == -1 || rightIndex == -1 || leftIndex == rightIndex)
            {
                continue;
            }

            int myIndex = (handType == HandType.Right) ? rightIndex : leftIndex;
            int otherIndex = (handType == HandType.Right) ? leftIndex : rightIndex;

            InitController(myIndex);
            if (otherHand)
            {
                otherHand.InitController(otherIndex);
            }

        }
        yield return null;
    }

    private void Update()
    {
        GameObject attached = currentAttachedObject;
        if(attached)
        {
            attached.SendMessage("HandAttachedUpdate", this, SendMessageOptions.DontRequireReceiver);
        }
        if (hoveringInteractable)
        {
            hoveringInteractable.SendMessage("HandHoverUpdate", this, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void LateUpdate()
    {
        if(controllerObject != null && attachedObjects.Count == 0)
        {
            AttachObject(controllerObject);
        }
    }

    private void FixedUpdate()
    {
        UpdateHandPoses();
    }

    //플레이어 손 컨트롤러 위치와 맞춰주기
    private void UpdateHandPoses()
    {
        if (controller != null)
        {
            SteamVR vr = SteamVR.instance;
            if (vr != null)
            {
                var pose = new Valve.VR.TrackedDevicePose_t();
                var gamePose = new Valve.VR.TrackedDevicePose_t();
                var err = vr.compositor.GetLastPoseForTrackedDeviceIndex(controller.index, ref pose, ref gamePose);
                if (err == Valve.VR.EVRCompositorError.None)
                {
                    var t = new SteamVR_Utils.RigidTransform(gamePose.mDeviceToAbsoluteTracking);
                    transform.localPosition = t.pos;
                    transform.localRotation = t.rot;
                }
            }
        }

    }

    //오브젝트 붙이기
    public void AttachObject(GameObject objectToAttach, AttachmentFlags flags = defaultAttachmentFlags, string attachmentPoint = "")
    {
        if(flags == 0)
        {
            flags = defaultAttachmentFlags;
        }
        CleanUpAttachedObjectStack();

        DetachObject(objectToAttach);

        if(((flags & AttachmentFlags.DetachFromOtherHand) == AttachmentFlags.DetachFromOtherHand) && otherHand)
        {
            otherHand.DetachObject(objectToAttach);
        }

        if((flags & AttachmentFlags.DetachOthers) == AttachmentFlags.DetachOthers)
        {
            while (attachedObjects.Count > 0)
            {
                DetachObject(attachedObjects[0].attachedObject);
            }
        }

        if(currentAttachedObject)
        {
            currentAttachedObject.SendMessage("OnHandFocusLost", this, SendMessageOptions.DontRequireReceiver);
        }

        AttachedObject attachedObject = new AttachedObject();
        attachedObject.attachedObject = objectToAttach;
        attachedObject.originalParent = objectToAttach.transform.parent != null ? objectToAttach.transform.parent.gameObject : null;

        if ((flags & AttachmentFlags.ParentToHand) == AttachmentFlags.ParentToHand)
        {
            //Parent the object to the hand
            objectToAttach.transform.parent = GetAttachmentTransform(attachmentPoint);
            attachedObject.isParentedToHand = true;
        }
        else
        {
            attachedObject.isParentedToHand = false;
        }
        attachedObjects.Add(attachedObject);

        if ((flags & AttachmentFlags.SnapOnAttach) == AttachmentFlags.SnapOnAttach)
        {
            objectToAttach.transform.localPosition = Vector3.zero;
            objectToAttach.transform.localRotation = Quaternion.identity;
        }

        objectToAttach.SendMessage("OnAttachedToHand", this, SendMessageOptions.DontRequireReceiver);

        UpdateHovering();

    }
    //오브젝트 떨구기
    public void DetachObject(GameObject objectToDetach, bool restoreOriginalParet = true)
    {
        int index = attachedObjects.FindIndex(l => l.attachedObject == objectToDetach);
        if(index != -1)
        {
            GameObject prevTopObject = currentAttachedObject;

            Transform parentTransform = null;
            if(attachedObjects[index].isParentedToHand)
            {
                if(restoreOriginalParet && (attachedObjects[index].originalParent != null))
                {
                    parentTransform = attachedObjects[index].originalParent.transform;
                }
                attachedObjects[index].attachedObject.transform.parent = parentTransform;
            }

            attachedObjects[index].attachedObject.SetActive(true);
            attachedObjects[index].attachedObject.SendMessage("OnDetachedFromHand", this, SendMessageOptions.DontRequireReceiver);
            attachedObjects.RemoveAt(index);

            GameObject newTopObject = currentAttachedObject;

            if(newTopObject != null && newTopObject != prevTopObject)
            {
                newTopObject.SetActive(true);
                newTopObject.SendMessage("OnHandFocusAcquired", this, SendMessageOptions.DontRequireReceiver);
            }
        }
        CleanUpAttachedObjectStack();
    }


    //주위 잡을 수 있는 오브젝트 있나 확인
    private void UpdateHovering()
    {
        if (hoverLocked)
            return;

        if (applicationLostFocusObject.activeSelf)
            return;

        float closetDistance = float.MaxValue;
        Interactable closestInteractable = null;
        // Pick the closest hovering
        float flHoverRadiusScale = playerInstance.transform.lossyScale.x;
        float flScaledSphereRadius = hoverSphereRadius * flHoverRadiusScale;

        // if we're close to the floor, increase the radius to make things easier to pick up
        float handDiff = Mathf.Abs(transform.position.y - playerInstance.trackingOriginTransform.position.y);
        float boxMult = Util.RemapNumberClamped(handDiff, 0.0f, 0.5f * flHoverRadiusScale, 5.0f, 1.0f) * flHoverRadiusScale;
        
        // null out old vals
        for (int i = 0; i < overlappingColliders.Length; ++i)
        {
            overlappingColliders[i] = null;
        }

        Physics.OverlapBoxNonAlloc(
            hoverSphereTransform.position - new Vector3(0, flScaledSphereRadius * boxMult - flScaledSphereRadius, 0),
            new Vector3(flScaledSphereRadius, flScaledSphereRadius * boxMult * 2.0f, flScaledSphereRadius),
            overlappingColliders,
            Quaternion.identity,
            hoverLayerMask.value
        );

        int iActualColliderCount = 0;

        foreach (Collider collider in overlappingColliders)
        {
            if (collider == null)
                continue;

            Interactable contacting = collider.GetComponentInParent<Interactable>();

            if (contacting == null)
                continue;


            if (attachedObjects.FindIndex(l => l.attachedObject == contacting.gameObject) != -1)
                continue;

            float distance = Vector3.Distance(contacting.transform.position, hoverSphereTransform.position);

            if(distance < closetDistance)
            {
                closetDistance = distance;
                closestInteractable = contacting;
            }
            iActualColliderCount++;
        }

        hoveringInteractable = closestInteractable;

        if (iActualColliderCount > 0 && iActualColliderCount != prevOverlappingColliders)
        {
            prevOverlappingColliders = iActualColliderCount;
        }

    }

    //생성됬을때 초기화
    //UpdateHovering 양손이 켜졌을경우 동시에 확인하는게 아닌 번갈아가면서 체크할 수 있게 함
    private void OnEnable()
    {
        inputFocusAction.enabled = true;
        // Stagger updates between hands
        float hoverUpdateBegin = ((otherHand != null) && (otherHand.GetInstanceID() < GetInstanceID())) ? (0.5f * hoverUpdateInterval) : (0.0f);
        InvokeRepeating("UpdateHovering", hoverUpdateBegin, hoverUpdateInterval);
    }

    private void OnDisable()
    {
        inputFocusAction.enabled = false;
        CancelInvoke();
    }

    private void OnInputFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            DetachObject(applicationLostFocusObject, true);
            applicationLostFocusObject.SetActive(false);
            UpdateHandPoses();
            UpdateHovering();
            BroadcastMessage("OnParentHandInputFocusAcquired", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            applicationLostFocusObject.SetActive(true);
            AttachObject(applicationLostFocusObject, AttachmentFlags.ParentToHand);
            BroadcastMessage("OnParentHandInputFocusLost", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void HoverLock(Interactable interactable)
    {
        hoverLocked = true;
        hoveringInteractable = interactable;
    }

    public void HoverUnlock(Interactable interactable)
    {
        if (hoveringInteractable == interactable)
        {
            hoverLocked = false;
        }
    }

    //ControllerValue
    //컨트롤러값 관련된 함수들
    public Vector3 GetTrackedObjectVelocity()
    {
        if(controller != null)
        {
            return transform.parent.TransformVector(controller.velocity);
        }
        return Vector3.zero;
    }

    public Vector3 GetTrackedObjectAngularVelocity()
    {
        if(controller != null)
        {
            return transform.parent.TransformVector(controller.angularVelocity);
        }
        return Vector3.zero;
    }

    public bool GetTouchPadDown()
    {
        if(controller != null)
        {
            return controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad);
        }
        return false;
    }

    public bool GetTouchPadUp()
    {
        if (controller != null)
        {
            return controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad);
        }
        return false;
    }

    public bool GetTouchPad()
    {
        if (controller != null)
        {
            return controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad);
        }
        return false;
    }

    public bool GetTriggerButtonDown()
    {
        if (controller != null)
        {
            return controller.GetHairTriggerDown();
        }
        return false;
    }

    public bool GetTriggerButtonUp()
    {
        if (controller != null)
        {
            return controller.GetHairTriggerUp();
        }
        return false;
    }

    public bool GetTriggerButton()
    {
        if (controller != null)
        {
            return controller.GetHairTrigger();
        }
        return false;
    }

    public float GetTriggerAxis()
    {
        if(controller != null)
        {
            return controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
        }
        return 0.0f;
    }

    public bool GetGripButtonDown()
    {
        if(controller != null)
        {
            return controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip);
        }
        return false;
    }

    public bool GetGripButtonUp()
    {
        if(controller != null)
        {
            return controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip);
        }
        return false;
    }

    public bool GetGripButton()
    {
        if(controller != null)
        {
            return controller.GetPress(SteamVR_Controller.ButtonMask.Grip);
        }
        return false;
    }

    public void Vibration(float length, float strength)
    {
        StartCoroutine(LongVibration(length, strength));
    }

    private void CleanUpAttachedObjectStack()
    {
        attachedObjects.RemoveAll(l => l.attachedObject == null);
    }

    //초기화 관련
    private void InitController(int index)
    {
        if(controller == null)
        {
            controller = SteamVR_Controller.Input(index);

            controllerObject = GameObject.Instantiate(controllerPrefab);
            controllerObject.SetActive(true);
            controllerObject.name = controllerPrefab.name + "_" + this.name;
            controllerObject.layer = gameObject.layer;
            controllerObject.tag = gameObject.tag;
            //AttachObject()
            controller.TriggerHapticPulse(800);
            controllerObject.transform.localScale = controllerPrefab.transform.localScale;
        }
    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, strength, length - i));
            yield return null;
        }
    }
}
