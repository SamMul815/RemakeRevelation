using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RushAttack_Trigger : Dragon_PushAttack_Trigger
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 dir =
                Vector3.Normalize(_manager.Player.position - _manager.transform.position);

            //dir += new Vector3(0.0f, 0.2f, 0.0f);
            dir += _pushDir;

            if (BlackBoard.Instance.IsPlayer)
            { 
                Player.instance.playerRigid.PlayerPush(dir, _pushPower);
                Player.instance.playerStat.Hit(_damage);
            }
            BlackBoard.Instance.IsPlayerRushAttack = true;
            DragonManager.Instance.AttackOff();

        }
    }
}
