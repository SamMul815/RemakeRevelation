using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBaseDragon : Bullet
{
    [SerializeField]
    private float bulletHP;
    private float currentHP;
 
    protected override void Awake()
    {
        base.Awake();
        currentHP = bulletHP;
    }

    public override void Init()
    {
        base.Init();
        moveDir = transform.forward;
        currentHP = bulletHP;
    }

    public void Hit(float damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            DestroyDieBullet();
            //PoolManager.Instance.PushObject(this.gameObject);
        }
    }
    protected override void Move()
    {
        moveDir = transform.forward;
        transform.position += moveDir * Time.fixedDeltaTime * moveSpeed;
    }

    public void ChangeSpeed(float speed)
    {
        moveSpeed = speed;
    }

}
