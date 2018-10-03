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
        BlackBoard.Instance.IsLanding = true;
        BlackBoard.Instance.IsFlying = false;
        DragonManager.Instance.AttackOff();
        DragonAniManager.SwicthAnimation("Dragon_Landing");

        FiexdPos = BlackBoard.Instance.FiexdPosition;
    }

    public override bool Run()
    {

        DragonManager.Instance.transform.position = Vector3.MoveTowards(
                DragonManager.Instance.transform.position,
                BlackBoard.Instance.FiexdPosition,
                MovementManager.Instance.CurSpeed * Time.deltaTime);

        Vector3 DragonPos = DragonManager.Instance.transform.position;

        if (UtilityManager.DistanceCalc(DragonPos, FiexdPos, 0.0f))
        {
            BlackBoard.Instance.FiexdPosition = FiexdPos + new Vector3(0.0f, 10.0f, 0.0f);
            DragonManager.Instance.transform.position = FiexdPos + new Vector3(0.0f, 10.0f, 0.0f);
        }

        if (DragonManager.Landing)
        {
            Vector3 forward = (DragonManager.Player.position - DragonManager.Instance.transform.position).normalized;

            forward = (forward).normalized;
            forward.y = 0.0f;

            DragonManager.Instance.transform.rotation =
                Quaternion.Slerp(
                    DragonManager.Instance.transform.rotation,
                    Quaternion.LookRotation(forward),
                    0.05f);
        }
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
