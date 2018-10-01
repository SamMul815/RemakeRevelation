//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;



//public class PlayerVRController : MonoBehaviour
//{
//    [SerializeField]
//    private SteamVR_TrackedObject trackedObjectLeft;
//    [SerializeField]
//    private SteamVR_TrackedObject trackedObjectRight;
//    [SerializeField]
//    TeleportPointer pointer;

//    [SerializeField]
//    private Transform originTransform;
//    [SerializeField]
//    private Transform headTransform;

//    [SerializeField]
//    private Gun leftHandGun;
//    [SerializeField]
//    private Gun rightHandGun;

//    //PlayerVRState playerState;

//    [SerializeField]
//    public float teleportMovingTime = 0.4f;

//    //private bool isSlow = false;
//    //private float slowTime = 0.0f;
//    //[SerializeField]
//    //private float maxSlowTime = 5.0f;
//    //[SerializeField]
//    //[Range(0, 1)]
//    //private float slowValue = 0.2f;
//    //[SerializeField]
//    //private Slider slowSliderUI;
//    //[SerializeField]
//    //private Image slowGearUI;



//    SteamVR_Controller.Device LeftHand
//    {
//        get
//        {
//            return SteamVR_Controller.Input((int)trackedObjectLeft.index);
//        }
//    }
//    SteamVR_Controller.Device RightHand
//    {
//        get
//        {
//            return SteamVR_Controller.Input((int)trackedObjectRight.index);
//        }
//    }

//    private void Awake()
//    {

//    }

//    private void Start()
//    {
//        playerState = PlayerVRState.TeleportNone;
//    }

//    private void Update()
//    {
//        Teleport();
//        GunFire();
//        //Slow();
//    }

//    private void FixedUpdate()
//    {
//      // Move();
//    }

//    void Teleport()
//    {
//        if (playerState == PlayerVRState.TeleportNone && trackedObjectLeft.isActiveAndEnabled)
//        {
//            if (LeftHand.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
//            {
//                pointer.transform.parent = trackedObjectLeft.transform;
//                pointer.transform.localPosition = Vector3.zero;
//                pointer.transform.localRotation = Quaternion.identity;
//                pointer.transform.localScale = Vector3.one;
//                pointer.enabled = true;
//                playerState = PlayerVRState.TeleportSelect;
//                pointer.ForceupdateCurrentAngle();
//            }
//        }
//        else if (playerState == PlayerVRState.TeleportSelect)
//        {
//            if (LeftHand.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
//            {
//                if (pointer.CanTeleport)
//                {
//                    playerState = PlayerVRState.Teleporting;
//                    //FMODSoundManager.Instance.PlayTeleportsound(this.transform.position);
//                    StartCoroutine("CorTeleport", pointer.SelectedPoint);
//                }
//                else
//                {
//                    playerState = PlayerVRState.TeleportNone;
//                }
//                pointer.enabled = false;
//                pointer.transform.parent = null;
//                pointer.transform.position = Vector3.zero;
//                pointer.transform.rotation = Quaternion.identity;
//                pointer.transform.localScale = Vector3.one;
//            }
//        }
//        else //playerState == PlayerVRState.Teleporting
//        {

//        }
//    }
 
//    void GunFire()
//    {
//        float rightHandAxis = 0.0f;
//        float leftHandAxis = 0.0f;

//        if (trackedObjectRight.isActiveAndEnabled)
//            rightHandAxis = RightHand.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
//        if(trackedObjectLeft.isActiveAndEnabled)
//            leftHandAxis = LeftHand.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;

//        if (rightHandAxis >= 1.0f)
//        {
//            rightHandGun.Fire();
//        }
//        if(leftHandAxis >= 1.0f)
//        {
//            leftHandGun.Fire();
//        }
//    }

//    //void Slow()
//    //{
//    //    if (RightHand.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
//    //    {
//    //        if(slowTime <= 0)
//    //            isSlow = true;
//    //    }

//    //    if(isSlow)
//    //    {
//    //        if (slowTime >= maxSlowTime)
//    //            isSlow = false;
//    //        else
//    //        {
//    //            Time.timeScale = slowValue; 
//    //            slowTime += Time.unscaledDeltaTime;
//    //            if (slowTime > maxSlowTime) slowTime = maxSlowTime;
//    //        }
//    //    }
//    //    else
//    //    {
//    //        Time.timeScale = 1.0f;
//    //        if (slowTime > 0)
//    //            slowTime -= Time.unscaledDeltaTime / 6;
//    //        else
//    //            slowTime = 0;
//    //    }
//    //    float value = 1 - slowTime / maxSlowTime;

//    //    slowSliderUI.value = value;
//    //    slowGearUI.transform.localRotation = Quaternion.Euler(0, 0, value * -1440);

//    //}

//    //int ArrowValue(int divCount, Vector2 axis)
//    //{
//    //    Vector2 baseAxis = new Vector2(0, 1.0f);
//    //    float angle = Vector2.SignedAngle(axis,baseAxis);
//    //    if (angle < 0)
//    //        angle = 360.0f + angle;

 
//    //    float d = 360.0f / divCount;
//    //    angle += (d / 2);

//    //    int retType = (int)(angle / d);

//    //    if (retType >= divCount)
//    //        retType = 0;

//    //    return retType;
//    //}


//    IEnumerator CorTeleport(Vector3 teleportPosition)
//    {
//        Vector3 difference = originTransform.position - headTransform.position;
//        Vector3 startPos = originTransform.position;
//        difference.y = 0;
//        teleportPosition += difference;

//        for(float t = 0; t< teleportMovingTime;t += Time.unscaledDeltaTime)
//        {
//            originTransform.position = Vector3.Lerp(startPos, teleportPosition, t / teleportMovingTime);
//            yield return new WaitForEndOfFrame();
//        }
//        originTransform.position = teleportPosition;
//        playerState = PlayerVRState.TeleportNone;
//        yield return null;
//    }

//}
