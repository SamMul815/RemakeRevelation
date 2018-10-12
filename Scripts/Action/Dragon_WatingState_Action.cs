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
        _clock.CurWaitingCoolingTime -= Time.deltaTime;

        if (_clock.CurWaitingCoolingTime <= 0.0f)
            _manager.IsAction = false;
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _blackBoard.IsWatingState = false;
        _clock.CurWaitingCoolingTime = _clock.WaitingCoolingTime;
    }


}
