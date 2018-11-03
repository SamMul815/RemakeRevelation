using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class BlackBoard : Singleton<BlackBoard>
{
    [SerializeField]
    private bool _isPlayer;
    public bool IsPlayer { get { return _isPlayer; } }

    [SerializeField]
    private Transform _dragonBulletBreathMouth;
    public Transform DragonBulletBreathMouth { get { return _dragonBulletBreathMouth; } }

    [SerializeField]
    private Transform _dragonBreathMouth;
    public Transform DragonBreathMouth { get { return _dragonBreathMouth; } }

    private Vector3 _fiexdPosition;
    public Vector3 FiexdPosition { set { _fiexdPosition = value; } get { return _fiexdPosition; } }

    private bool _isFiexdPosition;
    public bool IsFiexdPosition { set { _isFiexdPosition = value; } get { return _isFiexdPosition; } }

    [Space]
    [Header("Distance")]

    [SerializeField]
    private float _pawAttackDistance;
    public float PawAttackDistance { get { return _pawAttackDistance; } }

    [SerializeField]
    private float _breathAttackDistance;
    public float BreathAttackDistance { get { return _breathAttackDistance; } }

    [SerializeField]
    private float _howlingDistance;
    public float HowlingDistance { get { return _howlingDistance; } }

    [SerializeField]
    private float _dashDistance;
    public float DashDistance { get { return _dashDistance; } }

    [SerializeField]
    private float _rushDistance;
    public float RushDistance { get { return _rushDistance; } }

    [SerializeField]
    private float _bullletBreathAttackDistance;
    public float BullletBreathAttackDistance { get { return _bullletBreathAttackDistance; } }

    [SerializeField]
    private float _descentAttackFiexdDistance;
    public float DescentAttackFiexdDistance { get { return _descentAttackFiexdDistance; } }

    private float _landingDistance;
    public float LandingDistance { set { _landingDistance = value; } get { return _landingDistance; } }

    [SerializeField]
    private float _redZoneDistance;
    public float RedZoneDistance { get { return _redZoneDistance; } }

    [SerializeField]
    private float _fallingDistance;
    public float FallingDistance { get { return _fallingDistance; } }

    [SerializeField]
    private float _airSpearLimitDistance;
    public float AirSpearLimitDistance { get { return _airSpearLimitDistance; } }

    [Space]
    [Header("Bullet Breath Amount")]

    [SerializeField]
    private int _fanShapeAmount;
    public int FanShapeAmount { get { return _fanShapeAmount; } }


    private bool _isDashAttackOn;
    public bool IsDashAttackOn { set { _isDashAttackOn = value; } get { return _isDashAttackOn; } }

    private bool _isRushAttackOn;
    public bool IsRushAttackOn { set { _isRushAttackOn = value; } get { return _isRushAttackOn; } }

    private bool _isTailAttackOn;
    public bool IsTailAttackOn { set { _isTailAttackOn = value; } get { return _isTailAttackOn; } }


    //Dragon State
    private bool _isWatingState;
    public bool IsWatingState { set { _isWatingState = value; } get { return _isWatingState; } }

    private bool _isGround;
    public bool IsGround { set { _isGround = value; } get { return _isGround; } }

    private bool _isDescentAttack;
    public bool IsDescentAttack { set { _isDescentAttack = value; } get { return _isDescentAttack; } }

    private bool _isMeteoAttack;
    public bool IsMeteoAttack { set { _isMeteoAttack = value; } get { return _isMeteoAttack; } }

    private bool _isMeteoHovering;
    public bool IsMeteoHovering { set { _isMeteoHovering = value; } get { return _isMeteoHovering; } }

    private bool _isMeteoLoitering;
    public bool IsMeteoLoitering { set { _isMeteoLoitering = value; } get { return _isMeteoLoitering; } }

    private bool _isFlying;
    public bool IsFlying { set { _isFlying = value; } get { return _isFlying; } }

    private bool _isLanding;
    public bool IsLanding { set { _isLanding = value; } get { return _isLanding; } }

    private bool _isDead;
    public bool IsDead { set { _isDead = value; } get { return _isDead; } }

    private bool _isDestroyPart;
    public bool IsDestroyPart { set { _isDestroyPart = value; } get { return _isDestroyPart; } }

    private bool _isPlayerRushAttack;
    public bool IsPlayerRushAttack { set { _isPlayerRushAttack = value; } get { return _isPlayerRushAttack; } }

    private bool _isRedZoneIn;
    public bool IsRedZoneIn { set { _isRedZoneIn = value; } get { return _isRedZoneIn; } }

    private bool _isAirSpear;
    public bool IsAirSpear { set { _isAirSpear = value; } get { return _isAirSpear; } }

    [SerializeField]
    private float _redZoneDamage;
    public float RedZoneDamage { get { return _redZoneDamage; } }


    private void Awake()
    {
        _isGround = true;
    }

    public float Acceleration(float fCurSpeed, float fMaxSpeed, float fAccSpeed)
    {
        if (fCurSpeed >= fMaxSpeed)
            return fMaxSpeed;
        else
        {
            float dir = Mathf.Sign(fMaxSpeed - fCurSpeed);
            fCurSpeed += fAccSpeed * dir * Time.fixedDeltaTime;
            return (dir == Mathf.Sign(fMaxSpeed - fCurSpeed)) ? fCurSpeed : fMaxSpeed;
        }

    }

    public bool LandingRayHit(Transform trans, Vector3 dir, float distance, int layerMask)
    {
        RaycastHit hit;
        return Physics.SphereCast(trans.position, trans.lossyScale.x, dir, out hit, distance, layerMask);
    }

    public bool IsAirSpearAttack(Transform trans, Vector3 dir, float distance, int layerMask)
    {
        Collider [] cols = Physics.OverlapSphere(trans.position, distance, layerMask);

        for (int i = 0; i < cols.Length; i++)
            if(LayerMask.LayerToName(cols[i].gameObject.layer) == "Wall") return true;

        return false;
    }


}
