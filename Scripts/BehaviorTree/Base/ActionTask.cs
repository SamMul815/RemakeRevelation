using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public abstract class ActionTask : TreeNode
{

    [SerializeField]
    protected bool _isAttackAction;
    public bool IsAttackAction { get { return _isAttackAction; } }

    protected bool _isRunning = false;
    public bool IsRunning { set { _isRunning = value; } get { return _isRunning; } }

    protected bool _isEnd = false;
    public bool IsEnd { set { _isEnd = value; } get { return _isEnd; } }

    protected IEnumerator _actionCor;
    public IEnumerator ActionCor { set { _actionCor = value; } get { return _actionCor; } }

    protected float _curTurnTime;
    public float CurTurnTime { set { _curTurnTime = value; } get { return _curTurnTime; } }

    protected float _maxTurnTime;
    public float MaxTurnTime { get { return _maxTurnTime; } }

    public override void Init()
    {
        base.Init();
        _curTurnTime = 0.0f;
        _maxTurnTime = 10.0f;
    }

    public override void OnStart()
    {
        base.OnStart();
        _isRunning = true;

        BlackBoard.Instance.IsWatingState = _isAttackAction ? true : false;

        if (_actionCor != null)
        {
            CoroutineManager.DoCoroutine(_actionCor);
        }
        _isEnd = false;
        _curTurnTime = 0.0f;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _isRunning = false;

        if (ActionCor != null)
        { 
            CoroutineManager.DontCoroutine(_actionCor);
        }
        _manager.IsTurn = false;
        _manager.IsAction = false;
        _isEnd = true;
    }

}
