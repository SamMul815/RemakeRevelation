using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_TailAttack_Action : ActionTask
{
    Vector3 forward;
    float maxTurn;
    float Angle;
    float SumAngle;

    public override void Init()
    {
        base.Init();
        maxTurn = 180.0f;
        Angle = 0.0f;
        SumAngle = 0.0f; 
    }

    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Dragon_Tail");
        _clock.CurPawCoolingTime = 0.0f;
        forward = (Player.position - Dragon.position).normalized;
        Angle = 0.0f;
        SumAngle = 0.0f;
    }

    public override bool Run()
    {
        float Turn = 0.0f;
        if (_blackBoard.IsTailAttackOn)
        {
            if (!_manager.IsTurn)
            {
                if (Angle <= 0.0f)
                {
                    Turn = 0.0f;
                }
                else
                {
                    Turn = maxTurn - Angle - (maxTurn * (1.0f - CurTurnTime));
                    Angle = Turn;
                }
                Turn *= 2.0f;
                SumAngle += Turn + Angle;
                Angle = maxTurn - (maxTurn * (1.0f - CurTurnTime));
                CurTurnTime += Time.deltaTime;

                Dragon.Rotate(Dragon.up, Turn, Space.World);

                Debug.Log(Turn);

                if (SumAngle >= 180.0f)
                {
                    _manager.IsTurn = true;
                    SumAngle = 180.0f;
                }
                return false;
            }
        }
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _blackBoard.IsTailAttackOn = false;
    }

}
