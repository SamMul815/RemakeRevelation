using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Landing_Action : ActionTask
{
    Vector3 forward;
    Transform rayTransfrom;

    float maxSpeed;
    float accSpeed;
    float distance;

    float landingDistance;
    bool isLandingAttackFiexdDistance;

    public override void Init()
    {
        base.Init();
        maxSpeed = _manager.Stat.MaxSpeed;
        accSpeed = _manager.Stat.AccSpeed;

        distance = _blackBoard.DescentAttackFiexdDistance;
        landingDistance = _blackBoard.LandingDistance;

        isLandingAttackFiexdDistance = false;
        rayTransfrom = _manager.RayTransfrom;
    }

    public override void OnStart()
    {
        base.OnStart();
        _blackBoard.IsFlying = false;
        DragonAniManager.SwicthAnimation("Dragon_Gliding");
        _manager.AttackOn(DragonAttackTriggers.AirSpear);
        landingDistance = _blackBoard.LandingDistance;
        _movement.CurSpeed += 20.0f;
    }

    public override bool Run()
    {

        isLandingAttackFiexdDistance =
            UtilityManager.DistanceCalc(DragonTransform, PlayerTransform, distance);

        if (!isLandingAttackFiexdDistance && !_blackBoard.IsFiexdPosition)
        {
            _movement.CurSpeed =
               _blackBoard.Acceleration(_movement.CurSpeed, maxSpeed, accSpeed);

            forward = (PlayerTransform.position - DragonTransform.position).normalized;

            DragonTransform.position = Vector3.MoveTowards(
                DragonTransform.position,
                PlayerTransform.position,
                _movement.CurSpeed * Time.deltaTime);

            if (CurTurnTime < MaxTurnTime)
            {
                DragonTransform.rotation = Quaternion.RotateTowards(
                    DragonTransform.rotation,
                    Quaternion.LookRotation(forward, Vector3.up),
                    45.0f * Time.deltaTime);
            }
            return false;
        }

        if (!_blackBoard.IsFiexdPosition)
        {
            _blackBoard.FiexdPosition = PlayerTransform.position;
            _blackBoard.IsFiexdPosition = true;
        }

        forward = (_blackBoard.FiexdPosition - DragonTransform.position);

        if (!_manager.LandingOn)
        {
            _manager.LandingOn =
                _blackBoard.LandingRayHit(rayTransfrom,
                rayTransfrom.forward, landingDistance, _manager.DragonAvoidLayers);

            if (_manager.LandingOn)
            {
                CurTurnTime = 0.0f;
                _movement.CurSpeed = 40.0f;
                _manager.DragonRigidBody.useGravity = true;
                DragonAniManager.SwicthAnimation("Dragon_Landing");
            }

        }
        else
        {

            forward.x = 0.0f;
            forward.y = 0.0f;
        }

        if (forward == Vector3.zero && !_manager.IsTurn)
            _manager.IsTurn = true;

        DragonTransform.position = Vector3.MoveTowards(
            DragonTransform.position,
            _blackBoard.FiexdPosition,
            _movement.CurSpeed * Time.deltaTime);

        if (!_manager.IsTurn)
        {
            DragonTransform.rotation =
                Quaternion.Lerp(
                    DragonTransform.rotation,
                    Quaternion.LookRotation(forward, Vector3.up),
                    CurTurnTime / MaxTurnTime);

            CurTurnTime += Time.deltaTime;
        }

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
        _blackBoard.IsFiexdPosition = false;
        _blackBoard.IsLanding = false;
        _blackBoard.IsGround = true;
        _manager.LandingOn = false;
        _manager.FlyingOn = false;
    }

}
