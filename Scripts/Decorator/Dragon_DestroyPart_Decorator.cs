﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DestroyPart_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        bool IsDestroyPart = _blackBoard.IsDestroyPart;

        if ((IsDestroyPart))
        {

            if (_childAction)
            {
                if (!_childAction.IsRunning)
                {
                    if (!_manager.IsAction || !_childAction.IsRunning)
                        OnStart();
                    else if (_manager.IsAction)
                        return true;
                    else if (!_childAction.IsRunning)
                        OnStart();
                }
                if (_childAction.IsRunning && !_childAction.IsEnd)
                {
                    if (!_manager.IsAction)
                        OnStart();
                    else if (NodeState == TASKSTATE.FAULURE)
                        OnStart();

                    return ChildNode.Run();
                }
            }
            else
            {
                if (NodeState == TASKSTATE.RUNNING)
                    OnStart();
            }
            return ChildNode.Run();
        }
        else if(NodeState == TASKSTATE.RUNNING || 
            ChildNode.NodeState == TASKSTATE.RUNNING)
        {
            OnEnd();
        }
        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
