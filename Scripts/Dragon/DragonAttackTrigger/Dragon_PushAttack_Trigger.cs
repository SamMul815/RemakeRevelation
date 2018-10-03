using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_PushAttack_Trigger : DragonAttackTrigger
{


    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 dir = 
                Vector3.Normalize(DragonManager.Player.position - DragonManager.Instance.transform.position);

            dir += _offsetDir;
            Player.instance.playerRigid.PlayerPush(dir, _pushPower);
            BlackBoard.Instance.IsPlayerDashAttack = true;
            DragonManager.Instance.AttackOff();

        }
    }


}
