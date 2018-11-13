using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_FallingDead_Action : ActionTask
{
    Vector3 forward;
    Transform rayTransform;

    float maxSpeed;
    float accSpeed;

    float fallingDistance;

    bool isFallingDead;

    public override void Init()
    {
        base.Init();
        maxSpeed = _manager.Stat.MaxSpeed;
        accSpeed = _manager.Stat.AccSpeed;
        isFallingDead = false;

        fallingDistance = _blackBoard.FallingDistance;

        rayTransform = _manager.RayTransfrom;
    }

    public override void OnStart()
    {
        base.OnStart();
        _movement.StopMovement();
        _blackBoard.IsFlying = false;
        isFallingDead = false;
        DragonAniManager.SwicthAnimation("Dragon_Falling");
        _manager.DragonRigidBody.useGravity = true;
        _manager.DragonGroundCollider.enabled = true;
        _manager.AttackOff();
        _movement.CurSpeed += 20.0f;
    }

    public override bool Run()
    {
       if (!isFallingDead)
        {
            isFallingDead =
                _blackBoard.LandingRayHit(rayTransform, -rayTransform.up, fallingDistance, _manager.DragonAvoidLayers);

            _movement.CurSpeed = _blackBoard.Acceleration(_movement.CurSpeed, maxSpeed, accSpeed);
            DragonTransform.position += (-DragonTransform.up) * _movement.CurSpeed * Time.deltaTime;
            DragonTransform.rotation = Quaternion.identity;

            if (isFallingDead)
            {
                DragonTransform.rotation = Quaternion.identity;
                EffectManager.Instance.PoolParticleEffectOn("Landing", DragonTransform.position, DragonTransform.forward);
                DragonAniManager.SwicthAnimation("Dragon_Dead");
            }
        }

        if (isFallingDead)
        {
            DragonTransform.rotation = Quaternion.identity;
            EffectManager.Instance.PoolParticleEffectOn("Landing", DragonTransform.position, DragonTransform.forward);
            DragonAniManager.SwicthAnimation("Dragon_Dead");
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
