using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_DashAttack_Trigger : DragonAttackTrigger
{
    public float pushPower;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 dir = 
                Vector3.Normalize(DragonManager.Player.position - DragonManager.Instance.transform.position);

            dir += new Vector3(0.0f, 0.2f, 0.0f);
            Player.instance.playerRigid.PlayerPush(dir, pushPower);
            BlackBoard.Instance.IsPlayerDashAttack = true;
            DragonManager.Instance.AttackOff();

        }
    }


}
