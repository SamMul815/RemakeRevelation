using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_MeteoAttack_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Dragon_MeteoAttack");
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
