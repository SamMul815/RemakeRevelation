using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_BulletBreathAttack_Action : ActionTask
{
    public override void OnStart()
    {
        base.OnStart();
        _clock.CurBulletBreathCoolingTime = 0.0f;
    }

    public override bool Run()
    {
        if (!_manager.IsTurn)
        {
            Vector3 DragonPos = Dragon.position;
            Vector3 PlayerPos = Player.position;

            DragonPos.y = 0.0f;
            PlayerPos.y = 0.0f;

            Vector3 forward = (PlayerPos - DragonPos).normalized;


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
            DragonAniManager.SwicthAnimation("Dragon_Bullet_Breath");
            _manager.IsTurn = true;
        }
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
