using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Walk_Action : ActionTask
{
    Vector3 DragonPos;
    Vector3 PlayerPos;

    public override void Init()
    {
        base.Init();
    }

    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Walk");
    }

    public override bool Run()
    {

        DragonPos = DragonTransform.position;
        PlayerPos = PlayerTransform.position;

        DragonPos.y = 0.0f;
        PlayerPos.y = 0.0f;

        Vector3 forward = (PlayerPos - DragonPos).normalized;
        
        if (Vector3.Dot(DragonTransform.forward, forward) < 0.99f)
        {
            DragonTransform.rotation = Quaternion.Lerp(
                DragonTransform.rotation,
                Quaternion.LookRotation(forward, Vector3.up),
                CurTurnTime / MaxTurnTime);

            CurTurnTime += Time.deltaTime;
        }

        float WalkSpeed = _manager.Stat.WalkSpeed;

        DragonTransform.position = Vector3.MoveTowards(
            DragonTransform.position,
            PlayerPos,
            WalkSpeed * Time.deltaTime
            );
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
