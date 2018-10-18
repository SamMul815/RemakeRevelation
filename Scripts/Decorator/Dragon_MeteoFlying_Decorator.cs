using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_MeteoFlying_Decorator : DecoratorTask
{

    float MaxHP = 0.0f;
    float HP = 0.0f;
    float SaveHP = 0.0f;
    float MeteoHP = 0.0f;
    bool IsMeteo = false;

    public override void Init()
    {
        base.Init();
        MaxHP = _manager.Stat.MaxHP;
        HP = _manager.Stat.HP;
        SaveHP = _manager.Stat.MeteoSaveHP;
        MeteoHP = _manager.Stat.MeteoHP;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {

        HP = _manager.Stat.HP;
        SaveHP = _manager.Stat.MeteoSaveHP;

        IsMeteo = MeteoHP - (SaveHP - HP) <= 0.0f;

        if ((MaxHP > HP && IsMeteo && _blackBoard.IsGround && !_blackBoard.IsFlying && !_manager.IsAction) || (_manager.IsAction))
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
