﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class DragonAttackTrigger : MonoBehaviour
{

    protected DragonManager _manager;
    public DragonManager Manager { get { return _manager; } }

    [SerializeField]
    protected DragonAttackTriggers _triggerTag;
    public  DragonAttackTriggers TriggerTag { get { return _triggerTag; } }

    [SerializeField]
    protected float _damage;
    public float Damage { get { return _damage; } }

    private void Awake()
    {
        _manager = DragonManager.Instance;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DragonManager.Instance.AttackOff();
        }
    }

}
