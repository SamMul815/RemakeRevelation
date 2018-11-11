using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RushAttack_Action : ActionTask
{
    float _moveDistance = 0.0f;
    float _rushSpeed;
    float dot;

    Vector3 DragonPos;
    Vector3 PlayerPos;
    Vector3 forward;


    public override void Init()
    {
        base.Init();
        dot = 0.0f;
    }

    public override void OnStart()
    {
        base.OnStart();

        if(_blackBoard.IsAirSpear)
        {
            _manager.Stat.AirSpearSaveHP = _manager.Stat.HP;
            _blackBoard.IsAirSpear = false;
        }
        _manager.IsAction = true;
        _manager.Stat.DashMovePosition = PlayerTransform.position;
        _clock.CurDashCoolingTime = 0.0f;
        dot = 0.0f;

    }

    public override bool Run()
    {
        if (!_manager.IsTurn)
        {
            DragonPos = DragonTransform.position;
            PlayerPos = PlayerTransform.position;

            DragonPos.y = 0.0f;
            PlayerPos.y = 0.0f;

            forward = (PlayerPos - DragonPos).normalized;


            dot = Vector3.Dot(DragonTransform.forward, forward);

            if (CurTurnTime < MaxTurnTime)
            {

                Vector3 Cross = Vector3.Cross(DragonTransform.forward, forward);
                float Result = Vector3.Dot(Cross, Vector3.up);

                if (Result < 0.0f)
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    if (angle >= 15.0f /*&& angle <= 120.0f*/)
                        DragonAniManager.SwicthAnimation("Dragon_LeftTrun");
                }
                else
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    if (angle >= 15.0f /*&& angle <= 120.0f*/)
                        DragonAniManager.SwicthAnimation("Dragon_RightTrun");
                }

                if (dot >= 1.0f)
                    CurTurnTime = MaxTurnTime;

                DragonTransform.rotation = Quaternion.Lerp(
                    DragonTransform.rotation,
                    Quaternion.LookRotation(forward, Vector3.up),
                    CurTurnTime / MaxTurnTime);


                CurTurnTime += Time.deltaTime;
                return false;
            }
            _manager.Stat.DashMovePosition = PlayerPos;
            FmodManager.Instance.PlaySoundAttatch(DragonTransform.gameObject, "Dash");
            DragonAniManager.SwicthAnimation("Dragon_Rush");
            EffectManager.Instance.PoolParticleEffectOn("Rush", DragonTransform);

            _moveDistance = (DragonTransform.position - PlayerTransform.position).sqrMagnitude;
            _moveDistance = Mathf.Sqrt(_moveDistance);

            _manager.IsTurn = true;
        }


        if (_blackBoard.IsRushAttackOn)
        {
            float Distance = _manager.Stat.RushMoveLimitDistance;
            _rushSpeed = _moveDistance;

            if (!UtilityManager.DistanceCalc(DragonTransform.position, _manager.Stat.DashMovePosition, Distance))
            {
                DragonTransform.position = Vector3.MoveTowards(
                    DragonTransform.position,
                    _manager.Stat.DashMovePosition,
                    _rushSpeed * Time.deltaTime);
            }

        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
