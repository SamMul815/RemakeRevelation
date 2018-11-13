using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Dead_Action : ActionTask
 {
    public override void Init()
    {
        base.Init();
    }

    public override void OnStart()
    {
        base.OnStart();
        FmodManager.Instance.PlaySoundOneShot(DragonTransform.position, "Dying");
        DragonAniManager.SwicthAnimation("Dragon_Dead");
    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}