using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class DragonAttackTrigger : MonoBehaviour
{

    [SerializeField]
    private DragonAttackTriggers _triggerTag;
    public  DragonAttackTriggers TriggerTag { get { return _triggerTag; } }

    [SerializeField]
    private float _damage;
    public float Damage { get { return _damage; } }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DragonManager.Instance.AttackOff();
        }
    }

}
