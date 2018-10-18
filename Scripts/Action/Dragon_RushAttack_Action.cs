﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RushAttack_Action : ActionTask
{
    private float _moveDistance = 0.0f;
    private float _rushSpeed;
    Vector3 forward;


    public override void Init()
    {
        base.Init();
    }

    public override void OnStart()
    {
        base.OnStart();
        _blackBoard.IsDashAttackOn = false;
        _blackBoard.IsRushAttackOn = false;
        _manager.Stat.DashMovePosition = Player.position;
        _clock.CurDashCoolingTime = 0.0f;
    }

    public override bool Run()
    {
        if (!_manager.IsTurn)
        {
            Vector3 DragonPos = Dragon.position;
            Vector3 PlayerPos = Player.position;

            DragonPos.y = 0.0f;
            PlayerPos.y = 0.0f;

            forward = (PlayerPos - DragonPos).normalized;

            if (Vector3.Dot(Dragon.forward, forward) < 0.99f)
            {
                //DragonAniManager.SwicthAnimation("LeftTrun");
                Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    CurTurnTime / MaxTurnTime);
                CurTurnTime += Time.deltaTime;
                return false;
            }
            DragonAniManager.SwicthAnimation("Dragon_Rush");
            _manager.Stat.DashMovePosition = Player.position;

            _moveDistance = (Dragon.position - Player.position).sqrMagnitude;
            _moveDistance = Mathf.Sqrt(_moveDistance);

            _manager.IsTurn = true;
        }


        if (_blackBoard.IsRushAttackOn)
        {
            float Distance = _manager.Stat.RushMoveLimitDistance;
            _rushSpeed = _moveDistance;

            if (!UtilityManager.DistanceCalc(Dragon.position, _manager.Stat.DashMovePosition, Distance))
            {
                Dragon.position = Vector3.MoveTowards(
                    Dragon.position,
                    _manager.Stat.DashMovePosition,
                    _rushSpeed * Time.deltaTime);
            }

        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _blackBoard.IsDashAttackOn = false;
        _blackBoard.IsRushAttackOn = false;
    }

}
