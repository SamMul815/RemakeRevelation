﻿using System.Collections;
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

        forward = (Player.position - _manager.transform.position).normalized;

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
            _blackBoard.FiexdPosition = Player.position;
            _blackBoard.IsFiexdPosition = true;
            fiexdPos = _blackBoard.FiexdPosition;
            CurTurnTime = 0.0f;
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

        if(UtilityManager.DistanceCalc(DragonPos, fiexdPos, 0.0f))
        {
            _blackBoard.FiexdPosition = fiexdPos;
            Dragon.position = fiexdPos;
            CurTurnTime = 0.0f;
        }

        if (_manager.LandingOn)
        {
            forward = (Player.position - Dragon.position).normalized;
            forward.y = 0.0f;

            Dragon.rotation =
                Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    CurTurnTime / MaxTurnTime);
            CurTurnTime += Time.deltaTime;
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
