using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Walk_Decorator : DecoratorTask
{

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        //Transform Dragon = UtilityManager.Instance.DragonTransform();
        //Transform Player = UtilityManager.Instance.PlayerTransform();

        //float Distance = BlackBoard.Instance.WalkDistance;

        //bool IsWalking = UtilityManager.DistanceCalc(Dragon, Player, Distance);
        //bool IsAttacking = DragonManager.IsAttacking;

        //if (IsWalking || IsAttacking)
        //{
        //    ActionTask childAction = ChildNode.GetComponent<ActionTask>();

        //    if (childAction)
        //    {
        //        if (!childAction.IsRunning)
        //        { 
        //            if (NodeState != TASKSTATE.RUNNING || childAction.IsEnd)
        //            {
        //                OnStart();
        //            }
        //        }
        //        else
        //        {
        //            return ChildNode.Run();
        //        }

        //    }
        //    else if (NodeState != TASKSTATE.RUNNING)
        //    {
        //        OnStart();
        //    }

        //    return ChildNode.Run();
        //}
        //else if (NodeState == TASKSTATE.RUNNING)
        //{
        //    OnEnd();
        //}

        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
