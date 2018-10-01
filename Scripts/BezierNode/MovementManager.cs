using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    TakeOff
}

public class MovementManager : Singleton<MovementManager> {

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
            _nodesManager[_currentMoveType].IsMoveEnd = true;
            _nodesManager[_currentMoveType].enabled = false;
        }

        _currentMoveType = moveType;
        _nodesManager[_currentMoveType].Init();
        _nodesManager[_currentMoveType].enabled = true;

    }

    //노드 포지션 및 로테이션 셋팅
    //public void MovementReady(MovementType TYPE)
    //{
    //    _nodesManager[TYPE].AllNodesCalc();

    //    int NodesCount = _nodesManager[TYPE].NodesSpeed.Count;

    //    for (int nodeIndex = 0; nodeIndex < NodesCount; nodeIndex++)
    //    {
    //        _nodesManager[TYPE].Stat.NodeDir.Add(_nodesManager[TYPE].NodesDir[nodeIndex]);
    //        _nodesManager[TYPE].Stat.NodeSpeed.Add(_nodesManager[TYPE].NodesSpeed[nodeIndex]);
    //        _nodesManager[TYPE].Stat.NodeRot.Add(_nodesManager[TYPE].NodesRot[nodeIndex]);
    //    }
    //    _nodesManager[TYPE].IsMoveReady = true;

    //}

    //노드를 따라서 이동 및 회전

    //private void Movements(MovementType TYPE)
    //{
    //    int NodesCount = _nodesManager[TYPE].NodesSpeed.Count;
    //    int NodesIndex = _nodesManager[TYPE].NodesIndex;

    //    if (NodesIndex < NodesCount)
    //    {
    //        _nodesManager[TYPE].IsMoveEnd = false;

    //        float moveDistance = _nodesManager[TYPE].Stat.NodeSpeed[NodesIndex] * Time.deltaTime;
    //        float nextDistance = Vector3.Distance(_nodesManager[TYPE].Stat.NodeDir[NodesIndex], transform.position);
            
    //        Vector3 dir = (_nodesManager[TYPE].Stat.NodeDir[NodesIndex] - transform.position).normalized;
            
    //        Vector3 eulerAngle = _nodesManager[TYPE].NodesRot[NodesIndex] + new Vector3(0.0f, transform.rotation.eulerAngles.y, 0.0f);
    //        bool dragonUp = _nodesManager[TYPE].NodesDragonUp[NodesIndex];

    //        for (; moveDistance > nextDistance;)
    //        {
    //            transform.position += dir * nextDistance;
    //            moveDistance -= nextDistance;

    //            _nodesManager[TYPE].NodesIndex++;
    //            NodesIndex = _nodesManager[TYPE].NodesIndex;

    //            if (NodesIndex >= NodesCount)
    //                return;
    //            dir = (_nodesManager[TYPE].Stat.NodeDir[NodesIndex] - transform.position).normalized;

    //            eulerAngle = _nodesManager[TYPE].NodesRot[NodesIndex] + new Vector3(0.0f, transform.rotation.eulerAngles.y, 0.0f);


    //            nextDistance = Vector3.Distance(_nodesManager[TYPE].Stat.NodeDir[NodesIndex],
    //                transform.position);

    //        }

    //        if (_nodesManager[TYPE].CenterAxisRot != null)
    //        {
    //            Vector3 CentralAxis = (_nodesManager[TYPE].CenterAxisRot.position - transform.position).normalized;

    //            transform.rotation =
    //                Quaternion. Slerp(
    //                    transform.rotation,
    //                    Quaternion.LookRotation(dir, CentralAxis),
    //                    45.0f * Time.fixedDeltaTime);
    //        }
    //        else
    //        {
    //            Quaternion rot;

    //            if (dragonUp)
    //            {
    //                rot = Quaternion.Slerp(transform.rotation,
    //                    Quaternion.LookRotation(dir, transform.up) * Quaternion.Euler(eulerAngle), 0.1f);
    //            }
    //            else
    //            {
    //                rot = Quaternion.Slerp(transform.rotation,
    //                    Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(eulerAngle), 0.1f);
    //            }
    //            transform.rotation = rot;
    //        }

    //        transform.position += dir * moveDistance;

    //        if (NodesIndex + 1 >= NodesCount)
    //            return;
    //        dir = (_nodesManager[TYPE].Stat.NodeDir[NodesIndex] - transform.position).normalized;

    //    }
    //    else
    //    {
    //        _nodesManager[TYPE].IsMoveEnd = true;
    //    }
    //}


}
