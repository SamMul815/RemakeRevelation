﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_BreathAttack_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        if (_blackBoard.IsPlayerDashAttack)
        {
            float waitingTime = _clock.BreathWaitingTime;
            _actionCor = IsPlayerDashAttackTakeDamege(waitingTime);
            CoroutineManager.DoCoroutine(_actionCor);
            return;
        }
        Clock.Instance.CurBreathCoolingTime = 0.0f;
    }

    public override bool Run()
    {
        if (!_manager.IsTurn && !_blackBoard.IsPlayerDashAttack)
        {
            Vector3 DragonPos = Dragon.position;
            Vector3 PlayerPos = Player.position;

            DragonPos.y = 0.0f;
            PlayerPos.y = 0.0f;

            Vector3 forward = (PlayerPos - DragonPos).normalized;

            if (Vector3.Dot(Dragon.forward, forward) < 0.99f)
            {
                Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    0.05f);
                return false;
            }

            _manager.IsTurn = true;
            DragonAniManager.SwicthAnimation("Dragon_Breath");
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _blackBoard.IsPlayerDashAttack = false;
    }

    IEnumerator IsPlayerDashAttackTakeDamege(float waitingTime)
    {

        yield return CoroutineManager.GetWaitForSeconds(waitingTime);
        DragonAniManager.SwicthAnimation("Dragon_Breath");

    }


}
