using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Throwable : MonoBehaviour
{
    private PlayerHand.AttachmentFlags attachmentFlags = PlayerHand.AttachmentFlags.ParentToHand | PlayerHand.AttachmentFlags.DetachFromOtherHand | PlayerHand.AttachmentFlags.SnapOnAttach;
    public string attachmentPoint;
    public float maxAngularVeloctiy = 50.0f;
    public float catchSpeedThreshold = 0.0f;
    public float maxThrowPower = 15.0f;
    public bool restoreOriginalParent = false;
    public bool IsTutorial = false;
    public float gravityValue = 0.5f;

    private VelocityEstimator velocityEstimator;
    private bool attached = false;
    private Vector3 attachPosition;
    private Quaternion attachRotation;

    private bool IsThrow = false;
    private Rigidbody rb;

    public Material OnMaterial;
    public Material OffMaterial;

    public GameObject explosionParticle;
    public GameObject throwParticle;
    public float damage;
    private GameObject oldObject;


    private void Awake()
    {
        velocityEstimator = GetComponent<VelocityEstimator>();
        rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVeloctiy;
        throwParticle.SetActive(false);
        IsThrow = false;
    }

    public void OnMaterialChange()
    {
        this.GetComponent<MeshRenderer>().material = OnMaterial;
    }
    public void OffMaterialChange()
    {
        this.GetComponent<MeshRenderer>().material = OffMaterial;
    }
    public void Attatching(PlayerHand hand)
    {
        if (hand.GetTriggerButton() && !attached)
        {
            oldObject = hand.currentAttachedObject;
            if (oldObject != null)
                oldObject.SetActive(false);

            OffMaterialChange();
            if (IsTutorial)
            {
                GameObject tutorialObject = Instantiate(this.gameObject);
                tutorialObject.GetComponent<Throwable>().IsTutorial = false;
                hand.AttachObject(tutorialObject, attachmentFlags, attachmentPoint);
            }
            else
            {
                hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
            }
        }
    }

    //private void OnHandHoverBegin(PlayerHand hand)
    //{
    //    if (!attached)
    //    {
    //        if (!IsThrow)
    //        {
    //            this.GetComponent<MeshRenderer>().material = OnMaterial;
    //        }
    //    }
    //}

    //private void OnHandHoverEnd(PlayerHand hand)
    //{
    //    //if (!IsThrow)
    //    //{
    //    this.GetComponent<MeshRenderer>().material = OffMaterial;
    //    //}
    //    //copyMaterial.SetFloat("_Rimonoff", 0);
    //    //if (!attached)
    //    //{
    //    //    if (hand.GetTriggerButton())
    //    //    {
    //    //        Rigidbody rb = GetComponent<Rigidbody>();
    //    //        if (rb.velocity.magnitude >= catchSpeedThreshold)
    //    //        {
    //    //            hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
    //    //        }
    //    //    }
    //    //}
    //}

    //private void HandHoverUpdate(PlayerHand hand)
    //{
    //    //Trigger got pressed
    //    if (hand.GetTriggerButton() && !IsThrow)
    //    {
    //        oldObject = hand.currentAttachedObject;
    //        if (oldObject != null)
    //            oldObject.SetActive(false);
    //        if (IsTutorial)
    //        {
    //            GameObject tutorialObject = Instantiate(this.gameObject);
    //            tutorialObject.GetComponent<Throwable>().IsTutorial = false;
    //            hand.AttachObject(tutorialObject, attachmentFlags, attachmentPoint);
    //        }
    //        else
    //        {
    //            hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
    //        }
    //    }
    //}

    private void OnAttachedToHand(PlayerHand hand)
    {
        attached = true;
        hand.HoverLock(null);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;

        if(hand.controller == null)
        {
            velocityEstimator.BeginEstimatingVelocity();
        }
        attachPosition = transform.position;
        attachRotation = transform.rotation;

    }

    private void OnDetachedFromHand(PlayerHand hand)
    {
        attached = false;
        if (oldObject != null)
            oldObject.SetActive(true);
        //onDetachFromHand.Invoke();

        hand.HoverUnlock(null);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        Vector3 position = Vector3.zero;
        Vector3 velocity = Vector3.zero;
        Vector3 angularVelocity = Vector3.zero;
        if (hand.controller == null)
        {
            velocityEstimator.FinishEstimatingVelocity();
            velocity = velocityEstimator.GetVelocityEstimate();
            angularVelocity = velocityEstimator.GetAngularVelocityEstimate();
            position = velocityEstimator.transform.position;
        }
        else
        {
            velocity = Player.instance.trackingOriginTransform.TransformVector(hand.controller.velocity);
            velocity = velocity * maxThrowPower;

            //Debug.Log(velocity.ToString() + velocity.magnitude.ToString());
            angularVelocity = Player.instance.trackingOriginTransform.TransformVector(hand.controller.angularVelocity) / 2.0f;
            position = hand.transform.position;
        }

        Vector3 r = transform.TransformPoint(rb.centerOfMass) - position;
        rb.velocity = velocity + Vector3.Cross(angularVelocity * maxThrowPower, r);
        rb.angularVelocity = angularVelocity;

        // Make the object travel at the release velocity for the amount
        // of time it will take until the next fixed update, at which
        // point Unity physics will take over
        float timeUntilFixedUpdate = (Time.fixedDeltaTime + Time.fixedTime) - Time.time;
        transform.position += timeUntilFixedUpdate * velocity;
        float angle = Mathf.Rad2Deg * angularVelocity.magnitude;
        Vector3 axis = angularVelocity.normalized;
        transform.rotation *= Quaternion.AngleAxis(angle * timeUntilFixedUpdate, axis);
    }

    private void HandAttachedUpdate(PlayerHand hand)
    {
        if(!hand.GetTriggerButton())
        {
            StartCoroutine(LateDetach(hand));
        }
    }

    private IEnumerator LateDetach(PlayerHand hand)
    {
        yield return new WaitForEndOfFrame();

        hand.DetachObject(gameObject, restoreOriginalParent);
        IsThrow = true;
        throwParticle.SetActive(true);
    }

    //-------------------------------------------------
    private void OnHandFocusAcquired(PlayerHand hand)
    {
        gameObject.SetActive(true);
        velocityEstimator.BeginEstimatingVelocity();
    }

    //-------------------------------------------------
    private void OnHandFocusLost(PlayerHand hand)
    {
        gameObject.SetActive(false);
        velocityEstimator.FinishEstimatingVelocity();
    }

    private void Update()
    {
        if(rb.velocity.sqrMagnitude <= 0.1f)
        {
            IsThrow = false;
            throwParticle.SetActive(false);
        }
        else
        {
            rb.velocity -= Physics.gravity * gravityValue*Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (attached) return;
        if (!IsThrow) return;

        if(collision.gameObject.CompareTag("Dragon"))
        {
            UIManager.Instance.CreatePopupTextYellow(damage.ToString(),transform.position);
            DragonManager.Instance.OnDestroyPart(damage);
            FmodManager.Instance.PlaySoundOneShot(transform.position, "Stone");
        }
        else if(collision.gameObject.CompareTag("TutorialTarget"))
        {
            UIManager.Instance.CreatePopupTextYellow(damage.ToString(), transform.position);
            collision.gameObject.GetComponent<TutorialTarget>().Hit(damage);
            FmodManager.Instance.PlaySoundOneShot(transform.position, "Stone");
        }

        if(explosionParticle != null)
        {
            GameObject explosionObj;
            PoolManager.Instance.PopObject(explosionParticle,this.transform.position,out explosionObj);
        }
        
        
        this.gameObject.SetActive(false);
    }
}