using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Landing_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float LandingDistance = _blackBoard.LandingDistance;

        bool IsLanding = _blackBoard.IsLanding;
        bool IsFlying = _blackBoard.IsFlying;
        bool IsGround = _blackBoard.IsGround;


        if ((IsLanding && !IsGround && IsFlying && !_manager.IsAction) || _manager.IsAction)
        {
            if (_childAction)
            {
                if (!_childAction.IsRunning)
                {
                    if (!_manager.IsAction)
                        OnStart();
                    else if ((_manager.IsAction)) /*&& !IsLanding ))*/
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

                    return _childAction.Run();
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
