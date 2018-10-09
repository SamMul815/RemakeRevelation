using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RushAttack_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        BlackBoard.Instance.IsDashAttackOn = false;
        BlackBoard.Instance.IsRushAttackOn = false;
        DragonManager.Instance.Stat.DashMovePosition = DragonManager.Player.transform.position;
        Clock.Instance.CurDashCoolingTime = 0.0f;
        DragonManager.IsAction = true;

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
                //DragonAniManager.SwicthAnimation("LeftTrun");
                Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    0.05f);
                return false;
            }
            DragonAniManager.SwicthAnimation("Dragon_Rush");
            DragonManager.IsTurn = true;
        }


        if (BlackBoard.Instance.IsRushAttackOn)
        {
            Transform Dragon = DragonManager.Instance.transform;
            Transform Player = DragonManager.Player;

            float Distance = DragonManager.Instance.Stat.RushMoveLimitDistance;

            float RushSpeed = DragonManager.Instance.Stat.RushSpeed;

            if (!UtilityManager.DistanceCalc(Dragon.position, DragonManager.Instance.Stat.DashMovePosition, Distance))
            {
                Dragon.position = Vector3.MoveTowards(
                    Dragon.position,
                    DragonManager.Instance.Stat.DashMovePosition,
                    RushSpeed * Time.deltaTime);
            }

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
