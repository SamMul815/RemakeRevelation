using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RightPawAttack_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {

        Vector3 toTarget = (PlayerTransform.position - DragonTransform.position).normalized;

        float Dot = Vector3.Dot(DragonTransform.forward, toTarget);

        if(Dot >= Mathf.Cos(Mathf.Deg2Rad * 180.0f * 0.5f))
        {
            Vector3 Cross = Vector3.Cross(DragonTransform.forward, toTarget);

            float Result = Vector3.Dot(Cross, Vector3.up);

            if((Result >= 0.0f && !_manager.IsAction) || _manager.IsAction)
            {
                if (_childAction)
                {
                    if (!_childAction.IsRunning)
                    {
                        if (!_manager.IsAction)
                            OnStart();
                        else if (_manager.IsAction)
                            return true;
                        else if (!_childAction.IsRunning)
                            OnStart();
                    }
                    if(_childAction.IsRunning && !_childAction.IsEnd)
                    {
                        if (!_manager.IsAction)
                            OnStart();
                        else if (!_childAction.IsRunning)
                            OnStart();

                        return _childAction.Run();
                    }

                }
                else
                {
                    if(NodeState != TASKSTATE.RUNNING)
                        OnStart();
                    return ChildNode.Run();
                }
            }
            else if (NodeState == TASKSTATE.RUNNING ||
                ChildNode.NodeState == TASKSTATE.RUNNING)
            {
                OnEnd();
            }
        }
        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
