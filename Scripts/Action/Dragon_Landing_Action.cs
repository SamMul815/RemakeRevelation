using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Landing_Action : ActionTask
{
    public override void OnStart()
    {
        base.OnStart();
        BlackBoard.Instance.IsLanding = true;
        BlackBoard.Instance.IsFlying = false;
        DragonManager.Instance.AttackOff();
        DragonAniManager.SwicthAnimation("Dragon_Landing");

    }

    public override bool Run()
    {

        Debug.Log("Landing");

        DragonManager.Instance.transform.position = Vector3.MoveTowards(
                DragonManager.Instance.transform.position,
                BlackBoard.Instance.FiexdPosition,
                MovementManager.Instance.CurSpeed * Time.deltaTime);

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
