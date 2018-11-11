﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_MeteoAttack_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        MovementManager.Instance.CurSpeed = 0.0f;
        DragonAniManager.SwicthAnimation("Dragon_MeteoWaiting");
    }

    public override bool Run()
    {
        Vector3 forward;

        if (!_manager.IsTurn)
        {
            Vector3 DragonPos = DragonTransform.position;
            Vector3 PlayerPos = PlayerTransform.position;

            DragonPos.y = 0.0f;
            PlayerPos.y = 0.0f;

            forward = (PlayerPos - DragonPos).normalized;

            float dot = Vector3.Dot(DragonTransform.forward, forward);

            if (CurTurnTime < MaxTurnTime)
            {

                if (dot >= 1.0f)
                    CurTurnTime = MaxTurnTime;

                DragonTransform.rotation = Quaternion.Lerp(
                    DragonTransform.rotation,
                    Quaternion.LookRotation(forward, Vector3.up),
                    CurTurnTime / MaxTurnTime);
                CurTurnTime += Time.deltaTime;
                return false;
            }
            DragonAniManager.SwicthAnimation("Dragon_MeteoAttack");
            _manager.IsTurn = true;
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        BlackBoard.Instance.IsMeteoAttack = false;
    }


}
