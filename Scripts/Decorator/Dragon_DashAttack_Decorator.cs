using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DashAttack_Decorator : DecoratorTask
{
    float curCoolingTime = 0.0f;
    float coolingTime = 0.0f;

    float redZoneDistance = 0.0f;
    float distance = 0.0f;
    bool isRush_Attack;

    public override void Init()
    {
        base.Init();
        isRush_Attack = false;
        distance = _blackBoard.DashDistance;
        coolingTime = _clock.DashCoolingTime;
        redZoneDistance = _blackBoard.RedZoneDistance;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        curCoolingTime = _clock.CurDashCoolingTime;

        isRush_Attack = UtilityManager.DistanceCalc(Dragon, Player, distance) &&
                                !(UtilityManager.DistanceCalc(Dragon, Player, redZoneDistance));

        if (((curCoolingTime >= coolingTime && isRush_Attack) && !_manager.IsAction) || _manager.IsAction)
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
