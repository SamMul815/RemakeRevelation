using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Meteo,
    AirSpear
}

public class MovementManager : Singleton<MovementManager>
{

    [SerializeField]
    private MovementType _currentMoveType;
    public MovementType CurrentMoveType { get { return _currentMoveType; } }

    private float _curSpeed;
    public float CurSpeed { set { _curSpeed = value; } get { return _curSpeed; } }

    Dictionary<MovementType, NodeManager> _nodesManager = new Dictionary<MovementType, NodeManager>();

    private bool _isInit;

    private void Awake()
    {
        NodeManager[] nodeMangers = FindObjectsOfType<NodeManager>();

        foreach (NodeManager nm in nodeMangers)
        {
            _nodesManager.Add(nm.MovementTag, nm);
            _nodesManager[nm.MovementTag].enabled = false;
        }
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            _isInit = true;
        }
    }

    public NodeManager GetNodeManager()
    {
        return _nodesManager[_currentMoveType];
    }

    public void SetMovement(MovementType moveType)
    {
        if (_isInit)
        {
            if (!_nodesManager[_currentMoveType].IsMoveEnd)
                _nodesManager[_currentMoveType].IsMoveEnd = true;

            _nodesManager[_currentMoveType].enabled = false;
        }

        _currentMoveType = moveType;
        _nodesManager[_currentMoveType].Init();
        _nodesManager[_currentMoveType].enabled = true;

    }

}
