using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotateYDragon : BulletBaseDragon
{

    public float angleSpeed;
    private float radius;
    private float moveTime;
    private float currentRadius = 0.0f;
    //private float lerpTime = 1.0f;
    private Vector3 basePosition;
    protected override void OnCollisionEvent()
    {
        //base.OnCollisionEvent();
    }

    public override void Init()
    {
        base.Init();
        moveTime = 0.0f;
        currentRadius = 0.0f;
        radius = 0.0f;
    }

    protected override void Move()
    {
       // basePosition = UtilityManager.Instance.DragonPosition() + Vector3.up;
        float forwardValue = Mathf.Cos(moveTime * angleSpeed * Mathf.Deg2Rad);
        float upValue = 0;
        float rightValue = Mathf.Sin(moveTime * angleSpeed * Mathf.Deg2Rad);

        moveTime += Time.deltaTime;
        currentRadius = radius;
        radius += moveSpeed * Time.deltaTime;
        this.transform.position = basePosition +
                        (forwardValue * transform.forward * currentRadius) +
                        //(upValue * transform.up * currentRadius) +
                        (rightValue * transform.right * currentRadius);
    }

    public void BasePosition(Vector3 pos)
    {
        basePosition = pos;
    }


}
