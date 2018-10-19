using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_PawsAttack_Decorator : DecoratorTask
{
    bool isPaw_Attack;
    float distance;
    float curCoolingTime;
    float coolingTime;
    float redZoneDistance = 0.0f;

    public override void Init()
    {
        base.Init();
        redZoneDistance = _blackBoard.RedZoneDistance;
        curCoolingTime = _clock.CurPawCoolingTime;
        coolingTime = _clock.PawCoolingTime;
        distance = _blackBoard.PawAttackDistance;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {

        isPaw_Attack = UtilityManager.DistanceCalc(Dragon, Player, distance) && 
            !(UtilityManager.DistanceCalc(Dragon, Player, redZoneDistance));
       
        if (((curCoolingTime >= coolingTime && isPaw_Attack)  && !_manager.IsAction) || _manager.IsAction)
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
