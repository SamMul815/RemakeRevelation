using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public abstract class DecoratorTask : TreeNode
{
    protected TreeNode _childNode;
    public TreeNode ChildNode { get { return _childNode; } }

    protected ActionTask _childAction;

    public override void Init()
    {
        base.Init();
        if (ChildNode.GetComponent<ActionTask>())
        {
            _childAction = ChildNode.GetComponent<ActionTask>();
        }
        else
        {
            _childAction = null;
        }
    }

    public override void OnStart()
    {
        if (_childAction)
        {
            _manager.SetActionTask(_childAction);
            _manager.IsAction = true;
        }
        base.OnStart();
    }

    public override void OnEnd()
    {
        base.OnEnd();
        if (_childAction)
        {
            if (_childAction.IsRunning)
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
