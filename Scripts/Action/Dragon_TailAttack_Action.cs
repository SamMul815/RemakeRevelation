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
    }

    public override bool Run()
    {
        Debug.Log("Tail");
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
