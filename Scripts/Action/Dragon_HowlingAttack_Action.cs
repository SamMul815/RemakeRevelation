using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_HowlingAttack_Action : ActionTask
{
    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Dragon_Howling");
        _clock.CurHowlingCoolingTime = 0.0f;
    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {
        
        base.OnEnd();

    }

}
