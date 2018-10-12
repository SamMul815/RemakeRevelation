using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DashAttack_Decorator : DecoratorTask
{

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float CurCoolingTime = _clock.CurDashCoolingTime;
        float CoolingTime = _clock.DashCoolingTime;
 
        float Distance = _blackBoard.DashDistance;

        bool IsRush_Attack = UtilityManager.DistanceCalc(Dragon, Player, Distance);

        if (((CurCoolingTime >= CoolingTime && IsRush_Attack) && !_manager.IsAction) || _manager.IsAction)
        {
            if (_childAction)
            {
                if(!_childAction.IsRunning)
                {
                    if (!_manager.IsAction)
                        OnStart();
                    else if (_manager.IsAction)
                        return true;
                    else if (!_childAction.IsRunning)
                        OnStart();
                }
                if(!_childAction.IsRunning || _childAction.IsEnd)
                {
                    if (!_manager.IsAction)
                        OnStart();
                    else if (!_childAction.IsRunning)
                        OnStart();

                    return ChildNode.Run();
                }

            }
            else
            {
                if(NodeState != TASKSTATE.RUNNING)
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
