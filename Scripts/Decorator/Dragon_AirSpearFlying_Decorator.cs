using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_AirSpearFlying_Decorator : DecoratorTask
{

    float MaxHP;
    float HP;
    float SaveHP;
    float AirSpearHP;

    bool IsAirSpear;
    bool IsFlying;
    bool IsGround;

    public override void Init()
    {
        base.Init();
        MaxHP = _manager.Stat.MaxHP;
        HP = _manager.Stat.HP;
        SaveHP = _manager.Stat.AirSpearSaveHP;
        AirSpearHP = _manager.Stat.AirSpearHP;

        IsFlying = _blackBoard.IsFlying;
        IsGround = _blackBoard.IsGround;
    }

    public override void OnStart()
    {
        base.OnStart();
        _blackBoard.IsAirSpear = true;
    }

    public override bool Run()
    {
        HP = _manager.Stat.HP;
        SaveHP = _manager.Stat.AirSpearSaveHP;

        IsAirSpear = AirSpearHP - (SaveHP - HP) <= 0.0f;

        IsFlying = _blackBoard.IsFlying;
        IsGround = _blackBoard.IsGround;

        if ((MaxHP > HP && IsAirSpear && IsGround && !IsFlying && !_manager.IsAction) || _manager.IsAction)
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

                    return ChildNode.Run();
                }
            }
            else if (NodeState != TASKSTATE.RUNNING)
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
