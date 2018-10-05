using System.Collections;
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
        Transform Dragon = DragonManager.Instance.transform;
        Transform Player = DragonManager.Player;
        Vector3 forward;

        if (!DragonManager.IsTurn)
        {
            Vector3 DragonPos = Dragon.position;
            Vector3 PlayerPos = DragonManager.Player.position;

            DragonPos.y = 0.0f;
            PlayerPos.y = 0.0f;

            forward = (PlayerPos - DragonPos).normalized;

            if(Vector3.Dot(Dragon.forward, forward) < 0.99f)
            {
                Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    0.05f);
                return false;
            }
            DragonAniManager.SwicthAnimation("Dragon_MeteoAttack");
            DragonManager.IsTurn = true;
        }

        Debug.Log("MeteoAttack");

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        BlackBoard.Instance.IsMeteoAttack = false;
    }


}
