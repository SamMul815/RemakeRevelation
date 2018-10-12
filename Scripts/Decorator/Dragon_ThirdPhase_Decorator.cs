using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_ThirdPhase_Decorator : DecoratorTask
{

    public override void OnStart()
    {
        base.OnStart();
        float MaxHP = DragonManager.Instance.Stat.MaxHP;
        float AirSpearHPPercent = DragonManager.Instance.Stat.ThirdPhaseAirSpearHPPrecent;
        DragonManager.Instance.Stat.AirSpearHP = MaxHP * AirSpearHPPercent;
    }

    public override bool Run()
    {
        float ThirdPhaseHP = DragonManager.Instance.Stat.ThirdPhaseHP;
        float HP = DragonManager.Instance.Stat.HP;

        bool IsThirdPhaseHP = (HP <= ThirdPhaseHP);

        if ((IsThirdPhaseHP && !_manager.IsAction) || _manager.IsAction)
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
            else if (NodeState != TASKSTATE.RUNNING)
            {
                if (!_manager.IsAction)
                    OnStart();
                else if (_manager.IsAction)
                    return true;
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
