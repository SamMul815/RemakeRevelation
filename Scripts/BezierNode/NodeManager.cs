using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

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
public class NodeManager : MonoBehaviour
{

    [SerializeField]
    private MovementType _movementTag;

    private static MovementManager _manager;

    private MoveStat _stat;
    public MoveStat Stat { get { return _stat; } }

    public List<BezierNode> Nodes = new List<BezierNode>();    //노드들

    private List<Vector3> _nodesDir = new List<Vector3>();
    private List<Vector3> _nodesRot = new List<Vector3>();
    private List<bool> _nodesUp = new List<bool>();         //노드 Up vecter

    private List<float> _nodesSpeed = new List<float>();    //노드 Speed

    private Vector3 _curveNodeCenter;   //E
    private Vector3 _nextNodeCenter;    //F
    private Vector3 _arriveNodePos;     //최종노드위치값

    private bool _isMoveEnd;
    private int _nodesIndex;
    private int _nodesCount;

    private bool _isFindNode;

    public float TimeInterval = 0.02f; //dir / speed;


    public MovementType MovementTag { set { _movementTag = value; } get { return _movementTag; } }
    public MovementManager Manager { get { return _manager; } }

    public List<Vector3> NodesDir { get { return _nodesDir; } }
    public List<Vector3> NodesRot { get { return _nodesRot; } }
    public List<float> NodesSpeed { get { return _nodesSpeed; } }
    public List<bool> NodesDragonUp { get { return _nodesUp; } }

    public bool IsMoveEnd { set { _isMoveEnd = value; } get { return _isMoveEnd; } }
    public int NodesIndex { set { _nodesIndex = value; } get { return _nodesIndex; } }
    public int NodesCount { set { _nodesCount = value; } get { return _nodesCount; } }

    public bool IsStick;
    public bool IsLoop;
    public bool IsPlayer;

    public Transform CenterAxisRot;

    private void Awake()
    {
        _manager = MovementManager.Instance;
        _stat = GetComponent<MoveStat>();
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
        NodesIndex = 0;
        _isMoveEnd = false;
        _isFindNode = false;
        _nodesCount = _nodesSpeed.Count;
    }

    private bool FindNode()
    {
        if (IsStick)
        {
            transform.position = _manager.transform.position;
            Vector3 PlayerPos = DragonManager.Player.position;
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

        Transform Dragon = _manager.transform;

        if (_nodesIndex < _nodesCount)
        {
            _isMoveEnd = false;

            float moveDistance = _stat.NodeSpeed[_nodesIndex] * Time.deltaTime; //움직인 거리
            float nextDistance = Vector3.Distance(_stat.NodeDir[_nodesIndex], Dragon.position); //남은 거리

            //방향 구하기
            Vector3 dir = (_stat.NodeDir[_nodesIndex] - Dragon.position).normalized;

            Vector3 eulerAngle = _nodesRot[_nodesIndex] + new Vector3(0.0f, 0.0f, Dragon.rotation.eulerAngles.z);//로테이션 앵글값 구하기
            bool dragonUp = _nodesUp[_nodesIndex]; //Up백터

            for (; moveDistance > nextDistance;) // 현재거리가 남은거리보다 작으면
            {
                Dragon.position += dir * nextDistance;//이동
                moveDistance -= nextDistance;//움직인 거리에서 이동한 거리 빼기

                _nodesIndex++;

                if (_nodesIndex >= _nodesCount) return; // 현재 이동이 끝났을 경우

                dir = (_stat.NodeDir[_nodesIndex] - Dragon.position).normalized;

                eulerAngle = _nodesRot[_nodesIndex] + new Vector3(0.0f, 0.0f, Dragon.rotation.eulerAngles.z);//로테이션 앵글값 구하기

                nextDistance = Vector3.Distance(_stat.NodeDir[_nodesIndex], Dragon.position);
            }

            if (CenterAxisRot != null)// 노드의 중심축이 있는지
            {
                Vector3 CentralAxis = (CenterAxisRot.position - transform.position).normalized; //중심축 방향벡터

                Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(dir, CentralAxis),
                    45.0f * Time.deltaTime);
            }

            else// 노드 중심축이 없으면 노드의 회전에 따라서 회전하기
            {
                Quaternion rot;

                if (dragonUp)// 드래곤의 up백터를 이용해서 회전을 할건인지...
                {
                    rot = Quaternion.Slerp(
                        Dragon.rotation,
                        Quaternion.LookRotation(dir, _manager.transform.up) * Quaternion.Euler(eulerAngle),
                        0.1f);
                }
                else
                {
                    rot = Quaternion.Slerp(
                        Dragon.rotation,
                        Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(eulerAngle),
                        0.1f);
                }

                Dragon.rotation = rot;
            }

            Dragon.position += dir * moveDistance;

            if (_nodesIndex + 1 >= _nodesCount)
                return;

            dir = (Stat.NodeDir[_nodesIndex] - Dragon.position).normalized;
        }

        else
        {
            if (!IsLoop)
            {
                _isMoveEnd = true;
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

                Vector3 rot = Vector3.Slerp(Nodes[index].GetRotate.eulerAngles, Nodes[index + 1].GetRotate.eulerAngles, t);

                nextDis = CalcNodePos(index, index + 1, tt, t);  //현재 좌표

                _nodesRot.Add(rot);//노드들의 방향
                _stat.NodeRot.Add(rot);//노드의 방향

                _nodesSpeed.Add(speed);//노드들의 스피드
                _stat.NodeSpeed.Add(speed);//노드의 스피드

                _nodesDir.Add(nextDis);//노드들의 거리
                _stat.NodeDir.Add(nextDis);//노드의 거리

                _nodesUp.Add(Nodes[index].DragonUp);
                _nodesCount++;
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
