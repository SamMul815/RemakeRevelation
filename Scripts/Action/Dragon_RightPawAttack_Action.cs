using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RightPawAttack_Action : ActionTask
{
    public override void Init()
    {
        base.Init();
    }

    public override void OnStart()
    {
        base.OnStart();
        _clock.CurPawCoolingTime = 0.0f;
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

            float dot = Vector3.Dot(Dragon.forward, forward);

            if (dot < 0.99f)
            {

                Vector3 Cross = Vector3.Cross(Dragon.forward, forward);
                float Result = Vector3.Dot(Cross, Vector3.up);

                if (Result < 0.0f)
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    //if (angle >= 30.0f && angle <= 120.0f)
                    DragonAniManager.SwicthAnimation("Dragon_LeftTrun");
                }
                else
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    //if (angle >= 30.0f && angle <= 120.0f)
                    DragonAniManager.SwicthAnimation("Dragon_RightTrun");
                }
                return false;
            }

            DragonAniManager.SwicthAnimation("Dragon_RightPaw");
            Vector3 Pos = Dragon.position;
            EffectManager.Instance.PoolParticleEffectOn("RightPaw", Dragon.position, Dragon.forward);
            _manager.IsTurn = true;
        }
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }


}
