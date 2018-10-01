using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : Bullet
{
    [SerializeField]
    TrailRenderer trail;
    public override void Init()
    {
        base.Init();
        moveDir = this.transform.forward;
        trail.Clear();

    }

    //protected override void Reset()
    //{
    //    base.Reset();
    //    trail.Clear();
    //}

    protected override void OnCollisionEvent()
    {
        Collider _col = hitInfo[0].collider;
        if (_col.tag == "DragonBullet")
        {
            _col.gameObject.GetComponent<BulletBaseDragon>().Hit(damage);
        }
        else if (_col.tag == "DragonHit")
        {
            int d = Random.Range(-damagePlusMinusValue, damagePlusMinusValue);
            d += damage;
            UIManager.Instance.CreatePopupText(d.ToString(), hitInfo[0].point);
            //DragonHP관련
            DragonController.DragonManager.Instance.Hit(d);
        }
        else if (_col.tag == "WeakPoint")
        {
            UIManager.Instance.CreatePopupTextRed(damage.ToString(), hitInfo[0].point);
            _col.gameObject.GetComponent<WeakPoint>().Hit(damage);
        }
        DestroyHitBullet();

        //for(int i = 0; i<hitInfo.Length; i++)
        //{
        //    Collider _col = hitInfo[i].collider;
        //    if(_col.tag == "DragonBullet")
        //    {
        //        _col.gameObject.GetComponent<BulletBaseDragon>().Hit(damage);
        //        break;
        //    }
        //    else if (_col.tag == "DragonHit")
        //    {
        //        //DragonHP관련
        //        UIManager.Instance.CreatePopupText(damage.ToString(), hitInfo[i].point);
        //        Debug.Log(_col.name);
        //        break;
        //    }
        //    else if(_col.tag == "WeakPoint")
        //    {
        //        UIManager.Instance.CreatePopupTextRed(damage.ToString(), hitInfo[i].point);
        //        _col.gameObject.GetComponent<WeakPoint>().Hit(damage);
        //    }
        //    DestroyBullet();
        //}
    }

    protected override void Move()
    {
        //this.transform.position += moveDir * Time.fixedUnscaledDeltaTime * moveSpeed;
        this.transform.position += moveDir * Time.fixedDeltaTime * moveSpeed;
    }

}
