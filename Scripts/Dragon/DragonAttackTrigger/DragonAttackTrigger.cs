using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class DragonAttackTrigger : MonoBehaviour
{

    [SerializeField]
    protected DragonAttackTriggers _triggerTag;
    public  DragonAttackTriggers TriggerTag { get { return _triggerTag; } }

    [SerializeField]
    protected Vector3 _offsetDir;
    public Vector3 OffsetDir { get { return _offsetDir; } }

    [SerializeField]
    protected float _pushPower;
    public float PushPower { get { return _pushPower; } }

    [SerializeField]
    protected float _damage;
    public float Damage { get { return _damage; } }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DragonManager.Instance.AttackOff();
        }
    }

}
