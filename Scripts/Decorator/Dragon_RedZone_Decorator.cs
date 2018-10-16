using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_RedZone_Decorator : DecoratorTask
{
    float redZoneDistance = 0.0f;

    bool isRedZone;

    public override void Init()
    {
        base.Init();
        redZoneDistance = _blackBoard.RedZoneDistance;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        isRedZone = false;//UtilityManager.DistanceCalc(Dragon, Player, redZoneDistance);

        if((isRedZone && !_manager.IsAction) || _manager.IsAction)
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
                    else if (NodeState == TASKSTATE.FAULURE)
                        OnStart();
                    return _childAction.Run();
                }
            }
            else
            {
                if (NodeState != TASKSTATE.RUNNING)
                    OnStart();
            }
            return ChildNode.Run();
        }
        else if (NodeState == TASKSTATE.RUNNING ||
            ChildNode.NodeState == TASKSTATE.RUNNING)
        {
            OnEnd();
        }
        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}

