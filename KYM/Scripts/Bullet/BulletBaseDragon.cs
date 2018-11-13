using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBaseDragon : Bullet
{
    [SerializeField]
    private float bulletHP;
    public float homingPower = 0.01f;
    private float currentHP;
    private Transform player;


    protected override void Awake()
    {
        base.Awake();
        currentHP = bulletHP;
        player = Player.instance.transform;
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
        if(_col.CompareTag("Player") || _col.CompareTag("Gun"))
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
        Vector3 playerDir = player.position - transform.position;
        Vector3 dir = Vector3.Lerp(transform.forward, playerDir.normalized, homingPower);

        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.position += dir * Time.fixedDeltaTime * moveSpeed;

        moveDir = transform.forward;

        /*
        moveDir = transform.forward;
        transform.position += moveDir * Time.fixedDeltaTime * moveSpeed;
        */

    }

    public void ChangeSpeed(float speed)
    {
        moveSpeed = speed;
    }

}
