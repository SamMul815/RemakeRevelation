using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;
using System;

/*
    만 든 날 : 2018-03-29
    작 성 자 : 전민수

    노드 총 관리 Manager

    베지어곡선을 이용하여 노드 만들기     
*/

/*
 *  수정한날 : 2018 - 04 - 30
 *  작성자 : 김영민
 *  수정내역: 업벡터 관련 추가 리스트 생성
 */
[RequireComponent(typeof(MoveStat))]
[RequireComponent(typeof(BezierNodeEventCollection))]
public class NodeManager : MonoBehaviour
{
    [SerializeField]
    private MovementType _movementTag;

    private static MovementManager _manager;

    private BezierNodeEventCollection _eventCollection;

    private MoveStat _stat;
    public MoveStat Stat { get { return _stat; } }

    public List<BezierNode> Nodes = new List<BezierNode>();    //노드들

    private List<Vector3> _nodesDir = new List<Vector3>();
    private List<Quaternion> _nodesRot = new List<Quaternion>();
    private List<bool> _nodesUp = new List<bool>();         //노드 Up vecter

    private List<float> _nodesSpeed = new List<float>();    //노드 Speed

    private Vector3 _curveNodeCenter;   //E
    private Vector3 _nextNodeCenter;    //F
    private Vector3 _arriveNodePos;     //최종노드위치값

    private bool _isMoving;
    private bool _isMoveEnd;

    private int _curNodeIndex;

    private int _segmentIndex;
    private int _segmentCount;

    private bool _isFindNode;

    private Dictionary<int, List<NodeEvnData>> _nodeTimeEvents;

    public float TimeInterval = 0.02f; //dir / speed;

    public MovementType MovementTag { get { return _movementTag; } }

    public MovementManager Manager { get { return _manager; } }

    public BezierNodeEventCollection EventCollection { get { return _eventCollection; } }

    public List<Vector3> NodesDir { get { return _nodesDir; } }
    public List<Quaternion> NodesRot { get { return _nodesRot; } }
    public List<float> NodesSpeed { get { return _nodesSpeed; } }
    public List<bool> NodesDragonUp { get { return _nodesUp; } }

    public bool IsMoving { set { _isMoving = value; } get { return _isMoving; } }
    public bool IsMoveEnd { set { _isMoveEnd = value; } get { return _isMoveEnd; } }

    public int CurNodeIndex { set { _curNodeIndex = value; } get { return _curNodeIndex; } }

    public int SegmentIndex { set { _segmentIndex = value; } get { return _segmentIndex; } }
    public int SegmentCount { set { _segmentCount = value; } get { return _segmentCount; } }

    public bool IsStick;
    public bool IsLoop;

    public Transform CenterAxis; //Node 중심축

    private void Awake()
    {
        _eventCollection = GetComponent<BezierNodeEventCollection>();
        _manager = MovementManager.Instance;
        _stat = GetComponent<MoveStat>();
    }

    private void Start()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].Init();
        }
    }

    private void Update()
    {
        if (!_isFindNode)
        {
            _isFindNode = FindNode();

            if (_isFindNode)
                AllNodesCalc();
        }
        if (_isFindNode)
        {
            Movement();
        }
    }

    public void Init()
    {
        Clear();
        _segmentIndex = 0;
        _isMoving = true;
        _isMoveEnd = false;
        _isFindNode = false;
        _segmentCount = _nodesSpeed.Count;
    }

    private bool FindNode()
    {
        if (IsStick)
        {
            transform.position = _manager.transform.position;
            Vector3 PlayerPos = DragonManager.Instance.Player.position;
            Vector3 thisPos = transform.position;

            PlayerPos.y = 0.0f;
            thisPos.y = 0.0f;

            transform.rotation = Quaternion.LookRotation((PlayerPos- thisPos).normalized);

            return true;
        }
        else
        {
            Vector3 forward = (transform.position - _manager.transform.position).normalized;

            _manager.CurSpeed = BlackBoard.Instance.Acceleration(_manager.CurSpeed, Nodes[0].NodeSpeed, 50.0f);

            _manager.transform.position =
                Vector3.MoveTowards(
                    _manager.transform.position,
                    transform.position,
                    _manager.CurSpeed * Time.deltaTime);

            _manager.transform.rotation =
                Quaternion.Slerp(
                    _manager.transform.rotation,
                    Quaternion.LookRotation(forward),
                    360.0f * Time.deltaTime);

            return UtilityManager.DistanceCalc(_manager.transform, transform, 0.0f);
        }
    }

    private void Movement()
    {
        Transform moveObj = _manager.transform;

        if (_segmentIndex < _segmentCount)
        {

            float moveDistance = _stat.NodeSpeed[_segmentIndex] * Time.deltaTime; //움직인 거리
            float nextDistance = Vector3.Distance(_stat.NodeDir[_segmentIndex], moveObj.position); //남은 거리

            //방향 구하기
            Vector3 dir = (_stat.NodeDir[_segmentIndex] - moveObj.position).normalized;

            Quaternion Angle = _nodesRot[_segmentIndex];//로테이션 앵글값 구하기

            bool dragonUp = _nodesUp[_segmentIndex]; //Up백터

            for (; moveDistance > nextDistance;) // 현재거리가 남은거리보다 작으면
            {
                moveObj.position += dir * nextDistance;//이동
                moveDistance -= nextDistance;//움직인 거리에서 이동한 거리 빼기

                _segmentIndex++;

                if (_segmentIndex >= _segmentCount) return; // 현재 이동이 끝났을 경우

                dir = (_stat.NodeDir[_segmentIndex] - moveObj.position).normalized;

                Angle = _nodesRot[_segmentIndex];//로테이션 앵글값 구하기

                nextDistance = Vector3.Distance(_stat.NodeDir[_segmentIndex], moveObj.position);
            }

            if (CenterAxis != null)// 노드의 중심축이 있는지
            {
                Vector3 CentralAxis = (CenterAxis.position - transform.position).normalized; //중심축 방향벡터

                moveObj.rotation = Quaternion.Slerp(
                    moveObj.rotation,
                    Quaternion.LookRotation(dir, CentralAxis),
                    45.0f * Time.deltaTime);
            }

            else// 노드 중심축이 없으면 노드의 회전에 따라서 회전하기
            {
                Quaternion rot;

                if (dragonUp)// 드래곤의 up백터를 이용해서 회전을 할건인지...
                {
                    rot = Quaternion.Slerp(
                        moveObj.rotation,
                        Quaternion.LookRotation(dir, _manager.transform.up) * Angle,
                        0.1f);
                }
                else
                {
                    rot = Quaternion.Slerp(
                        moveObj.rotation,
                        Quaternion.LookRotation(dir, Vector3.up) * Angle,
                        0.1f);
                }

                moveObj.rotation = rot;
            }

            moveObj.position += dir * moveDistance;

            if (_segmentIndex + 1 >= _segmentCount)
                return;

            /* --- 노드 이벤트 부분 --- */
            _curNodeIndex = (int)(_segmentIndex / Nodes[_curNodeIndex].NodeSegment);

            for (int i = 0; i < Nodes[_curNodeIndex].NodeEvent.Count; i++)
            {

                float t = (1.0f / Nodes[_curNodeIndex].NodeSegment) * _segmentIndex;

                if (t >= 1.0f)
                {
                    t -= (int)t;
                    t = Mathf.Round(t * 1000.0f) / 1000f;
                }

                NodeEvnData NodeEvent = Nodes[_curNodeIndex].NodeEvent[i];

                bool isRunning = Nodes[_curNodeIndex].IsRunning[i];

                if (NodeEvent.RunningTime <= t)
                {
                    if (!isRunning)
                    {
                        _eventCollection.SendMessage(NodeEvent.FunctionName);
                        Nodes[_curNodeIndex].IsRunning[i] = true;
                    }
                }
            }

            /* --- 노드 이벤트 부분 끝 --- */

            dir = (Stat.NodeDir[_segmentIndex] - moveObj.position).normalized;
        }

        else
        {
            if (!IsLoop)
            {
                _isMoveEnd = true;
                _isMoving = false;
                enabled = false;
            }
            else
            {
                Init();
            }

        }
    }

    private void AllNodesCalc()  //전체 노드 거리 계산
    {
        Vector3 nextDis = Nodes[0].transform.position;//현재좌표

        for (int index = 0; index + 1 < Nodes.Count; index++) //초기 노드부터 최종 노드까지 돌아가
        {
            for (int segment = 0; segment < Nodes[index].NodeSegment; segment++)    //다음좌표가 다음 노드포지션 값이랑 일치하는지
            {
                float t = (1.0f / Nodes[index].NodeSegment) * segment;
                float tt = (1.0f - t);

                float speed = Mathf.Lerp(Nodes[index].NodeSpeed, Nodes[index + 1].NodeSpeed, t);

                Quaternion rot = Quaternion.Slerp(Nodes[index].GetRotate, Nodes[index + 1].GetRotate, t);

                nextDis = CalcNodePos(index, index + 1, tt, t);  //현재 좌표

                _nodesRot.Add(rot);//노드들의 방향
                _stat.NodeRot.Add(rot);//노드의 방향

                _nodesSpeed.Add(speed);//노드들의 스피드
                _stat.NodeSpeed.Add(speed);//노드의 스피드

                _nodesDir.Add(nextDis);//노드들의 거리
                _stat.NodeDir.Add(nextDis);//노드의 거리

                _nodesUp.Add(Nodes[index].DragonUp);
                _segmentCount++;
                
                t += TimeInterval; //시간 0~1

            }

        }

    }

    private Vector3 CalcNodePos(int Current, int Next, float LostT, float NextT)   //하나 하나의 노드 거리 계산
    {
        if (Current >= Nodes.Count || Next >= Nodes.Count)
            return Vector3.zero;


        Transform Node = Nodes[Current].transform;                  //A
        Transform CurveNode = Nodes[Current].CurveNode;             //B
        Transform NextNode = Nodes[Next].transform;                 //C

        _curveNodeCenter = (LostT * Node.position) + (NextT * CurveNode.position);       //E
        _nextNodeCenter = (LostT * CurveNode.position) + (NextT * NextNode.position);    //F

        _arriveNodePos = (LostT * _curveNodeCenter) + (NextT * _nextNodeCenter);          //도착

        return _arriveNodePos;

    }

    private void Clear()
    {
        _nodesDir.Clear();
        _nodesRot.Clear();
        _nodesSpeed.Clear();
        _nodesUp.Clear();
        _stat.Clear();

        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].Reset();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 to = Nodes[0].transform.position;//다음좌표
        Vector3 from = Nodes[0].transform.position;//이전좌표

        //초기 노드부터 최종 노드까지 돌아가
        for (int index = 0; index + 1 < Nodes.Count; index++)
        {
            for (int segment = 0; segment < Nodes[index].NodeSegment; segment++)
            {
                float t = (1.0f / Nodes[index].NodeSegment) * segment;
                float tt = (1.0f - t);

                from = to;  //전 좌표
                to = CalcNodePos(index, index + 1, tt, t);  //현재 좌표

                Gizmos.DrawLine(from, to);  //라인
                t += TimeInterval; //시간 0~1
            }
        }
    }

}
