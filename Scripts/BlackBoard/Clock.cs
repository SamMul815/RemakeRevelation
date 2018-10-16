using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Singleton<Clock>
{
    [Space]
    [Header("Cooling Time")]

    [SerializeField]
    private float _waitingCoolingTime;
    public float WaitingCoolingTime { get { return _waitingCoolingTime; } }

    private float _curWaitingCoolingTime;
    public float CurWaitingCoolingTime { set { _curWaitingCoolingTime = value; } get { return _curWaitingCoolingTime; } }

    [SerializeField]
    private float _bulletBreathCoolingTime;
    public float BulletBreathCoolingTime { get { return _bulletBreathCoolingTime; } }

    private float _curBulletBreathCoolingTime;
    public float CurBulletBreathCoolingTime { set { _curBulletBreathCoolingTime = value; } get { return _curBulletBreathCoolingTime; } }

    [SerializeField]
    private float _howlingCoolingTime;
    public float HowlingCoolingTime { get { return _howlingCoolingTime; } }

    private float _curHowlingCoolingTime;
    public float CurHowlingCoolingTime { set { _curHowlingCoolingTime = value; } get { return _curHowlingCoolingTime; } }

    [SerializeField]
    private float _dashCoolingTime;
    public float DashCoolingTime { get { return _dashCoolingTime; } }

    private float _curDashCoolingTime;
    public float CurDashCoolingTime { set { _curDashCoolingTime = value; } get { return _curDashCoolingTime; } }

    [SerializeField]
    private float _breathCoolingTime;
    public float BreathCoolingTime { get { return _breathCoolingTime; } }

    private float _curBreathCoolingTime;
    public float CurBreathCoolingTime { set { _curBreathCoolingTime = value; } get { return _curBreathCoolingTime; } }

    [SerializeField]
    private float _pawCoolingTime;
    public float PawCoolingTime { get { return _pawCoolingTime; } }

    private float _curPawCoolingTime;
    public float CurPawCoolingTime { set { _curPawCoolingTime = value; } get { return _curPawCoolingTime; } }

    [SerializeField]
    private float _breathWaitingTime;
    public float BreathWaitingTime { get { return _breathWaitingTime; } }

    private float _curRedZoneTime;
    public float CurRedZoneTime { set { _curRedZoneTime = value; } get { return _curRedZoneTime; } }

    [SerializeField]
    private float _timeInterval;
    public float TimeInterval { get { return _timeInterval; } }


    private void Awake()
    {
        _curWaitingCoolingTime = _waitingCoolingTime;
    }


    private void FixedUpdate()
    {
        if (_curBulletBreathCoolingTime < _bulletBreathCoolingTime)
            _curBulletBreathCoolingTime += Time.deltaTime;

        if (_curHowlingCoolingTime < _howlingCoolingTime)
            _curHowlingCoolingTime += Time.deltaTime;

        if (_curDashCoolingTime < _dashCoolingTime)
            _curDashCoolingTime += Time.deltaTime;

        if (_curPawCoolingTime < _pawCoolingTime)
            _curPawCoolingTime += Time.deltaTime;

        if (_curBreathCoolingTime < _breathCoolingTime)
            _curBreathCoolingTime += Time.deltaTime;

        if (BlackBoard.Instance.IsRedZoneIn)
        {
            _curRedZoneTime += _timeInterval;
        }
        else
        {
            if (_curRedZoneTime > 0.0f)
            {
                _curRedZoneTime -= _timeInterval;
            }
            else
            {
                _curRedZoneTime = 0.0f;
            }
        }

    }


}
