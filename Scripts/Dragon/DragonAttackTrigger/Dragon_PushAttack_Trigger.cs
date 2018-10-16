using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_PushAttack_Trigger : DragonAttackTrigger
{
    [SerializeField]
    protected float _pushPower;
    public float PushPower { get { return _pushPower; } }

    [SerializeField]
    protected Vector3 _pushDir;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Vector3 dir = 
                Vector3.Normalize(_manager.Player.position - _manager.transform.position);

            //dir += new Vector3(0.0f, 0.4f, 0.0f);
            dir += _pushDir;

            Debug.Log("OnHit");
            Debug.Log(dir);
            if (BlackBoard.Instance.IsPlayer)
            {
                Player.instance.playerRigid.PlayerPush(dir, _pushPower);
                Player.instance.playerStat.Hit(_damage);
            }

            DragonManager.Instance.AttackOff();

        }
    }


}
