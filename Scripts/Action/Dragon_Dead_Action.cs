using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Dead_Action : ActionTask
 {

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        Debug.Log("Dead");
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}