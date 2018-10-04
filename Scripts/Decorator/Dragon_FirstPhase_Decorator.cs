using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_FirstPhase_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float FirstPhaseHP = DragonManager.Instance.Stat.FirstPhaseHP;
        float HP = DragonManager.Instance.Stat.HP;

        bool IsFirstPhase = (HP <= FirstPhaseHP);
        bool IsAction = DragonManager.IsAction;

        if ((IsFirstPhase && !IsAction) || IsAction)
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

            Debug.Log("FirstPhase");
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
