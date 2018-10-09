using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public abstract class DecoratorTask : TreeNode
{
    protected TreeNode _childNode;
    public TreeNode ChildNode { get { return _childNode; } }

    public override void OnStart()
    {
        if (ChildNode.GetComponent<ActionTask>())
        {
            ActionTask childAction = ChildNode.GetComponent<ActionTask>();
            DragonManager.SetActionTask(childAction);
            DragonManager.IsAction = true;
        }
        base.OnStart();
    }

    public override void OnEnd()
    {
        base.OnEnd();
        if (ChildNode.GetComponent<ActionTask>())
        {
            if (ChildNode.GetComponent<ActionTask>().IsRunning)
            {
                ChildNode.OnEnd();
            }
        }
    }

    public override void ChildAdd(TreeNode node)
    {  
        _childNode = node;
    }
}
