using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHowlingDragon : BulletBase
{
    public bool isHitPlayer = false;

    public override void Init()
    {
        prevPosition = transform.position;
        moveDir = transform.forward;
        MoveDistance = MaxMoveDistance;
    }

    protected override void OnCollisionEvent()
    {
        if (isHitPlayer) return;

        Collider _col = hitInfo.collider;
        if (_col.CompareTag("Player") || _col.CompareTag("Gun"))
        {
            Player.instance.playerStat.Hit(damage);
            isHitPlayer = true;
        }
    }
}
