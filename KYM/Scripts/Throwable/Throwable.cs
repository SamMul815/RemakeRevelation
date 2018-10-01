using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public PlayerHand.AttachmentFlags attachmentFlags = PlayerHand.AttachmentFlags.ParentToHand | PlayerHand.AttachmentFlags.DetachFromOtherHand;
    public string attachmentPoint;
    public float maxAngularVeloctiy = 50.0f;
    public float catchSpeedThreshold = 0.0f;
    public float maxThrowPower = 15.0f;
    public bool restoreOriginalParent = false;

    private VelocityEstimator velocityEstimator;
    private bool attached = false;
    private Vector3 attachPosition;
    private Quaternion attachRotation;

    //public UnityEvent onPickUp;
    //public UnityEvent onDetachFromHand;

    private void Awake()
    {
        velocityEstimator = GetComponent<VelocityEstimator>();
        GetComponent<Rigidbody>().maxAngularVelocity = maxAngularVeloctiy;
    }

    private void OnHandHoverBegin(PlayerHand hand)
    {
        if(!attached)
        {
            if(hand.GetTriggerButton())
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if(rb.velocity.magnitude >= catchSpeedThreshold)
                {
                    hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
                }
            }
        }
    }

    private void HandHoverUpdate(PlayerHand hand)
    {
        //Trigger got pressed
        if (hand.GetTriggerButton())
        {
            hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
        }
    }

    private void OnAttachedToHand(PlayerHand hand)
    {
        attached = true;
        //onPickUp.Invoke();
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
            velocity = velocity * (velocity.magnitude / 2 + 1.0f);


            //if(velocity.magnitude / 2.0f > maxThrowPower )
            //{
            //    velocity = velocity * maxThrowPower;
            //}
            //else
            //{
            //    velocity = velocity * (velocity.magnitude / 2.0f + 0.5f);
            //}
            Debug.Log(velocity.ToString() + velocity.magnitude.ToString());
            angularVelocity = Player.instance.trackingOriginTransform.TransformVector(hand.controller.angularVelocity) / 2.0f;
            position = hand.transform.position;
        }

        Vector3 r = transform.TransformPoint(rb.centerOfMass) - position;
        rb.velocity = velocity + Vector3.Cross(angularVelocity, r);
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

}
