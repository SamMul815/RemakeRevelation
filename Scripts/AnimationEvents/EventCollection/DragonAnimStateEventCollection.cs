using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class DragonAnimStateEventCollection : BaseAnimStateEventsCollection
{
    protected override void Awake()
    {
        base.Awake();

        AddAnimEnterEventFunc(TrunStart, "RightTrun");
        AddAnimEnterEventFunc(TrunStart, "LeftTrun");



        AddAnimTimeEventFunc(FirstWalk, "Walk");
        AddAnimTimeEventFunc(LastWalk, "Walk");

        AddAnimTimeEventFunc(BreathAttackOn, "Breath");
        AddAnimTimeEventFunc(ActionEnd, "Breath");

        AddAnimTimeEventFunc(LandingOn, "Landing");
        AddAnimTimeEventFunc(LandingAttackOff, "Landing");
        AddAnimTimeEventFunc(ActionEnd, "Landing");


        AddAnimTimeEventFunc(RightPawAttackOn, "RightPaw");
        AddAnimTimeEventFunc(PawAttackOff, "RightPaw");
        AddAnimTimeEventFunc(ActionEnd, "RightPaw");

        AddAnimTimeEventFunc(LeftPawAttackOn, "LeftPaw");
        AddAnimTimeEventFunc(PawAttackOff, "LeftPaw");
        AddAnimTimeEventFunc(ActionEnd, "LeftPaw");


        AddAnimTimeEventFunc(DashAttackOn, "Dash");
        AddAnimTimeEventFunc(DashFirstRightPaw, "Dash");
        AddAnimTimeEventFunc(DashLeftAttack, "Dash");
        AddAnimTimeEventFunc(DashLastRightPaw, "Dash");
        AddAnimTimeEventFunc(ActionEnd, "Dash");


        AddAnimTimeEventFunc(RushAttackOn, "Rush");
        AddAnimTimeEventFunc(RushAttackOff, "Rush");
        AddAnimTimeEventFunc(ActionEnd, "Rush");

        AddAnimTimeEventFunc(TailAttackOn, "Tail");
        AddAnimTimeEventFunc(TailAttackOff, "Tail");
        AddAnimTimeEventFunc(ActionEnd, "Tail");


        AddAnimTimeEventFunc(ShotBreathAttackOn, "Shot_Breath");
        AddAnimTimeEventFunc(ActionEnd, "Shot_Breath");

        AddAnimTimeEventFunc(HowlingAttackOn, "Howling");
        AddAnimTimeEventFunc(ActionEnd, "Howling");

        AddAnimTimeEventFunc(DescentFlyingStart, "AirSpearTakeOff");

        AddAnimTimeEventFunc(MeteoFlyingStart, "MeteoTakeOff");

        AddAnimTimeEventFunc(MeteoAttackSoundOn, "MeteoAttack");
        AddAnimTimeEventFunc(MeteoAttackOn, "MeteoAttack");
        AddAnimTimeEventFunc(MeteoAttackEnd, "MeteoAttack");

        AddAnimTimeEventFunc(ActionEnd, "DestroyPart");
        
        AddAnimTimeEventFunc(ActionEnd, "TakeOff");

        AddAnimTimeEventFunc(ActionEnd, "NearHowling");

        AddAnimTimeEventFunc(FlyingSoundOn, "Flying");

        AddAnimTimeEventFunc(HoveringSoundOn, "Hovering");
        AddAnimTimeEventFunc(HoveringSoundOn, "MeteoWaiting");

        
        AddAnimTimeEventFunc(DeadEnd, "Dead");
        AddAnimTimeEventFunc(FallingDead, "FallingDead");
        AddAnimTimeEventFunc(DeadEnd, "FallingDead");

    }

    private void FirstWalk (EvnData evnData)
    {
        //Debug.Log("test");
        FmodManager.Instance.PlaySoundOneShot(_manager.transform.position, "Walk");
    }

    private void LastWalk (EvnData evnData)
    {
        FmodManager.Instance.PlaySoundOneShot(_manager.transform.position, "Walk");
    }

    private void TrunStart(EvnData evnData)
    {
        FmodManager.Instance.PlaySoundOneShot(_manager.transform.position, "Turn");
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

    private void AttackOff(EvnData evnData)
    {
        _manager.AttackOff();
    }

    private void PawAttackOff (EvnData evnData)
    {
        FmodManager.Instance.PlaySoundOneShot(_manager.transform.position, "Paw_Effect");
        _manager.AttackOff();
    }

    private void DashFirstRightPaw(EvnData evnData)
    {
        Vector3 Pos = _manager.RightPawTransform.position;
        FmodManager.Instance.PlaySoundOneShot(Pos, "Dash_Effect");
        EffectManager.Instance.PoolParticleEffectOn("RightDash", Pos, _manager.transform.forward);
    }

    private void DashLeftAttack(EvnData evnData)
    {
        Vector3 Pos = _manager.LeftPawTransform.position;
        FmodManager.Instance.PlaySoundOneShot(Pos, "Dash_Effect");
        EffectManager.Instance.PoolParticleEffectOn("LeftDash", Pos, _manager.transform.forward);
    }

    private void DashLastRightPaw(EvnData evnData)
    {
        Vector3 Pos = _manager.RightPawTransform.position;
        FmodManager.Instance.PlaySoundOneShot(Pos, "Dash_Effect");
        EffectManager.Instance.PoolParticleEffectOn("RightDash", Pos, _manager.transform.forward);
        BlackBoard.IsDashAttackOn = false;
        _manager.AttackOff();
    }

    private void RushAttackOn(EvnData evnData)
    {
        _blackBoard.IsRushAttackOn = true;
        _manager.AttackOn(DragonAttackTriggers.Rush);
    }

    private void RushAttackOff(EvnData evnData)
    {
        FmodManager.Instance.PlaySoundOneShot(_manager.transform.position, "Rush_Effect");
        _blackBoard.IsRushAttackOn = false;
        _manager.AttackOff();
    }

    private void RightPawAttackOn(EvnData evnData)
    {
        _manager.AttackOn(DragonAttackTriggers.RightPaw);
    }

    private void LeftPawAttackOn(EvnData evnData)
    {
        _manager.AttackOn(DragonAttackTriggers.LeftPaw);
    }

    private void LandingOn(EvnData evnData)
    {
        _manager.DragonGroundCollider.enabled = true;
        EffectManager.Instance.PoolParticleEffectOn("Landing", _manager.transform.position, _manager.transform.forward);
    }

    private void LandingAttackOff(EvnData evnData)
    {
        _manager.AttackOff();
    }

    private void ShotBreathAttackOn(EvnData evnData)
    {
        Transform DragonMouth = _blackBoard.DragonBulletBreathMouth;
        BulletManager.Instance.CreateDragonBaseBulletTest(DragonMouth, 0.15f, 10);
    }

    private void HowlingAttackOn(EvnData evnData)
    {
        Vector3 pos =_manager.transform.position;
        int Amount = _blackBoard.FanShapeAmount;
        FmodManager.Instance.PlaySoundOneShot(pos, "Howling_Effect");
        BulletManager.Instance.CreateDragonBaseBullet(pos, Amount);
    }

    private void BreathAttackOn(EvnData evnData)
    {

        Transform DragonMouth = _blackBoard.DragonBreathMouth;

        Vector3 dir =
            (_utility.Player.position -
            DragonMouth.position).normalized;

        dir.y = 0.0f;

        BulletManager.Instance.CreateDragonBreath(DragonMouth.position, dir);

    }

    private void FlyingSoundOn (EvnData evnData)
    {
        FmodManager.Instance.PlaySoundAttatch(_manager.gameObject, "Flying");
    }

    private void HoveringSoundOn (EvnData evnData)
    {
        FmodManager.Instance.PlaySoundAttatch(_manager.gameObject, "Hovering");
    }

    private void DescentFlyingStart(EvnData evnData)
    {
        MovementManager.Instance.SetMovement(MovementType.AirSpear);
        _manager.DragonRigidBody.useGravity = false;
        _manager.DragonGroundCollider.enabled = false;
        _blackBoard.IsGround = false;
        _manager.FlyingOn = true;
    }
    
    private void MeteoFlyingStart(EvnData evnData)
    {
        MovementManager.Instance.SetMovement(MovementType.Meteo);
        _manager.DragonRigidBody.useGravity = false;
        _manager.DragonGroundCollider.enabled = false;
        _blackBoard.IsGround = false;
        _manager.FlyingOn = true;

    }

    private void MeteoAttackSoundOn(EvnData evnData)
    {
        FmodManager.Instance.PlaySoundOneShot(Player.instance.transform.position, "Meteor");
    }

    private void MeteoAttackOn(EvnData evnData)
    {
        BulletManager.Instance.CreateDragonMeteoBullet(_manager.transform, 50.0f, 100,0.1f);
    }

    private void MeteoAttackEnd(EvnData evnData)
    {
        _manager.IsAction = false;
        _blackBoard.IsMeteoHovering = true;
    }

    private void TailAttackOn(EvnData evnData)
    {
        _blackBoard.IsTailAttackOn = true;
        _manager.AttackOn(DragonAttackTriggers.Tail);
    }

    private void TailAttackOff(EvnData evnData)
    {
        _blackBoard.IsTailAttackOn = false;
        _manager.AttackOff();
    }

    private void FallingDead (EvnData evnData)
    {
        EffectManager.Instance.PoolParticleEffectOn("Landing", _manager.transform.position, _manager.transform.forward);
    }

    private void DeadEnd (EvnData evnData)
    {
        GameEndManager.Instance.GameClear();
    }

}
