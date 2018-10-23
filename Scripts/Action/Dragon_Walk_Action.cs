using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Walk_Action : ActionTask
{
    float angle;

    public override void Init()
    {
        base.Init();
        angle = 0.0f;
    }

    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Walk");
        angle = 0.0f;
    }

    public override bool Run()
    {

        Vector3 DragonPos = DragonTransform.position;
        Vector3 PlayerPos = PlayerTransform.position;

        DragonPos.y = 0.0f;
        PlayerPos.y = 0.0f;

        Vector3 forward = (PlayerPos - DragonPos).normalized;

        angle = Vector3.Dot(DragonTransform.forward, forward);

        if (Vector3.Dot(DragonTransform.forward, forward) < 0.99f)
        {
            DragonTransform.rotation = Quaternion.Lerp(
                DragonTransform.rotation,
                Quaternion.LookRotation(forward),
                CurTurnTime / MaxTurnTime);

            CurTurnTime += Time.deltaTime;
        }

        float WalkSpeed = _manager.Stat.WalkSpeed;

        DragonTransform.position = Vector3.MoveTowards(
            DragonTransform.position,
            PlayerTransform.position,
            WalkSpeed * Time.deltaTime
            );
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
