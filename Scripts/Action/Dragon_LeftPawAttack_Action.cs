using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_LeftPawAttack_Action : ActionTask
{
    public override void OnStart()
    {
        base.OnStart();
        //DragonAniManager.SwicthAnimation("Dragon_RightPow");
        DragonAniManager.SwicthAnimation("Dragon_LeftPaw");
        Clock.Instance.CurPawCoolingTime = 0.0f;
        //초기화
    }

    public override bool Run()
    {
        Transform Dragon = DragonManager.Instance.transform;
        Transform Player = DragonManager.Player;

        Vector3 DragonPos = Dragon.position;
        Vector3 PlayerPos = Player.position;

        DragonPos.y = 0.0f;
        PlayerPos.y = 0.0f;

        Vector3 forward = (PlayerPos - DragonPos).normalized;

        //if (!DragonManager.IsTurn)
        //{
        if (Vector3.Dot(Dragon.forward, forward) < 0.99f)
        {
            //DragonAniManager.SwicthAnimation("LeftTrun");
            Dragon.rotation = Quaternion.Slerp(
                Dragon.rotation,
                Quaternion.LookRotation(forward),
                0.05f);
            return false;
        }
        DragonManager.IsTurn = true;
        //}
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
