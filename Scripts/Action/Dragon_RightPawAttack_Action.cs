using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RightPawAttack_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        _clock.CurPawCoolingTime = 0.0f;
    }

    public override bool Run()
    {
        Vector3 DragonPos = Dragon.position;
        Vector3 PlayerPos = Player.position;

        DragonPos.y = 0.0f;
        PlayerPos.y = 0.0f;

        Vector3 forward = (PlayerPos - DragonPos).normalized;

        if (!_manager.IsTurn)
        {
            if (Vector3.Dot(Dragon.forward, forward) < 0.99f)
            {
                //DragonAniManager.SwicthAnimation("LeftTrun");
                Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    0.05f);
                return false;
            }

            DragonAniManager.SwicthAnimation("Dragon_RightPaw");
            EffectManager.Instance.PoolParticleEffectOn("RightPaw", Dragon);
            _manager.IsTurn = true;
        }
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }


}
