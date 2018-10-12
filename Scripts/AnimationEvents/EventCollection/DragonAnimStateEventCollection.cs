using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class DragonAnimStateEventCollection : BaseAnimStateEventsCollection
{
    protected override void Awake()
    {
        base.Awake();

        
        AddAnimTimeEventFunc(BreathAttackOn, "Breath");

        AddAnimTimeEventFunc(LandingOn, "Landing");
        AddAnimTimeEventFunc(LadingEnd, "Landing");

        AddAnimTimeEventFunc(RightPawAttackOn, "RightPaw");
        AddAnimTimeEventFunc(LeftPawAttackOn, "LeftPaw");

        AddAnimTimeEventFunc(DashAttackOn, "Dash");
        AddAnimTimeEventFunc(RushAttackOn, "Rush");


        AddAnimTimeEventFunc(ShotBreathAttackOn ,"Shot_Breath");
        AddAnimTimeEventFunc(HowlingAttackOn, "Howling");



        AddAnimTimeEventFunc(DescentFlyingOn, "DescentFlying");
        AddAnimTimeEventFunc(MeteoTakeOffOn, "MeteoTakeOff");

        AddAnimTimeEventFunc(MeteoAttackEnd, "MeteoAttack");

        AddAnimTimeEventFunc(DashAttackOff, "Dash");
        AddAnimTimeEventFunc(AttackOff, "Rush");

        AddAnimTimeEventFunc(LeftPawAttackOff, "LeftPaw");
        AddAnimTimeEventFunc(RightPawAttackOff, "RightPaw");

        AddAnimTimeEventFunc(ActionEnd, "Shot_Breath");
        AddAnimTimeEventFunc(ActionEnd, "RightPaw");
        AddAnimTimeEventFunc(ActionEnd, "LeftPaw");
        AddAnimTimeEventFunc(ActionEnd, "Dash");
        AddAnimTimeEventFunc(ActionEnd, "Rush");
        AddAnimTimeEventFunc(ActionEnd, "Howling");
        AddAnimTimeEventFunc(ActionEnd, "Breath");
        AddAnimTimeEventFunc(ActionEnd, "DestroyPart");
        AddAnimTimeEventFunc(ActionEnd, "TakeOff");
        AddAnimTimeEventFunc(ActionEnd, "Landing");
        AddAnimTimeEventFunc(ActionEnd, "Tail");

    }

    private void ActionEnd(EvnData evnData)
    {
        _manager.IsAction = false;
    }     

    private void DashAttackOn(EvnData evnData)
    {
        _blackBoard.IsDashAttackOn = true;
        _manager.AttackOn(DragonAttackTriggers.Dash);

    }

    private void RushAttackOn(EvnData evnData)
    {
        _blackBoard.IsRushAttackOn = true;
        _manager.AttackOn(DragonAttackTriggers.Rush);
    }

    private void AttackOff(EvnData evnData)
    {
        _manager.AttackOff();
    }

    private void DashAttackOff(EvnData evnData)
    {
        _manager.AttackOff();
        BlackBoard.IsDashAttackOn = false;
    }

    
    private void RightPawAttackOn(EvnData evnData)
    {
        _manager.AttackOn(DragonAttackTriggers.RightPaw);
    }

    private void LeftPawAttackOn(EvnData evnData)
    {
        _manager.AttackOn(DragonAttackTriggers.LeftPaw);
    }

    private void RightPawAttackOff(EvnData evnDat)
    {
        _manager.AttackOff();
    }

    private void LeftPawAttackOff(EvnData evnData)
    {
        _manager.AttackOff();
    }


    private void LandingOn(EvnData evnData)
    {

        _manager.FlyingOn = false;
        _manager.LandingOn = true;
        _manager.DragonRigidBody.useGravity = true;
        _manager.DragonGroundCollider.enabled = true;

        Vector3 MoveDir = Vector3.down;
        _manager.DragonRigidBody.AddForce(MoveDir * 500.0f, ForceMode.Impulse);

    }

    private void LadingEnd(EvnData evnData)
    {

    }


    private void ShotBreathAttackOn(EvnData evnData)
    {
        Transform DragonMouth = _blackBoard.DragonMouth;
        BulletManager.Instance.CreateDragonBaseBulletTest(DragonMouth.position, 0.15f, 10);
    }


    private void HowlingAttackOn(EvnData evnData)
    {
        Vector3 pos =
            _manager.transform.position +
            new Vector3(0.0f, 2.0f, 0.0f);

        int Amount = _blackBoard.FanShapeAmount;
        BulletManager.Instance.CreateDragonBaseBullet(pos, Amount);
    }

    private void BreathAttackOn(EvnData evnData)
    {

        Transform DragonMouth = _blackBoard.DragonMouth;

        Vector3 dir =
            (_manager.Player.position -
            _manager.transform.position).normalized;
        dir.y = 0.0f;

        BulletManager.Instance.CreateDragonBreath(DragonMouth.position, dir);

    }
    
    private void DescentFlyingOn(EvnData evnData)
    {
        _manager.FlyingOn = true;
        _manager.DragonRigidBody.useGravity = false;

        _blackBoard.IsGround = false;
        _blackBoard.IsFlying = true;
    }

    private void MeteoTakeOffOn(EvnData evnData)
    {
        _manager.FlyingOn = true;
        _manager.DragonRigidBody.useGravity = false;
        _manager.DragonGroundCollider.enabled = false;
        MovementManager.Instance.SetMovement(MovementType.Meteo);

    }

    private void MeteoAttackEnd(EvnData evnData)
    {
        _manager.IsAction = false;
        _blackBoard.IsLanding = true;
    }

}
