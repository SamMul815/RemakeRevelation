﻿using System.Collections;
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
        AddAnimTimeEventFunc(LadingEnd, "Landing");
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
        AddAnimTimeEventFunc(AttackOff, "Rush");
        AddAnimTimeEventFunc(ActionEnd, "Rush");


        AddAnimTimeEventFunc(ShotBreathAttackOn ,"Shot_Breath");
        AddAnimTimeEventFunc(ActionEnd, "Shot_Breath");

        AddAnimTimeEventFunc(HowlingAttackOn, "Howling");
        AddAnimTimeEventFunc(ActionEnd, "Howling");

        AddAnimTimeEventFunc(DescentFlyingOn, "DescentFlying");

        AddAnimTimeEventFunc(MeteoFlyingStart, "MeteoTakeOff");

        AddAnimTimeEventFunc(MeteoAttackOn, "MeteoAttack");
        AddAnimTimeEventFunc(MeteoAttackEnd, "MeteoAttack");

        AddAnimTimeEventFunc(ActionEnd, "DestroyPart");
        AddAnimTimeEventFunc(ActionEnd, "TakeOff");
        AddAnimTimeEventFunc(ActionEnd, "Tail");

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

    private void ActionEnd(EvnData evnData)
    {
        _manager.IsAction = false;
    }     

    private void DashAttackOn(EvnData evnData)
    {
        _blackBoard.IsDashAttackOn = true;
        _manager.AttackOn(DragonAttackTriggers.Dash);

    }

    private void DashFirstRightPaw(EvnData evnData)
    {
        Vector3 Pos = _manager.RightPawTransform.position;
        EffectManager.Instance.PoolParticleEffectOn("RightDash", Pos, Vector3.zero);
    }

    private void DashLeftAttack(EvnData evnData)
    {
        Vector3 Pos = _manager.LeftPawTransform.position;
        EffectManager.Instance.PoolParticleEffectOn("LeftDash", Pos, Vector3.zero);
    }

    private void DashLastRightPaw(EvnData evnData)
    {
        Vector3 Pos = _manager.RightPawTransform.position;
        EffectManager.Instance.PoolParticleEffectOn("RightDash", Pos, Vector3.zero);
        BlackBoard.IsDashAttackOn = false;
        _manager.AttackOff();
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
        Transform DragonMouth = _blackBoard.DragonBulletBreathMouth;
        BulletManager.Instance.CreateDragonBaseBulletTest(DragonMouth, 0.15f, 10);
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

        Transform DragonMouth = _blackBoard.DragonBreathMouth;

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

    private void MeteoFlyingStart(EvnData evnData)
    {
        _manager.FlyingOn = true;
        _manager.DragonRigidBody.useGravity = false;
        _manager.DragonGroundCollider.enabled = false;
        MovementManager.Instance.SetMovement(MovementType.Meteo);

    }

    private void MeteoAttackOn(EvnData evnData)
    {
        BulletManager.Instance.CreateDragonMeteoBullet(_manager.transform, 50.0f, 20);
    }

    private void MeteoAttackEnd(EvnData evnData)
    {
        _manager.IsAction = false;
        _blackBoard.IsLanding = true;
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
