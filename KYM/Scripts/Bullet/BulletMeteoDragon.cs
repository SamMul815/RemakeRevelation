using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMeteoDragon : Bullet {

    public LayerMask layerMask;
    public float rayDistance;
    public GameObject hitEffect;

    protected override void Move()
    {
        moveDir = transform.forward;
        transform.position += moveDir * Time.fixedDeltaTime * moveSpeed;
    }

    private void Update()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, rayDistance, layerMask))
        {
            hitEffect.transform.position = raycastHit.point;
            hitEffect.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
