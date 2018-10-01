using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_HowlingAttack_Decorator : DecoratorTask
{

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float CurCoolingTime = Clock.Instance.CurHowlingCoolingTime;
        float CoolingTime = Clock.Instance.HowlingCoolingTime;

        Transform Dragon = DragonManager.Instance.transform;
        Transform Player = DragonManager.Player;

        float Distance = BlackBoard.Instance.HowlingDistance;

        bool IsHowling_Attack = UtilityManager.DistanceCalc(Dragon, Player, Distance);
        bool IsAction = DragonManager.IsAction;

        if (((CurCoolingTime > CoolingTime && IsHowling_Attack) && !IsAction) || IsAction)
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
                if(childAction.IsRunning || childAction.IsEnd)
                {
                    if (!DragonManager.IsAction)
                        OnStart();
                    else if (!childAction.IsRunning)
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
