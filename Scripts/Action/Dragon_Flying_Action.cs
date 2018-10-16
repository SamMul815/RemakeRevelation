using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Flying_Action : ActionTask
{
    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Dragon_Flying");
    }

    public override bool Run()
    {
        Debug.Log("test");
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }


}
