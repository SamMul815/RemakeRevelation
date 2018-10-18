using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DashAttack_Trigger : Dragon_PushAttack_Trigger
{

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 dir = 
                Vector3.Normalize(_utility.Player.position - _manager.transform.position);

            dir += _pushDir;

            if (BlackBoard.Instance.IsPlayer)
            {
                Player.instance.playerRigid.PlayerPush(dir, _pushPower);
                Player.instance.playerStat.Hit(_damage);
            }

            DragonManager.Instance.AttackOff();

        }
    }


}
