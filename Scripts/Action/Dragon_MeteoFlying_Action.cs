using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_MeteoFlying_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        _blackBoard.IsGround = false;
        _manager.AttackOff();
        _manager.Stat.MeteoSaveHP = _manager.Stat.HP;
        DragonAniManager.SwicthAnimation("Dragon_MeteoTakeOff");

    }

    public override bool Run()
    {

        if (_manager.FlyingOn)
        {
            if (_movement.CurrentNodeManager().IsMoveEnd)
            {
                _manager.IsAction = false;
                _blackBoard.IsMeteoAttack = true;
            }
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _blackBoard.IsFlying = true;
    }
}
