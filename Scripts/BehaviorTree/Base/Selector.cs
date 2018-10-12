using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : CompositeTask
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
                    _manager.SetActionTask(childAction);
                }
                if(childAction.Run())
                {
                    return true;
                }
                return false;
            }
            if (child.Run())
            {
                if (NodeState != TASKSTATE.RUNNING)
                    OnStart();
                return true;
            }
        }
        if (NodeState != TASKSTATE.FAULURE)
            OnEnd();

        return false; 
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }
}
