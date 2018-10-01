using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succeeder : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        ChildNode.Run();
        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }


}
