using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_MeteoFlying_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        BlackBoard.Instance.IsFlying = true;
        BlackBoard.Instance.IsGround = false;
        DragonManager.Instance.AttackOff();
        DragonManager.Instance.Stat.MeteoSaveHP = DragonManager.Instance.Stat.HP;
        DragonAniManager.SwicthAnimation("Dragon_MeteoTakeOff");

    }

    public override bool Run()
    {

        if (MovementManager.Instance.GetNodeManager().IsMoveEnd)
        {
            BlackBoard.Instance.IsMeteoAttack = true;
            DragonManager.IsAction = false;
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        BlackBoard.Instance.IsFlying = true;
    }
}
