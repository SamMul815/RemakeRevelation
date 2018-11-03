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
        AddAnimTimeEventFunc(ActionEnd, "Breath");

        AddAnimTimeEventFunc(LandingOn, "Landing");
        AddAnimTimeEventFunc(LandingAttackOff, "Landing");
        AddAnimTimeEventFunc(ActionEnd, "Landing");


        AddAnimTimeEventFunc(RightPawAttackOn, "RightPaw");
        AddAnimTimeEventFunc(AttackOff, "RightPaw");
        AddAnimTimeEventFunc(ActionEnd, "RightPaw");

        AddAnimTimeEventFunc(LeftPawAttackOn, "LeftPaw");
        AddAnimTimeEventFunc(AttackOff, "LeftPaw");
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

        AddAnimTimeEventFunc(MeteoAttackOn, "MeteoAttack");
        AddAnimTimeEventFunc(MeteoAttackEnd, "MeteoAttack");

        AddAnimTimeEventFunc(ActionEnd, "DestroyPart");

        AddAnimTimeEventFunc(TakeOff, "TakeOff");
        AddAnimTimeEventFunc(ActionEnd, "TakeOff");

        AddAnimTimeEventFunc(ActionEnd, "NearHowling");

        //---------------------Sound Start------------------------
        ////Howling
        //AddAnimTimeEventFunc(SoundHowling, "Howling");

        ////Breath
        //AddAnimTimeEventFunc(SoundBreath, "Breath");

        ////Breath
        //AddAnimTimeEventFunc(SoundShot_Breath, "Shot_Breath");

        ////Dash
        //AddAnimTimeEventFunc(SoundDashAttack1, "Dash");
        //AddAnimTimeEventFunc(SoundDashAttack2, "Dash");
        //AddAnimTimeEventFunc(SoundDashAttack3, "Dash");

        ////Rush
        //AddAnimTimeEventFunc(SoundRushAttack, "Rush");

        //---------------------Sound End--------------------------
    }


    private void TakeOff(EvnData evnData)
    {
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

    private void DashFirstRightPaw(EvnData evnData)
    {
        Vector3 Pos = _manager.RightPawTransform.position;
        EffectManager.Instance.PoolParticleEffectOn("RightDash", Pos, _manager.transform.forward);
    }

    private void DashLeftAttack(EvnData evnData)
    {
        Vector3 Pos = _manager.LeftPawTransform.position;
        EffectManager.Instance.PoolParticleEffectOn("LeftDash", Pos, _manager.transform.forward);
    }

    private void DashLastRightPaw(EvnData evnData)
    {
        Vector3 Pos = _manager.RightPawTransform.position;
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
        BulletManager.Instance.CreateDragonBaseBulletTest(DragonMouth.position, 0.15f, 10);
    }

    private void HowlingAttackOn(EvnData evnData)
    {
        Vector3 pos =
            _manager.transform.position +
            new Vector3(0.0f, 2.0f, 0.0f);

        int Amount = _blackBoard.FanShapeAmount;
        Vector3 Pos = _manager.LeftPawTransform.position;
        BulletManager.Instance.CreateDragonBaseBullet(pos, Amount);
    }

    private void BreathAttackOn(EvnData evnData)
    {

        Transform DragonMouth = _blackBoard.DragonBreathMouth;

        Vector3 dir =
            (_utility.Player.position -
            _manager.transform.position).normalized;
        dir.y = 0.0f;

        BulletManager.Instance.CreateDragonBreath(DragonMouth.position, dir);

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

    private void MeteoAttackOn(EvnData evnData)
    {
        BulletManager.Instance.CreateDragonMeteoBullet(_manager.transform, 50.0f, 10);
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
    //---------------------Sound Function Start -----------------------------------------------

    //Howling
    private void SoundHowling(EvnData evenData)
    {
        SoundManagerNormal.Instance.PlayAudio("dt1", DragonManager.Instance.transform.position,0.0f,AudioDType._3D);
    }

    //Breath
    private void SoundBreath(EvnData evnData)
    {
        SoundManagerNormal.Instance.PlayAudio("dt3", DragonManager.Instance.transform.position,1.0f, AudioDType._3D);
    }

    //Shot_Breath
    private void SoundShot_Breath(EvnData evnData)
    {
        SoundManagerNormal.Instance.PlayAudio("dt4", DragonManager.Instance.transform.position, 1.0f, AudioDType._3D);
    }

    //Dash
    private void SoundDashAttack1(EvnData evnData)
    {
        SoundManagerNormal.Instance.PlayAudio("da1",DragonManager.Instance.transform.position,0.0f, AudioDType._3D);
    }
    private void SoundDashAttack2(EvnData evnData)
    {
        SoundManagerNormal.Instance.PlayAudio("da2", DragonManager.Instance.transform.position,0.0f, AudioDType._3D);
    }
    private void SoundDashAttack3(EvnData evnData)
    {
        SoundManagerNormal.Instance.PlayAudio("da3", DragonManager.Instance.transform.position,0.0f, AudioDType._3D);
    }

    //Rush
    private void SoundRushAttack(EvnData evnData)
    {
        SoundManagerNormal.Instance.PlayAudio("dt3", DragonManager.Instance.transform.position,1.0f, AudioDType._3D);
    }

    //---------------------Sound Function End--------------------------------------------------

}
