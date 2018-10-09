using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Landing_Action : ActionTask
{
    Vector3 FiexdPos = new Vector3();   

    public override void OnStart()
    {
        base.OnStart();
        BlackBoard.Instance.IsFlying = false;
        DragonManager.Instance.AttackOff();

        DragonAniManager.SwicthAnimation("Dragon_Gliding");
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

        bool IsLandingAttackFiexdDistance =
            UtilityManager.DistanceCalc(DragonManager.Instance.transform, Player, Distance);

        if(!IsLandingAttackFiexdDistance && !BlackBoard.Instance.IsFiexdPosition)
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
            BlackBoard.Instance.FiexdPosition = Player.position + new Vector3(0.0f, -10.0f, 0.0f);
            BlackBoard.Instance.IsFiexdPosition = true;
            FiexdPos = BlackBoard.Instance.FiexdPosition;
        }

        float LandingDistance = BlackBoard.Instance.LandingDistance;

        if (!UtilityManager.DistanceCalc(
            DragonManager.Instance.transform.position, 
            BlackBoard.Instance.FiexdPosition, LandingDistance))
        {
            forward = (BlackBoard.Instance.FiexdPosition - DragonManager.Instance.transform.position).normalized;

            DragonManager.Instance.transform.position =
                Vector3.MoveTowards(
                    DragonManager.Instance.transform.position,
                    BlackBoard.Instance.FiexdPosition,
                    MovementManager.Instance.CurSpeed * Time.deltaTime);

            DragonManager.Instance.transform.rotation = Quaternion.Slerp(
                DragonManager.Instance.transform.rotation,
                Quaternion.LookRotation(forward, Vector3.up),
                0.1f);

            return false;
        }

        DragonAniManager.SwicthAnimation("Dragon_Landing");

        DragonManager.Instance.transform.position = Vector3.MoveTowards(
            DragonManager.Instance.transform.position,
            BlackBoard.Instance.FiexdPosition,
            MovementManager.Instance.CurSpeed * Time.deltaTime);

        Vector3 DragonPos = DragonManager.Instance.transform.position;

        if(UtilityManager.DistanceCalc(DragonPos, FiexdPos, 0.0f))
        {
            BlackBoard.Instance.FiexdPosition = FiexdPos + new Vector3(0.0f, 10.0f, 0.0f);
            DragonManager.Instance.transform.position = FiexdPos + new Vector3(0.0f, 10.0f, 0.0f);
        }

        if (DragonManager.LandingOn)
        {
            forward = (DragonManager.Player.position - DragonManager.Instance.transform.position).normalized;
            forward.y = 0.0f;

            DragonManager.Instance.transform.rotation =
                Quaternion.Slerp(DragonManager.Instance.transform.rotation,
                Quaternion.LookRotation(forward),
                0.05f);
        }

        //DragonManager.Instance.transform.position = Vector3.MoveTowards(
        //        DragonManager.Instance.transform.position,
        //        BlackBoard.Instance.FiexdPosition,
        //        MovementManager.Instance.CurSpeed * Time.deltaTime);

        //Vector3 DragonPos = DragonManager.Instance.transform.position;

        //if (UtilityManager.DistanceCalc(DragonPos, FiexdPos, 0.0f))
        //{
        //    BlackBoard.Instance.FiexdPosition = FiexdPos + new Vector3(0.0f, 10.0f, 0.0f);
        //    DragonManager.Instance.transform.position = FiexdPos + new Vector3(0.0f, 10.0f, 0.0f);
        //}

        //if (DragonManager.LandingOn)
        //{
        //    forward = (DragonManager.Player.position - 
        //        DragonManager.Instance.transform.position).normalized;
        //    forward.y = 0.0f;

        //    DragonManager.Instance.transform.rotation =
        //        Quaternion.Slerp(
        //            DragonManager.Instance.transform.rotation,
        //            Quaternion.LookRotation(forward),
        //            0.05f);
        //}
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        BlackBoard.Instance.IsFiexdPosition = false;
        BlackBoard.Instance.IsLanding = false;
        BlackBoard.Instance.IsGround = true;
    }

}
