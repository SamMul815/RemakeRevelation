using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Landing_Action : ActionTask
{
    Vector3 fiexdPos;
    Vector3 forward;

    float maxSpeed = 0.0f;
    float accSpeed = 0.0f;
    float distance = 0.0f;

    public override void Init()
    {
        base.Init();
        maxSpeed = _manager.Stat.MaxSpeed;
        accSpeed = _manager.Stat.AccSpeed;
        distance = _blackBoard.DescentAttackFiexdDistance;
    }

    public override void OnStart()
    {
        base.OnStart();
        _blackBoard.IsFlying = false;
        _manager.AttackOff();

        DragonAniManager.SwicthAnimation("Dragon_Gliding");
        _manager.AttackOn(DragonAttackTriggers.AirSpear);
        _movement.CurSpeed += 20.0f;
    }

    public override bool Run()
    {

        forward = (Player.position - Dragon.position).normalized;

        _movement.CurSpeed =
            _blackBoard.Acceleration(_movement.CurSpeed, maxSpeed, accSpeed);

        bool IsLandingAttackFiexdDistance =
            UtilityManager.DistanceCalc(Dragon, Player, distance);

        if(!IsLandingAttackFiexdDistance && !_blackBoard.IsFiexdPosition)
        {
            Dragon.position = Vector3.MoveTowards(
                Dragon.position,
                Player.position,
                _movement.CurSpeed * Time.deltaTime);

            Dragon.rotation = Quaternion.Slerp(
                Dragon.rotation,
                Quaternion.LookRotation(forward, Vector3.up),
                CurTurnTime / MaxTurnTime);

            CurTurnTime += Time.deltaTime;
            return false;
        }

        if (!_blackBoard.IsFiexdPosition)
        {
            CurTurnTime = 0.0f;
            fiexdPos = _blackBoard.FiexdPosition;
            _blackBoard.FiexdPosition = Player.position;
            _blackBoard.IsFiexdPosition = true;
        }

        float LandingDistance = _blackBoard.LandingDistance;

        if (!UtilityManager.DistanceCalc(Dragon.position, _blackBoard.FiexdPosition, LandingDistance))
        {
            forward = (_blackBoard.FiexdPosition - Dragon.position).normalized;

            Dragon.position =
                Vector3.MoveTowards(
                    Dragon.position,
                    _blackBoard.FiexdPosition,
                    _movement.CurSpeed * Time.deltaTime);

            Dragon.rotation = Quaternion.Slerp(
                Dragon.rotation,
                Quaternion.LookRotation(forward, Vector3.up),
                CurTurnTime / MaxTurnTime);
            CurTurnTime += Time.deltaTime;
            return false;
        }

        DragonAniManager.SwicthAnimation("Dragon_Landing");

        Dragon.position = Vector3.MoveTowards(
            Dragon.position,
            _blackBoard.FiexdPosition,
            _movement.CurSpeed * Time.deltaTime);

        Vector3 DragonPos = Dragon.position;
        Vector3 PlayerPos = Player.position;

        if (UtilityManager.DistanceCalc(DragonPos, fiexdPos, 0.0f))
        {
            _blackBoard.FiexdPosition = fiexdPos;
            forward = (_blackBoard.FiexdPosition - Dragon.position).normalized;
            Dragon.rotation = Quaternion.LookRotation(forward);
            CurTurnTime = 0.0f;
        }

        if (_manager.LandingOn)
        {
            forward = (_blackBoard.FiexdPosition - Dragon.position).normalized;
            _manager.DragonRigidBody.constraints = RigidbodyConstraints.FreezePosition;
            Dragon.rotation = Quaternion.LookRotation(forward);
        }
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _manager.DragonRigidBody.constraints = RigidbodyConstraints.None;
        _manager.DragonRigidBody.freezeRotation = true;
        _blackBoard.IsFiexdPosition = false;
        _blackBoard.IsLanding = false;
        _blackBoard.IsGround = true;
    }

}
