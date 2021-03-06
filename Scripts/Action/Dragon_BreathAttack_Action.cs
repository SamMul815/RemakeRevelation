﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_BreathAttack_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        Clock.Instance.CurBreathCoolingTime = 0.0f;

        Transform DragonMouth = BlackBoard.Instance.DragonMouth;

        Vector3 dir =
            (DragonManager.Player.position -
            DragonManager.Instance.transform.position).normalized;

        BulletManager.Instance.CreateDragonBreath(DragonMouth.position, dir);

        DragonAniManager.SwicthAnimation("Dragon_Breath");

    }

    public override bool Run()
    {
        if (!DragonManager.IsTurn)
        {
            Transform Dragon = DragonManager.Instance.transform;
            Transform Player = DragonManager.Player;


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

            DragonAniManager.SwicthAnimation("Dragon_Breath");
            DragonManager.IsTurn = true;
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }


}
