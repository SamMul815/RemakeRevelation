using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_LeftPawAttack_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        Transform Dragon = DragonManager.Instance.transform;
        Transform Player = DragonManager.Player;

        Vector3 toTarget = (Player.position - Dragon.position).normalized;

        float Dot = Vector3.Dot(Dragon.forward, toTarget);

        if (Dot >= Mathf.Cos(Mathf.Deg2Rad * 180.0f * 0.5f))
        {
            Vector3 Cross = Vector3.Cross(Dragon.forward, toTarget);

            float Result = Vector3.Dot(Cross, Vector3.up);
            bool IsAction = DragonManager.IsAction;

            if ((Result < 0.0f && !IsAction) || IsAction)
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
                        else if (!childAction.IsRunning)
                            OnStart();
                        return ChildNode.Run();
                    }
                }
                else
                {
                    if (NodeState != TASKSTATE.RUNNING)
                        OnStart();
                    return ChildNode.Run();
                }
            }
            else if (NodeState == TASKSTATE.RUNNING ||
                ChildNode.NodeState == TASKSTATE.RUNNING
                )
            {
                OnEnd();
            }
        }
        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
