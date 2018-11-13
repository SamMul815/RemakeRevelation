﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RedZoneAttack_Action : ActionTask
{
    Vector3 dir;
    float pushPower;
    float damege;
    float distance;

    float basePushPower;
     
    public override void Init()
    {
        base.Init();
        distance = _blackBoard.RedZoneDistance;
        damege = _blackBoard.RedZoneDamage;
        pushPower = 30.0f / distance;
        damege = 10.0f;

        basePushPower = 20.0f;
    }

    public override void OnStart()
    {
        base.OnStart();

        _blackBoard.IsRedZoneIn = true;
        EffectManager.Instance.PoolParticleEffectOn("NearHowling",
            _manager.transform.position,
            _manager.transform.forward);
        FmodManager.Instance.PlaySoundOneShot(DragonTransform.position, "Howling");
        DragonAniManager.SwicthAnimation("Dragon_NearHowling");

    }

    public override bool Run()
    {
        if (UtilityManager.DistanceCalc(DragonTransform, PlayerTransform, distance))
        {
            if (_blackBoard.IsRedZoneAttackOn)
            {
                float FinalDistance = (DragonTransform.position - PlayerTransform.position).sqrMagnitude;
                float FinalPushPower = pushPower * (distance - Mathf.Sqrt(FinalDistance));

                if (FinalPushPower <= basePushPower)
                {
                    FinalPushPower = basePushPower;
                }

                dir = (PlayerTransform.position - DragonTransform.position).normalized;
                dir += new Vector3(0.0f, 0.2f, 0.0f);

                FinalPushPower = Mathf.RoundToInt(FinalPushPower);
                if (_blackBoard.IsPlayer)
                {
                    _playerManager.playerRigid.PlayerPush(dir, FinalPushPower);
                    _playerManager.playerStat.Hit(damege);

                }
                _blackBoard.IsRedZoneAttackOn = false;
            }
        }
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
