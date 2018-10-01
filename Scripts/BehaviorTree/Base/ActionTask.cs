using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public abstract class ActionTask : TreeNode
{
    protected bool _isRunning = false;
    public bool IsRunning { set { _isRunning = value; } get { return _isRunning; } }

    protected bool _isEnd = false;
    public bool IsEnd { set { _isEnd = value; } get { return _isEnd; } }

    protected IEnumerator _actionCor;
    public IEnumerator ActionCor { set { _actionCor = value; } get { return _actionCor; } }

    public override void OnStart()
    {
        base.OnStart();
        _isRunning = true;
        if (_actionCor != null)
        {
            CoroutineManager.DoCoroutine(_actionCor);
        }
        _isEnd = false;
    }
    public override void OnEnd()
    {
        base.OnEnd();
        _isRunning = false;
        if (ActionCor != null)
        {
            CoroutineManager.DontCoroutine(_actionCor);
        }
        DragonManager.IsTurn = false;
        DragonManager.IsAction = false;
        _isEnd = true;
    }

}
