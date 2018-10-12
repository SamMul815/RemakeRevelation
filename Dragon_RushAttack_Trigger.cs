using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_RushAttack_Trigger : DragonAttackTrigger
{
    [SerializeField]
    protected float _pushPower;
    public float PushPower { get { return _pushPower; } }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 dir =
                Vector3.Normalize(_manager.Player.position - _manager.transform.position);

            dir += new Vector3(0.0f, 0.2f, 0.0f);

            Player.instance.playerRigid.PlayerPush(dir, _pushPower);
            BlackBoard.Instance.IsPlayerRushAttack = true;
            DragonManager.Instance.AttackOff();

        }
    }
}
