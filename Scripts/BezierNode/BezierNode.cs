using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 
    만 든 날 : 2018-03-29 - 21:15
    작 성 자 : 전민수

    기본 노드 Node

    베지어곡선을 이용하여 노드 만들기
    
*/
/*

   수정한날 : 2018 - 04 - 30
   작성자  : 김영민

   수정작업 : 
   이동시 보간할때 사용되는 Up벡터를 오브젝트 중점으로 사용할지 월드 중점으로 사용할지
   체크없으면 Vector3.Up 체크하면 Transform.up

*/


[Serializable]
public struct NodeEvnData
{
    [Range(0.0f, 1.0f)]
    public float RunningTime;
    public string FunctionName;

    [HideInInspector]
    public bool IsRunning;

    public NodeEvnData(float RunningTime, string FunctionName, bool IsRunning)
    {
        this.RunningTime = RunningTime;
        this.FunctionName = FunctionName;
        this.IsRunning = IsRunning;
    }

}

public class BezierNode : MonoBehaviour
{

    public Transform CurveNode;         //커브노드(중간노드)

    [SerializeField]
    private float _nodeSpeed = 5.0f;    //노드 스피드
    public float NodeSpeed { set { _nodeSpeed = value; } get { return _nodeSpeed; } }

    [SerializeField]
    private float _nodeSegment;
    public float NodeSegment { set { _nodeSegment = value; } get { return _nodeSegment; } }

    [SerializeField]
    private bool _dragonUp;
    public bool DragonUp { get { return _dragonUp; } }

    [Header("Event")]

    [SerializeField]
    private List<NodeEvnData> _nodeEvent = new List<NodeEvnData>();
    public List<NodeEvnData> NodeEvent { get { return _nodeEvent; } }

    private List<bool> _isRunning = new List<bool>();
    public List<bool> IsRunning { set { _isRunning = value; } get { return _isRunning; } }

    public Quaternion GetRotate { get { return transform.localRotation; } }
        
    public void Init()
    {
        for (int i = 0; i < _nodeEvent.Count; i++)
        {
            _isRunning.Add(false);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < _isRunning.Count; i++)
        {
            _isRunning[i] = false;
        }
    }

}
