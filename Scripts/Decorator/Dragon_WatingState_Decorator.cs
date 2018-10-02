using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_WatingState_Decorator : DecoratorTask
{

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float curCoolingTime = Clock.Instance.CurWaitingCoolingTime;
        bool IsAction = DragonManager.IsAction;
        
        if ((Clock.Instance.CurWaitingCoolingTime > 0.0f && BlackBoard.Instance.IsWatingState && !IsAction) || IsAction)
        {
            ActionTask childAction = ChildNode.GetComponent<ActionTask>();

            if (childAction)
            {
                if(!childAction.IsRunning)
                {
                    if (!DragonManager.IsAction)
                        OnStart();
                    else if (DragonManager.IsAction)
                        return true;
                    else if (!childAction.IsRunning)
                        OnStart();
                }
                if(childAction.IsRunning && !childAction.IsEnd)
                {
                    if (!DragonManager.IsAction)
                        OnStart();
                    else if (NodeState == TASKSTATE.FAULURE)
                        OnStart();

                    return ChildNode.Run();
                }
            }
            else
            {
                if (NodeState != TASKSTATE.RUNNING)
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
