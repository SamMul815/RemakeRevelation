using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_TailAttack_Action : ActionTask
{
    Vector3 forward = Vector3.zero;
    float turnTime = 0.13f;
    float curturnTime = 0.0f;

    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Dragon_Tail");
        _clock.CurPawCoolingTime = 0.0f;
        curturnTime = 0.0f;
        forward = (Player.position - Dragon.position).normalized;
    }

    public override bool Run()
    {
        //if (curturnTime < turnTime)
        //{
        //    Dragon.rotation = Quaternion.Slerp(
        //        Dragon.rotation,
        //        Quaternion.LookRotation(forward),
        //        curturnTime / turnTime);

        //    curturnTime += Time.deltaTime;
        //}

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
