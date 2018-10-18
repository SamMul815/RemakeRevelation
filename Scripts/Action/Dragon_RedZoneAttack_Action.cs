using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RedZoneAttack_Action : ActionTask
{
    Vector3 forward = Vector3.zero;


    public override void Init()
    {
        base.Init();
    }

    public override void OnStart()
    {
        base.OnStart();
        _blackBoard.IsRedZoneIn = true;
        DragonAniManager.SwicthAnimation("Dragon_Rush");
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
