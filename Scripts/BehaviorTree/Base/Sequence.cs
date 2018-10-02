using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : CompositeTask
{

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        foreach (TreeNode child in ChildNodes)
        {
            ActionTask childAction = child.GetComponent<ActionTask>();
            if (childAction)
            {
                if (childAction.NodeState != TASKSTATE.RUNNING &&
                    !childAction.IsRunning)
                {
                    OnStart();
                    DragonManager.SetActionTask(childAction);
                }
                if (!childAction.Run())
                    return false;

                return true;
            }
            if (!child.Run())
            {
                if (NodeState != TASKSTATE.RUNNING)
                    OnStart();
                return false;
            }
        }
        if (NodeState != TASKSTATE.FAULURE)
            OnEnd();

        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }
}
