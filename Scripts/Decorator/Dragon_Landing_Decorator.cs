using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Landing_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float LandingDistance = BlackBoard.Instance.LandingDistance;

        bool IsLanding = BlackBoard.Instance.IsLanding;

        //bool IsLanding = UtilityManager.DistanceCalc(DragonManager.Instance.transform.position, 
        //    BlackBoard.Instance.FiexdPosition, LandingDistance) && BlackBoard.Instance.IsFiexdPosition;

        bool IsFlying = BlackBoard.Instance.IsFlying;
        bool IsGround = BlackBoard.Instance.IsGround;

        bool IsAction = DragonManager.IsAction;

        if ((IsLanding && !IsGround && IsFlying && !IsAction) || IsAction)
        {
            ActionTask childAction = ChildNode.GetComponent<ActionTask>();

            if (childAction)
            {
                if (!childAction.IsRunning)
                {
                    if (!DragonManager.IsAction)
                        OnStart();
                    else if ((DragonManager.IsAction)) /*&& !IsLanding ))*/
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

                    return childAction.Run();
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
