using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class CameraWalk : MonoBehaviour
{

    [SerializeField]
    private Transform _dirTransform;

    [SerializeField]
    private Transform _dirTransform2;

    [SerializeField]
    private float _hoveringTime;
    private float _curTime = 0.0f;

    bool isHoveringEnd;

    // Use this for initialization
	void Start ()
    {
        isHoveringEnd = false;
        DragonAniManager.SwicthAnimation("Dragon_TakeOff");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isHoveringEnd)
        {
            if (MovementManager.Instance.CurrentNodeManager().IsMoveEnd)
            {
                Vector3 forward = (_dirTransform.position - this.transform.position).normalized;

                forward.y = 0.0f;
                DragonAniManager.SwicthAnimation("Dragon_Hovering");
                this.transform.rotation =
                    Quaternion.RotateTowards(
                        transform.rotation,
                        Quaternion.LookRotation(forward),
                        120.0f * Time.deltaTime);

                if (_hoveringTime <= _curTime)
                {
                    MovementManager.Instance.SetMovement(MovementType.DirectNode2);
                    isHoveringEnd = true;
                    return;
                }
                _curTime += Time.deltaTime;
            }
        }
        else
        {
            if (MovementManager.Instance.CurrentNodeManager().IsMoveEnd)
            {
                Vector3 forward = (_dirTransform2.position - this.transform.position).normalized;

                forward.x = 0.0f;
                forward.y = 0.0f;

                transform.rotation =
                    Quaternion.RotateTowards(
                        transform.rotation,
                        Quaternion.LookRotation(forward),
                        360.0f * Time.deltaTime);

                DragonManager.Instance.DragonRigidBody.useGravity = true;
                DragonAniManager.SwicthAnimation("Dragon_Landing");
            }
        }
	}
}
