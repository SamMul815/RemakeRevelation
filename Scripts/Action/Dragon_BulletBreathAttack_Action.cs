using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_BulletBreathAttack_Action : ActionTask
{
    float dot;

    public override void Init()
    {
        base.Init();
        dot = 0.0f;
    }


    public override void OnStart()
    {
        base.OnStart();
        _clock.CurBulletBreathCoolingTime = 0.0f;
        dot = 0.0f;
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
            dot = Vector3.Dot(Dragon.forward, forward);

            if (dot < 0.99f)
            {

                Vector3 Cross = Vector3.Cross(Dragon.forward, forward);
                float Result = Vector3.Dot(Cross, Vector3.up);

                if (Result < 0.0f)
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    if (angle >= 30.0f && angle <= 120.0f)
                        DragonAniManager.SwicthAnimation("Dragon_LeftTrun");
                }
                else
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    if (angle >= 30.0f && angle <= 120.0f)
                        DragonAniManager.SwicthAnimation("Dragon_RightTrun");
                }

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
