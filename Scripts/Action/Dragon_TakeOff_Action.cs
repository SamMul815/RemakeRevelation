using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_TakeOff_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        BlackBoard.Instance.IsTakeOff = true;
        BlackBoard.Instance.IsGround = false;
        DragonManager.Instance.AttackOff();
        DragonManager.Instance.Stat.TakeOffSaveHP = DragonManager.Instance.Stat.HP;
        DragonAniManager.SwicthAnimation("Dragon_TakeOff");

    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        BlackBoard.Instance.IsFlying = true;
        BlackBoard.Instance.IsTakeOff = false;
    }
}
