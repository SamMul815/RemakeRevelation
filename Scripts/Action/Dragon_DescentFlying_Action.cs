using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DescentFlying_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        BlackBoard.Instance.IsGround = false;
        DragonManager.Instance.AttackOff();
        DragonManager.Instance.Stat.DescentSaveHP = DragonManager.Instance.Stat.HP;
        MovementManager.Instance.SetMovement(MovementType.Descent);
        DragonAniManager.SwicthAnimation("Dragon_DescentFlying");

    }

    public override bool Run()
    {
        if (MovementManager.Instance.GetNodeManager().IsMoveEnd)
        {
            BlackBoard.Instance.IsFlying = true;
            BlackBoard.Instance.IsDescentAttack = true;
            DragonManager.IsAction = false;
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
