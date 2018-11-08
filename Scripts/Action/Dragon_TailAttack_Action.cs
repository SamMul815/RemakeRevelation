using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_TailAttack_Action : ActionTask
{
    float maxTurn;
    float Angle;

    public override void Init()
    {
        base.Init();
        maxTurn = 180.0f;
        Angle = 0.0f;
    }

    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Dragon_Tail");
        _clock.CurPawCoolingTime = 0.0f;
        Angle = 0.0f;
    }

    public override bool Run()
    {
        float Turn = 0.0f;
        if (_blackBoard.IsTailAttackOn)
        {
            if (Angle <= 0.0f) Turn = 0.0f;

            else
            {
                Turn = maxTurn - Angle - (maxTurn * (1.0f - CurTurnTime));
                Angle = Turn;
            }

            Turn *= 2.0f;
            Angle = maxTurn - (maxTurn * (1.0f - CurTurnTime));
            CurTurnTime += Time.deltaTime;

            DragonTransform.Rotate(DragonTransform.up, Turn, Space.World);
                
            return false;
        }
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _blackBoard.IsTailAttackOn = false;
    }

}
