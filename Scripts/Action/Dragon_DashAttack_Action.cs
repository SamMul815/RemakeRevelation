using System.Collections;
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
        Clock.Instance.CurDashCoolingTime = 0.0f;
        forward = (PlayerTransform.position - DragonTransform.position).normalized;

    }

    public override bool Run()
    {

        if (!_manager.IsTurn)
        {
            Vector3 DragonPos = DragonTransform.position;
            Vector3 PlayerPos = PlayerTransform.position;

            DragonPos.y = 0.0f;
            PlayerPos.y = 0.0f;

            float dot = Vector3.Dot(DragonTransform.forward, forward);

            if (dot < 0.99f)
            {

                Vector3 Cross = Vector3.Cross(DragonTransform.forward, forward);

                float Result = Vector3.Dot(Cross, Vector3.up);

                if(Result < 0.0f)
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    if (angle >= 15.0f/*&& angle <= 120.0f*/)
                        DragonAniManager.SwicthAnimation("Dragon_LeftTrun");
                }
                else
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    if (angle >= 15.0f/*&& angle <= 120.0f*/)
                        DragonAniManager.SwicthAnimation("Dragon_RightTrun");

                    //if (angle >= 30.0f && angle <= 120.0f)
                    //{
                    //}
                }

                DragonTransform.rotation = Quaternion.Lerp(
                    DragonTransform.rotation,
                    Quaternion.LookRotation(forward, Vector3.up),
                    CurTurnTime / MaxTurnTime);

                CurTurnTime += Time.deltaTime;

                return false;
            }
            FmodManager.Instance.PlaySoundOneShot(DragonTransform.position, "Dash");
            DragonAniManager.SwicthAnimation("Dragon_Dash");
            _manager.IsTurn = true;
        }

        if (_blackBoard.IsDashAttackOn)
        {
            float Distance = _manager.Stat.DashMoveDistance;
            float DashSpeed = Distance; // *(dashTime - Time.deltaTime);
            forward.y = 0.0f;
            DragonTransform.position += (DragonTransform.forward) * DashSpeed * Time.deltaTime;
        }
        
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
