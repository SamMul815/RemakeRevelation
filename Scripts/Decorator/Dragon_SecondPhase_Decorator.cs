using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_SecondPhase_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float SecondPhaseHP = DragonManager.Instance.Stat.SecondPhaseHP;
        float HP = DragonManager.Instance.Stat.HP;

        bool IsSecondPhase = (HP <= SecondPhaseHP);
        bool IsAction = DragonManager.IsAction;

        if ((IsSecondPhase && !IsAction) || IsAction)
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

            Debug.Log("SecondPhase");
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
