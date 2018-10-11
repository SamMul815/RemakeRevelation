using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DashAttack_Action : ActionTask
{

    Transform Dragon;
    Transform Player;
    float fStopTime;
    public override void OnStart()
    {
        BlackBoard.Instance.IsDashAttackOn = false;
        BlackBoard.Instance.IsRushAttackOn = false;
        DragonManager.Instance.Stat.DashMovePosition = DragonManager.Player.transform.position;
        Clock.Instance.CurDashCoolingTime = 0.0f;

        Dragon = DragonManager.Instance.transform;
        Player = DragonManager.Player;
        fStopTime = 0.0f;

        base.OnStart();
    }

    public override bool Run()
    {
        if (!DragonManager.IsTurn)
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
            DragonAniManager.SwicthAnimation("Dragon_Dash");
            DragonManager.IsTurn = true;
        }

        if (BlackBoard.Instance.IsDashAttackOn)
        {
            float Distance = DragonManager.Instance.Stat.DashMoveDistance;

            //float DashSpeed = Distance * (1.0f - Time.deltaTime);

            //Vector3 forward = (DragonManager.Instance.Stat.DashMovePosition - Dragon.position).normalized;

            if(fStopTime < 1.5f)
            {
                Vector3 forward = (Player.position - Dragon.position).normalized;
                Dragon.position += forward * Distance * Time.deltaTime;
                fStopTime += Time.deltaTime;
            }
            //Debug.Log(fStopTime);

            //Dragon.Translate(forward * DashSpeed * Time.deltaTime);
        }
        
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        BlackBoard.Instance.IsDashAttackOn = false;
        BlackBoard.Instance.IsRushAttackOn = false;
    }

}
