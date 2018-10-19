﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RedZoneAttack_Action : ActionTask
{
    public override void Init()
    {
        base.Init();
    }

    public override void OnStart()
    {
        base.OnStart();

        _blackBoard.IsRedZoneIn = true;
        EffectManager.Instance.PoolParticleEffectOn("NearHowling",
            _manager.transform.position,
            _manager.transform.forward);
        DragonAniManager.SwicthAnimation("Dragon_NearHowling");

    }

    public override bool Run()
    {
        Debug.Log("at");
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
