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

    protected override void OnCollisionEvent()
    {
        //base.OnCollisionEvent();

        Collider _col = hitInfo.collider;
        if(_col.CompareTag("Player"))
        {
            Player.instance.playerStat.Hit(damage);
        }
        DestroyHitBullet();
    }

    public void Hit(float damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            DestroyDieBullet();
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
