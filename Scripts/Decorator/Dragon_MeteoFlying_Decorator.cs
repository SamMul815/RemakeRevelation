using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_MeteoFlying_Decorator : DecoratorTask
{

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {

        float MaxHP = _manager.Stat.MaxHP;
        float HP = _manager.Stat.HP;
        float SaveHP = _manager.Stat.MeteoSaveHP;

        float MeteoHP = _manager.Stat.MeteoHP;

        bool IsMeteo = MeteoHP - (SaveHP - HP) <= 0.0f;

        bool IsFlying = _blackBoard.IsFlying;
        bool IsGround = _blackBoard.IsGround;

        if ((MaxHP > HP && IsMeteo && IsGround && !IsFlying && !_manager.IsAction) || (_manager.IsAction))
        {
            if (_childAction)
            {
                if(!_childAction.IsRunning)
                {
                    if (!_manager.IsAction)
                        OnStart();
                    else if (_manager.IsAction)
                        return true;
                    else if (!_childAction.IsRunning)
                        OnStart();
                }
                else if(_childAction.IsRunning && !_childAction.IsEnd)
                {
                    if (!_manager.IsAction)
                        OnStart();
                    else if (NodeState == TASKSTATE.FAULURE)
                        OnStart();

                    return _childAction.Run();
                }
            }
            else if (NodeState != TASKSTATE.RUNNING)
                    OnStart();

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
