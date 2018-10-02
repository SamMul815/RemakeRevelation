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

        AddAnimTimeEventFunc(RightPawAttackOn, "RightPaw");
        AddAnimTimeEventFunc(RightPawAttckOff, "RightPaw");

        AddAnimTimeEventFunc(LeftPawAttackOn, "LeftPaw");
        AddAnimTimeEventFunc(LeftPawAttackOff, "LeftPaw");

        AddAnimTimeEventFunc(ShotBreathAttackOn ,"Shot_Breath");

        AddAnimTimeEventFunc(HowlingAttackOn, "Howling");

        AddAnimTimeEventFunc(DashAttackOn, "Dash");
        AddAnimTimeEventFunc(DashAttackJump, "Dash");
        AddAnimTimeEventFunc(DashAttackJumpEnd, "Dash");
        AddAnimTimeEventFunc(DashAttackOff, "Dash");

        AddAnimTimeEventFunc(TakeOffOn, "TakeOff");

        AddAnimTimeEventFunc(ActionEnd, "Shot_Breath");
        AddAnimTimeEventFunc(ActionEnd, "RightPaw");
        AddAnimTimeEventFunc(ActionEnd, "LeftPaw");
        AddAnimTimeEventFunc(ActionEnd, "Dash");
        AddAnimTimeEventFunc(ActionEnd, "Howling");
        AddAnimTimeEventFunc(ActionEnd, "Breath");
        AddAnimTimeEventFunc(ActionEnd, "DestroyPart");
        AddAnimTimeEventFunc(ActionEnd, "TakeOff");
        AddAnimTimeEventFunc(ActionEnd, "Landing");

    }

    private void ActionEnd(EvnData evnData)
    {
        DragonManager.IsAction = false;
    }     

    private void DashAttackOn(EvnData evnData)
    {
        BlackBoard.Instance.IsDashAttackOn = true;
        Rigidbody r = DragonManager.Instance.DragonRigidBody;

        Transform Dragon = DragonManager.Instance.transform;
        Vector3 MoveDir = (Dragon.forward + (Vector3.up * 5.5f)).normalized;

        float DashMoveSpeed = 10.0f;

        DragonManager.Instance.AttackOn(DragonAttackTriggers.Dash);
        r.AddForce(Dragon.forward * DashMoveSpeed, ForceMode.Impulse);

    }

    private void DashAttackJump(EvnData evnData)
    {
        Rigidbody r = DragonManager.Instance.DragonRigidBody;
        Transform Dragon = DragonManager.Instance.transform;
        Vector3 MoveDir = (Vector3.up).normalized;

        float DashJumpSpeed = 150.0f;
        r.AddForce(MoveDir * DashJumpSpeed, ForceMode.Impulse);

    }

    private void DashAttackJumpEnd(EvnData evnData)
    {
        Rigidbody r = DragonManager.Instance.DragonRigidBody;
        Transform Dragon = DragonManager.Instance.transform;
        Vector3 MoveDir = (Vector3.down).normalized;

        float DashJumpSpeed = 500.0f;
        r.AddForce(MoveDir * DashJumpSpeed, ForceMode.Impulse);


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

    private void TakeOffOn(EvnData evnData)
    {
        MovementManager.Instance.SetMovement(MovementType.TakeOff);
        DragonManager.Instance.DragonGroundCollider.enabled = false;
        DragonManager.Instance.DragonRigidBody.useGravity = false;
    }

    private void LandingOn(EvnData evnData)
    {
        DragonManager.Landing = true;
        DragonManager.Instance.DragonGroundCollider.enabled = true;
        DragonManager.Instance.DragonRigidBody.useGravity = true;

        Vector3 MoveDir = Vector3.down;
        DragonManager.Instance.DragonRigidBody.AddForce(MoveDir * 500.0f, ForceMode.Impulse);

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

}
