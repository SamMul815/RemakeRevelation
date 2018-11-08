using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_WallCollide_Decoartor : DecoratorTask
{
    int wallLayer;
    bool isWallCollision;
    float limitDistance;
    Transform rayTransform;

    public override void Init()
    {
        base.Init();
        isWallCollision = false;
        limitDistance = _blackBoard.AirSpearLimitDistance;
        rayTransform = _manager.RayTransfrom;
        wallLayer = _manager.DragonAvoidLayers;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        isWallCollision = _blackBoard.IsAirSpearAttack(rayTransform, limitDistance, wallLayer);

        if ((isWallCollision && !_manager.IsAction) || _manager.IsAction)
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
                if (_childAction.IsRunning && !_childAction.IsEnd)
                {
                    if (!_manager.IsAction)
                        OnStart();
                    else if (NodeState == TASKSTATE.FAULURE)
                        OnStart();

                    return _childAction.Run();
                }
            }
            else if(NodeState != TASKSTATE.RUNNING)
            {
                if (!_manager.IsAction)
                    OnStart();
                else if (_manager.IsAction)
                    return true;
            }
            return ChildNode.Run();
        }
        else if(NodeState == TASKSTATE.RUNNING || 
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
