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
        AddAnimTimeEventFunc(RightPawAttckOff, "RightPaw");

        AddAnimTimeEventFunc(LeftPawAttackOn, "LeftPaw");
        AddAnimTimeEventFunc(LeftPawAttackOff, "LeftPaw");

        AddAnimTimeEventFunc(ShotBreathAttackOn ,"Shot_Breath");

        AddAnimTimeEventFunc(HowlingAttackOn, "Howling");

        AddAnimTimeEventFunc(DashAttackOn, "Dash");
        AddAnimTimeEventFunc(DashAttackOff, "Dash");

        AddAnimTimeEventFunc(RushAttackOn, "Rush");
        AddAnimTimeEventFunc(RushAttackOff, "Rush");

        AddAnimTimeEventFunc(DescentFlyingOn, "DescentFlying");

        AddAnimTimeEventFunc(MeteoTakeOffOn, "MeteoTakeOff");

        AddAnimTimeEventFunc(MeteoAttackEnd, "MeteoAttack");





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
        DragonManager.IsAction = false;
    }     

    private void DashAttackOn(EvnData evnData)
    {
        BlackBoard.Instance.IsDashAttackOn = true;
        DragonManager.Instance.AttackOn(DragonAttackTriggers.Dash);

    }

    private void RushAttackOn(EvnData evnData)
    {
        BlackBoard.Instance.IsRushAttackOn = true;
        DragonManager.Instance.AttackOn(DragonAttackTriggers.Dash);
    }

    private void RushAttackOff(EvnData evnData)
    {
        DragonManager.Instance.AttackOff();
    }


    private void DashAttackOff(EvnData evnData)
    {
        DragonManager.Instance.AttackOff();
    }
    
    private void RightPawAttackOn(EvnData evnData)
    {
        DragonManager.Instance.AttackOn(DragonAttackTriggers.RightPaw);
    }

    private void RightPawAttckOff(EvnData evnData)
    {
        DragonManager.Instance.AttackOff();
    }

    private void LeftPawAttackOn(EvnData evnData)
    {
        DragonManager.Instance.AttackOn(DragonAttackTriggers.LeftPaw);
    }

    private void LeftPawAttackOff(EvnData evnData)
    {
        DragonManager.Instance.AttackOff();
    }


    private void LandingOn(EvnData evnData)
    {

        DragonManager.FlyingOn = false;
        DragonManager.LandingOn = true;
        DragonManager.Instance.DragonRigidBody.useGravity = true;
        //DragonManager.Instance.DragonGroundCollider.enabled = true;

        Vector3 MoveDir = Vector3.down;
        DragonManager.Instance.DragonRigidBody.AddForce(MoveDir * 500.0f, ForceMode.Impulse);

    }

    private void LadingEnd(EvnData evnData)
    {
        DragonManager.Instance.DragonGroundCollider.enabled = true;
    }


    private void ShotBreathAttackOn(EvnData evnData)
    {
        Transform DragonMouth = BlackBoard.Instance.DragonMouth;
        BulletManager.Instance.CreateDragonBaseBulletTest(DragonMouth.position, 0.15f, 15);
    }


    private void HowlingAttackOn(EvnData evnData)
    {
        Vector3 pos =
            DragonManager.Instance.transform.position +
            new Vector3(0.0f, 2.0f, 0.0f);

        int Amount = BlackBoard.Instance.FanShapeAmount;
        BulletManager.Instance.CreateDragonBaseBullet(pos, Amount);
    }


    private void BreathAttackOn(EvnData evnData)
    {

        Transform DragonMouth = BlackBoard.Instance.DragonMouth;

        Vector3 dir =
            (DragonManager.Player.position -
            DragonManager.Instance.transform.position).normalized;
        dir.y = 0.0f;

        BulletManager.Instance.CreateDragonBreath(DragonMouth.position, dir);

    }


    private void DescentFlyingOn(EvnData evnData)
    {
        DragonManager.FlyingOn = true;
        BlackBoard.Instance.IsGround = false;
        BlackBoard.Instance.IsFlying = true;
        DragonManager.Instance.DragonRigidBody.useGravity = false;
        DragonManager.Instance.DragonGroundCollider.enabled = false;
    }

    private void MeteoTakeOffOn(EvnData evnData)
    {
        DragonManager.FlyingOn = true;
        MovementManager.Instance.SetMovement(MovementType.Meteo);
        DragonManager.Instance.DragonGroundCollider.enabled = false;
        DragonManager.Instance.DragonRigidBody.useGravity = false;

    }

    private void MeteoAttackEnd(EvnData evnData)
    {
        DragonManager.IsAction = false;
        BlackBoard.Instance.IsLanding = true;
    }

}
