using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_MeteoHovering_Action : ActionTask
{
    float curTime = 0.0f;
    float maxTime = 3.0f;

    public override void Init()
    {
        base.Init();
        curTime = 0.0f;
        maxTime = 3.0f;
    }

    public override void OnStart()
    {
        base.OnStart();
        curTime = 0.0f;
        DragonAniManager.SwicthAnimation("Dragon_Hovering");
    }

    public override bool Run()
    {
        if (curTime < maxTime)
        {
            curTime += Time.deltaTime;
            return false;
        }
        _blackBoard.IsMeteoLoitering = true;
        _blackBoard.IsMeteoHovering = false;
        _manager.IsAction = false;
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
