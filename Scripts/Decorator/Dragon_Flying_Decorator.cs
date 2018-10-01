using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Flying_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        bool IsFlying = BlackBoard.Instance.IsFlying;

        if (IsFlying)
        {
            ActionTask childAcion = ChildNode.GetComponent<ActionTask>();

            if (childAcion)
            {
                if(!childAcion.IsRunning)
                {
                    if (!DragonManager.IsAction)
                        OnStart();
                    else if (DragonManager.IsAction)
                        return true;
                    else if (!childAcion.IsRunning)
                        OnStart();
                }
                if (childAcion.IsRunning && !childAcion.IsEnd)
                {
                    if (!DragonManager.IsAction)
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
