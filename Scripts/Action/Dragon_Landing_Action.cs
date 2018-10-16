using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Landing_Action : ActionTask
{
    Vector3 FiexdPos = new Vector3();

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

        float Distance = _blackBoard.DescentAttackFiexdDistance;

        float MaxSpeed = _manager.Stat.MaxSpeed;
        float AccSpeed = _manager.Stat.AccSpeed;

        Vector3 forward = (Player.position - _manager.transform.position).normalized;

        _movement.CurSpeed =
            _blackBoard.Acceleration(_movement.CurSpeed, MaxSpeed, AccSpeed);

        bool IsLandingAttackFiexdDistance =
            UtilityManager.DistanceCalc(Dragon, Player, Distance);

        if(!IsLandingAttackFiexdDistance && !_blackBoard.IsFiexdPosition)
        {
            Dragon.position = Vector3.MoveTowards(
                Dragon.position,
                Player.position,
                _movement.CurSpeed * Time.deltaTime);

            Dragon.rotation = Quaternion.Slerp(
                Dragon.rotation,
                Quaternion.LookRotation(forward, Vector3.up),
                0.1f);

            return false;
        }

        if (!_blackBoard.IsFiexdPosition)
        {
            _blackBoard.FiexdPosition = Player.position;
            _blackBoard.IsFiexdPosition = true;
            FiexdPos = _blackBoard.FiexdPosition;
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
                0.1f);
            return false;
        }

        DragonAniManager.SwicthAnimation("Dragon_Landing");

        Dragon.position = Vector3.MoveTowards(
            Dragon.position,
            _blackBoard.FiexdPosition,
            _movement.CurSpeed * Time.deltaTime);

        Vector3 DragonPos = Dragon.position;

        if(UtilityManager.DistanceCalc(DragonPos, FiexdPos, 0.0f))
        {
            _blackBoard.FiexdPosition = FiexdPos;
            Dragon.position = FiexdPos;
        }

        if (_manager.LandingOn)
        {
            forward = (Player.position - Dragon.position).normalized;
            forward.y = 0.0f;

            Dragon.rotation =
                Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    0.05f);
        }
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _blackBoard.IsFiexdPosition = false;
        _blackBoard.IsLanding = false;
        _blackBoard.IsGround = true;
    }

}
