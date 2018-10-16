using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMeteoDragon : Bullet {

    //public LayerMask layerMask;
    public float rayDistance;
    public GameObject hitEffect;

    protected override void Move()
    {
        moveDir = transform.forward;
        transform.position += moveDir * Time.fixedDeltaTime * moveSpeed;
    }
    protected override void OnCollisionEvent()
    {
        //base.OnCollisionEvent();
        DestroyHitBullet();
    }

    protected override bool CollisionCheck()
    {
        Vector3 _dir = transform.position - prevPosition;
        Ray ray = new Ray(transform.position, _dir);
        return Physics.Raycast(ray, out hitInfo, moveSpeed * Time.fixedDeltaTime, hitLayer);
        //return base.CollisionCheck();
    }

    protected override void DestroyHitBullet()
    {
        GameObject particle;
        if (destroyParticle != null)
        {
            PoolManager.Instance.PopObject(destroyParticle, out particle);
            particle.transform.position = hitInfo.point;
            particle.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            Debug.LogWarning(this.gameObject.name + "Not Found Destroy Particle");
        }
        PoolManager.Instance.PushObject(this.gameObject);
        //base.DestroyHitBullet();
    }

    public void ChangeSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void Update()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, rayDistance, hitLayer))
        {
            hitEffect.transform.position = raycastHit.point;
            hitEffect.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
