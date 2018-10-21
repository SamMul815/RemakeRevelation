﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_AirSpearFlying_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        _manager.AttackOff();
        _blackBoard.IsFlying = true;
        _manager.Stat.AirSpearSaveHP = _manager.Stat.HP;
        _blackBoard.LandingDistance = _manager.Stat.AirSpearLandingDistance;
        DragonAniManager.SwicthAnimation("Dragon_DescentTakeOff");

    }

    public override bool Run()
    {

        if (_manager.FlyingOn)
        {
            if (_movement.CurrentNodeManager().IsMoveEnd)
            {
                _blackBoard.IsLanding = true;
                _manager.IsAction = false;
            }
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
