using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_HowlingAttack_Decorator : DecoratorTask
{
    float curCoolingTime = 0.0f;
    float coolingTime = 0.0f;

    float redZoneDistance = 0.0f;
    float distance = 0.0f;
    bool isHowling_Attack;

    public override void Init()
    {
        base.Init();
        isHowling_Attack = false;
        distance = _blackBoard.HowlingDistance;
        coolingTime = _clock.HowlingCoolingTime;
        redZoneDistance = _blackBoard.RedZoneDistance;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        curCoolingTime = _clock.CurHowlingCoolingTime;

        isHowling_Attack = UtilityManager.DistanceCalc(DragonTransform, PlayerTransform, distance) &&
                                !(UtilityManager.DistanceCalc(DragonTransform, PlayerTransform, redZoneDistance));

        if (((curCoolingTime > coolingTime && isHowling_Attack) && !_manager.IsAction) || _manager.IsAction)
        {
            if (_childAction)
            {
                if (!_childAction.IsRunning)
                {
                    if (!_manager.IsAction)
                        OnStart();
                    else if (_manager.IsAction)
                        return true;
                    else if (!_childAction.IsRunning)
                        OnStart();
                }
                if(_childAction.IsRunning || _childAction.IsEnd)
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
                if (NodeState != TASKSTATE.RUNNING)
                    OnStart();
            }
            return ChildNode.Run();

        }

        else if (NodeState == TASKSTATE.RUNNING || 
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
