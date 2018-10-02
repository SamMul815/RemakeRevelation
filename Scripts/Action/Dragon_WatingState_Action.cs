using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_WatingState_Action : ActionTask
{
    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Idle");
    }

    public override bool Run()
    {
        Clock.Instance.CurWaitingCoolingTime -= Time.deltaTime;

        if (Clock.Instance.CurWaitingCoolingTime <= 0.0f)
            DragonManager.IsAction = false;
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        BlackBoard.Instance.IsWatingState = false;
        Clock.Instance.CurWaitingCoolingTime = Clock.Instance.WaitingCoolingTime;
    }


}
