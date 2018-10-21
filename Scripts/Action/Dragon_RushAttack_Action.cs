using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RushAttack_Action : ActionTask
{
    private float _moveDistance = 0.0f;
    private float _rushSpeed;
    Vector3 forward;
    float dot;


    public override void Init()
    {
        base.Init();
        dot = 0.0f;
    }

    public override void OnStart()
    {
        base.OnStart();
        _manager.IsAction = true;
        _manager.Stat.DashMovePosition = Player.position;
        _clock.CurDashCoolingTime = 0.0f;
        dot = 0.0f;

    }

    public override bool Run()
    {
        if (!_manager.IsTurn)
        {
            Vector3 DragonPos = Dragon.position;
            Vector3 PlayerPos = Player.position;

            DragonPos.y = 0.0f;
            PlayerPos.y = 0.0f;

            forward = (PlayerPos - DragonPos).normalized;


            dot = Vector3.Dot(Dragon.forward, forward);
            
            if (dot < 0.99f)
            {

                Vector3 Cross = Vector3.Cross(Dragon.forward, forward);
                float Result = Vector3.Dot(Cross, Vector3.up);

                if (Result < 0.0f)
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    //if (angle >= 30.0f && angle <= 120.0f)
                        DragonAniManager.SwicthAnimation("Dragon_LeftTrun");
                }
                else
                {
                    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    //if (angle >= 30.0f && angle <= 120.0f)
                        DragonAniManager.SwicthAnimation("Dragon_RightTrun");
                }

                Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    CurTurnTime / MaxTurnTime);
                CurTurnTime += Time.deltaTime;
                return false;
            }
            _manager.Stat.DashMovePosition = Player.position;
            DragonAniManager.SwicthAnimation("Dragon_Rush");
            EffectManager.Instance.PoolParticleEffectOn("Rush", Dragon);

            _moveDistance = (Dragon.position - Player.position).sqrMagnitude;
            _moveDistance = Mathf.Sqrt(_moveDistance);

            _manager.IsTurn = true;
        }


        if (_blackBoard.IsRushAttackOn)
        {
            float Distance = _manager.Stat.RushMoveLimitDistance;
            _rushSpeed = _moveDistance;

            if (!UtilityManager.DistanceCalc(Dragon.position, _manager.Stat.DashMovePosition, Distance))
            {
                Dragon.position = Vector3.MoveTowards(
                    Dragon.position,
                    _manager.Stat.DashMovePosition,
                    _rushSpeed * Time.deltaTime);
            }

        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _manager.DragonRigidBody.constraints = RigidbodyConstraints.FreezePositionY;
        _manager.DragonRigidBody.freezeRotation = true;
    }

}
