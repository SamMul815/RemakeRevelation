using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DescentFlying_Decorator : DecoratorTask
{

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float MaxHP = DragonManager.Instance.Stat.MaxHP;
        float HP = DragonManager.Instance.Stat.HP;
        float SaveHP = DragonManager.Instance.Stat.DescentSaveHP;

        float DescentHP = DragonManager.Instance.Stat.DescentHP;

        bool IsDescent = DescentHP - (SaveHP - HP) <= 0.0f;
        bool IsAction = DragonManager.IsAction;

        bool IsFlying = BlackBoard.Instance.IsFlying;
        bool IsGround = BlackBoard.Instance.IsGround;

        if ((MaxHP > HP && IsDescent && IsGround && !IsFlying && !IsAction) || IsAction)
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
                    OnStart();
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
