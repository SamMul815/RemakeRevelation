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
    [HideInInspector] public Player _playerManager;
    [HideInInspector] public DragonManager _manager;
    [HideInInspector] public BlackBoard _blackBoard;
    [HideInInspector] public Clock _clock;

    [HideInInspector] public MovementManager _movement;

    [HideInInspector] public Transform DragonTransform;
    [HideInInspector] public Transform PlayerTransform;

    protected TASKSTATE _nodeState;
    public TASKSTATE NodeState { set { _nodeState = value; } get { return _nodeState; } }

    public virtual void Init()
    {
        _manager = DragonManager.Instance;
        _movement = MovementManager.Instance;
        _playerManager = Player.instance;

        _blackBoard = BlackBoard.Instance;
        _clock = Clock.Instance;

        DragonTransform = DragonManager.Instance.transform;
        PlayerTransform = UtilityManager.Instance.Player;

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
