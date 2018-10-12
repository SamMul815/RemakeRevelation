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
        DragonManager.Instance.DragonGroundCollider.enabled = true;
    }

    public override bool Run()
    {
        Vector3 forward;

        if (!_manager.IsTurn)
        {
            Vector3 DragonPos = Dragon.position;
            Vector3 PlayerPos = Player.position;

            forward = (PlayerPos - DragonPos).normalized;

            if(Vector3.Dot(Dragon.forward, forward) < 0.99f)
            {
                Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    0.03f);
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
