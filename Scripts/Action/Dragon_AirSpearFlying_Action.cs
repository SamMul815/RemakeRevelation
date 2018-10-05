using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_AirSpearFlying_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        DragonManager.Instance.AttackOff();
        DragonManager.Instance.Stat.AirSpearSaveHP = DragonManager.Instance.Stat.HP;
        MovementManager.Instance.SetMovement(MovementType.AirSpear);
        DragonAniManager.SwicthAnimation("Dragon_DescentFlying");

    }

    public override bool Run()
    {

        if (DragonManager.FlyingOn)
        {
            if (MovementManager.Instance.CurrentNodeManager().IsMoveEnd)
            {
                DragonManager.IsAction = false;
                BlackBoard.Instance.IsLanding = true;
            }
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
