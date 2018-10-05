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
        bool IsAction = DragonManager.IsAction;

        if ((IsThirdPhaseHP && !IsAction) || IsAction)
        {
            ActionTask childAction = ChildNode.GetComponent<ActionTask>();

            if (childAction)
            {
                if (!childAction.IsRunning)
                {
                    if (!DragonManager.IsAction)
                        OnStart();
                    else if (DragonManager.IsAction)
                        return true;
                    else if (!childAction.IsRunning)
                        OnStart();
                }
                if (childAction.IsRunning && !childAction.IsEnd)
                {
                    if (!DragonManager.IsAction)
                        OnStart();
                    else if (NodeState == TASKSTATE.FAULURE)
                        OnStart();

                    return ChildNode.Run();
                }
            }
            else if (NodeState != TASKSTATE.RUNNING)
            {
                if (!DragonManager.IsAction)
                    OnStart();
                else if (DragonManager.IsAction)
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
