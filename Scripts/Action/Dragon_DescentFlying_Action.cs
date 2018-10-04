using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DescentFlying_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        DragonManager.Instance.AttackOff();
        DragonManager.Instance.Stat.DescentSaveHP = DragonManager.Instance.Stat.HP;
        MovementManager.Instance.SetMovement(MovementType.Descent);
        DragonAniManager.SwicthAnimation("Dragon_DescentFlying");

    }

    public override bool Run()
    {
        if (MovementManager.Instance.GetNodeManager().IsMoveEnd)
        {
            DragonManager.IsAction = false;
            BlackBoard.Instance.IsLanding = true;
            //BlackBoard.Instance.IsFlying = true;
            //BlackBoard.Instance.IsDescentAttack = true;
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
