using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Walk_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Walk");
    }

    public override bool Run()
    {

        Vector3 DragonPos = Dragon.position;
        Vector3 PlayerPos = Player.position;

        DragonPos.y = 0.0f;
        PlayerPos.y = 0.0f;

        Vector3 forward = (PlayerPos - DragonPos).normalized;

        if (Vector3.Dot(Dragon.forward, forward) < 0.99f)
        {
            Dragon.rotation = Quaternion.Slerp(
                Dragon.rotation,
                Quaternion.LookRotation(forward),
                0.05f);
        }

        float WalkSpeed = _manager.Stat.WalkSpeed;

        Dragon.position = Vector3.MoveTowards(
            Dragon.position,
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
