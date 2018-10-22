using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RushAttack_Decorator : DecoratorTask
{
    float curCoolingTime;
    float coolingTime;

    float redZoneDistance = 0.0f;
    float distance;
    bool isRush_Attack;

    public override void Init()
    {
        base.Init();
        curCoolingTime = 0.0f;
        coolingTime = _clock.DashCoolingTime;
        redZoneDistance = _blackBoard.RedZoneDistance;
        distance = _blackBoard.RushDistance;
        isRush_Attack = false;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        curCoolingTime = _clock.CurDashCoolingTime;

        isRush_Attack = UtilityManager.DistanceCalc(DragonTransform, PlayerTransform, distance) &&
                                !(UtilityManager.DistanceCalc(DragonTransform, PlayerTransform, redZoneDistance));

        if (((curCoolingTime >= coolingTime && isRush_Attack) && !_manager.IsAction) || _manager.IsAction)
        {
            if(_childAction)
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
                if(_childAction.IsRunning|| _childAction.IsEnd)
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
