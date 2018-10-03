using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DescentAttack_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        MovementManager.Instance.CurSpeed = 0.0f;
        DragonAniManager.SwicthAnimation("Dragon_DescentAttack");
        DragonManager.Instance.AttackOn(DragonAttackTriggers.AirSpear);
    }

    public override bool Run()
    {

        float Distance = BlackBoard.Instance.DescentAttackFiexdDistance;
        Transform Player = DragonManager.Player;

        float MaxSpeed = DragonManager.Instance.Stat.MaxSpeed;
        float AccSpeed = DragonManager.Instance.Stat.AccSpeed;

        Vector3 forward = (Player.position - DragonManager.Instance.transform.position).normalized;

        MovementManager.Instance.CurSpeed = 
            BlackBoard.Instance.Acceleration(MovementManager.Instance.CurSpeed, MaxSpeed, AccSpeed);

        bool IsDescentAttackFiexdDistance = 
            UtilityManager.DistanceCalc(DragonManager.Instance.transform, Player, Distance);

        if (!IsDescentAttackFiexdDistance && !BlackBoard.Instance.IsFiexdPosition)
        {
            DragonManager.Instance.transform.position = Vector3.MoveTowards(
                    DragonManager.Instance.transform.position,
                    Player.position,
                    MovementManager.Instance.CurSpeed * Time.deltaTime);

            DragonManager.Instance.transform.rotation = Quaternion.Slerp(
                DragonManager.Instance.transform.rotation,
                Quaternion.LookRotation(forward, Vector3.up),
                0.1f);

            return false;
        }

        if (!BlackBoard.Instance.IsFiexdPosition)
        {
            BlackBoard.Instance.FiexdPosition = Player.position;
            BlackBoard.Instance.IsFiexdPosition = true;
        }

        forward = (BlackBoard.Instance.FiexdPosition - DragonManager.Instance.transform.position).normalized;

        DragonManager.Instance.transform.position = Vector3.MoveTowards(
            DragonManager.Instance.transform.position,
            BlackBoard.Instance.FiexdPosition,
            MovementManager.Instance.CurSpeed * Time.deltaTime);

        DragonManager.Instance.transform.rotation = Quaternion.Slerp(
            DragonManager.Instance.transform.rotation,
            Quaternion.LookRotation(forward, Vector3.up),
            0.1f);

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
