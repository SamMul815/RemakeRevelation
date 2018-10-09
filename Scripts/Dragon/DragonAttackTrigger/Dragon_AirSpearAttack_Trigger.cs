using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_AirSpearAttack_Trigger : DragonAttackTrigger
{
    [SerializeField]
    protected float _pushPower;
    public float PushPower { get { return _pushPower; } }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Vector3 dir = 
                Vector3.Normalize(DragonManager.Player.position - DragonManager.Instance.transform.position);

            dir += new Vector3(0.0f, 0.4f, 0.0f);

            Player.instance.playerRigid.PlayerPush(dir, _pushPower);
            BlackBoard.Instance.IsPlayerDashAttack = true;
            DragonManager.Instance.AttackOff();

        }
    }


}
