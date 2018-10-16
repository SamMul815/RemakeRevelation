using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_BreathAttack_Decorator : DecoratorTask
{
    float breathAttackDistance = 0.0f;

    float redZoneDistance = 0.0f;
    float curCoolingTime = 0.0f;
    float coolingTime = 0.0f;

    bool isBreathAttack = false;

    public override void Init()
    {
        base.Init();
        breathAttackDistance = _blackBoard.BreathAttackDistance;
        redZoneDistance = _blackBoard.RedZoneDistance;
        coolingTime = _clock.BreathCoolingTime;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        curCoolingTime = _clock.CurBreathCoolingTime;

        isBreathAttack = UtilityManager.DistanceCalc(Dragon, Player, breathAttackDistance) &&
                            !(UtilityManager.DistanceCalc(Dragon, Player, redZoneDistance));

        if (((curCoolingTime >= coolingTime && isBreathAttack) && !_manager.IsAction) 
            || _manager.IsAction)
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
