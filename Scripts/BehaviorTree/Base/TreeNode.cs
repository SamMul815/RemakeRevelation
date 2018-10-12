using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public enum TASKSTATE
{
    SUCCESS = 0,
    FAULURE,
    RUNNING
}

public abstract class TreeNode : MonoBehaviour
{

    [HideInInspector]public DragonManager _manager;
    [HideInInspector]public BlackBoard _blackBoard;
    [HideInInspector]public Clock _clock;

    [HideInInspector] public MovementManager _movement;

    [HideInInspector]public Transform Dragon;
    [HideInInspector]public Transform Player;

    protected TASKSTATE _nodeState;
    public TASKSTATE NodeState { set { _nodeState = value; } get { return _nodeState; } }

    public virtual void Init()
    {
        _manager = DragonManager.Instance;
        _movement = MovementManager.Instance;

        _blackBoard = BlackBoard.Instance;
        _clock = Clock.Instance;

        Dragon = DragonManager.Instance.transform;
        Player = DragonManager.Instance.Player;

    }

    public virtual void ChildAdd(TreeNode node)
    {

    }

    public virtual void OnStart()
    {
        NodeState = TASKSTATE.RUNNING;
    }
    public abstract bool Run();
    public virtual void OnEnd()
    {
        NodeState = TASKSTATE.FAULURE;
    }

}
