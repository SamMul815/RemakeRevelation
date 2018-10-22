using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_BulletBreathAttack_Decorator : DecoratorTask
{
    float bullletBreathAttackDistance = 0.0f;
    float redZoneDistance = 0.0f;

    float curCoolingTime = 0.0f;
    float coolingTime = 0.0f;

    bool isBulletBreathAttck = false;

    public override void Init()
    {
        base.Init();
        bullletBreathAttackDistance = _blackBoard.BullletBreathAttackDistance;
        redZoneDistance = _blackBoard.RedZoneDistance;
        coolingTime = _clock.BulletBreathCoolingTime;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        curCoolingTime = _clock.CurBulletBreathCoolingTime;

        isBulletBreathAttck = UtilityManager.DistanceCalc(DragonTransform, PlayerTransform, bullletBreathAttackDistance) &&
                                !(UtilityManager.DistanceCalc(DragonTransform, PlayerTransform, redZoneDistance));

        if (((curCoolingTime >= coolingTime && isBulletBreathAttck) && !_manager.IsAction)
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
