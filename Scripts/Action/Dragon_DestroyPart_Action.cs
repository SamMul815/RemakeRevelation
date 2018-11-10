using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DestroyPart_Action : ActionTask
{
    public override void OnStart()
    {
        base.OnStart();
        _manager.AttackOff();
        FmodManager.Instance.PlaySoundOneShot(DragonTransform.position, "Painful");
        DragonAniManager.SwicthAnimation("Dragon_DestroyPart");
    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _blackBoard.IsDestroyPart = false;
    }

}
