﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DashAttack_Action : ActionTask
{
    Vector3 forward;

    public override void Init()
    {
        base.Init();
    }

    public override void OnStart()
    {
        base.OnStart();
        BlackBoard.Instance.IsDashAttackOn = false;
        BlackBoard.Instance.IsRushAttackOn = false;
        Clock.Instance.CurDashCoolingTime = 0.0f;
        forward = (Player.position - Dragon.position).normalized;

    }

    public override bool Run()
    {

        if (!_manager.IsTurn)
        {
            Vector3 DragonPos = Dragon.position;
            Vector3 PlayerPos = Player.position;

            DragonPos.y = 0.0f;
            PlayerPos.y = 0.0f;

            if (Vector3.Dot(Dragon.forward, forward) < 0.99f)
            {
                Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    CurTurnTime / MaxTurnTime);

                CurTurnTime += Time.deltaTime;

                return false;
            }

            DragonAniManager.SwicthAnimation("Dragon_Dash");
            _manager.IsTurn = true;
            forward = (Player.position - Dragon.position).normalized;
        }

        if (_blackBoard.IsDashAttackOn)
        {
            float Distance = _manager.Stat.DashMoveDistance;
            float DashSpeed = Distance; // *(dashTime - Time.deltaTime);
            Dragon.position += (forward) * DashSpeed * Time.deltaTime;
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
