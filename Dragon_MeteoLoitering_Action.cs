using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_MeteoLoitering_Action : ActionTask
{

    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Dragon_Flying");
        _movement.SetMovement(MovementType.MeteoFlying);
    }

    public override bool Run()
    {
        if (_movement.CurrentNodeManager().IsMoveEnd)
        {
            _manager.IsAction = false;
            _blackBoard.IsLanding = true;
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
