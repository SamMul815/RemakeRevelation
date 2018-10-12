using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_TailAttack_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Dragon_Tail");
        _clock.CurPawCoolingTime = 0.0f;
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
