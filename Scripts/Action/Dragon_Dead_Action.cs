using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Dead_Action : ActionTask
 {
    bool isCrateUI;

    public override void Init()
    {
        base.Init();
        isCrateUI = false;
    }

    public override void OnStart()
    {
        base.OnStart();
        FmodManager.Instance.PlaySoundOneShot(DragonTransform.position, "Dying");
        DragonAniManager.SwicthAnimation("Dragon_Dead");
    }

    public override bool Run()
    {
        if (!isCrateUI)
        {
            Vector3 pos = DragonTransform.position + new Vector3(0.0f, 3.0f, 0.0f);
            UIManager.Instance.CreateGameClear(pos, -DragonTransform.forward, out isCrateUI);
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}