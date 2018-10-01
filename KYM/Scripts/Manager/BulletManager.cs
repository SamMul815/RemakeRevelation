using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField] private GameObject playerBaseBullet;
    [SerializeField] private GameObject dragonBaseBullet;
    [SerializeField] private GameObject dragonSlashSkill;
    [SerializeField] private GameObject dragonRotateBullet;
    [SerializeField] private GameObject dragonBaseBulletTest;
    [SerializeField] private GameObject dragonBreathPrefab;

    public void CreatePlayerBaseBullet(Vector3 _position, Vector3 _dir)
    {
        Quaternion rot = Quaternion.LookRotation(_dir, Vector3.up);
        GameObject bullet;
        PoolManager.Instance.PopObject(playerBaseBullet, out bullet);
        bullet.transform.position = _position;
        bullet.transform.rotation = rot;
        bullet.GetComponent<BulletBase>().Init();
    } 

    public void CreatePlayerBaseBullet(Transform _firePos)
    {
        GameObject bullet;
        PoolManager.Instance.PopObject(playerBaseBullet, out bullet);
        bullet.transform.position = _firePos.position;
        bullet.transform.rotation = _firePos.rotation;
        bullet.GetComponent<BulletBase>().Init();
    }

    public void CreateDragonBaseBullet(Vector3 _position,int amount)
    {
        float f_amount = amount;
        //PoolObject pObj = dragonBaseBullet.GetComponent<PoolObject>();
        for (int i = 0; i< amount; i++)
        {
            GameObject bullet;
            PoolManager.Instance.PopObject(dragonBaseBullet, out bullet);
            if (bullet == null) return;
            //if(i %2 == 0)
            //{
            //}
            bullet.transform.position = _position;
            bullet.transform.rotation = Quaternion.Euler(0f, i * (360.0f / f_amount),0f);

            //else if(i%2 == 1)
            //{
            //    bullet.transform.position = _position + Vector3.up * 5;
            //    bullet.transform.rotation = Quaternion.Euler(0f, i * (360.0f / f_amount), 0f);
            //}
            //else
            //{
            //    bullet.transform.position = _position + Vector3.up * 10;
            //    bullet.transform.rotation = Quaternion.Euler(0f, i * (360.0f / f_amount), 0f);
            //}    
        }
    }

    public void CreateDragonSlashSkill(Vector3 _position)
    {
        GameObject bullet;
        for (int i = 0; i < 10; i++)
        {
            PoolManager.Instance.PopObject(dragonRotateBullet, out bullet);
            bullet.GetComponent<BulletRotateYDragon>().BasePosition(_position);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0.0f, i * 36, 0.0f));

        }
        //PoolManager.Instance.PopObject(dragonSlashSkill, out bullet);
        //bullet.transform.position = _position;
    }

    public void CreateDragonBaseBulletTest(Vector3 _position, float _time, int _amount)
    {
        StartCoroutine(CorTestDragonBaseBullet(_position, _time, _amount));
    }

    public void CreateDragonBreath(Vector3 _position, Vector3 _dir)
    {
        GameObject breath;
        PoolManager.Instance.PopObject(dragonBreathPrefab, out breath);
        breath.transform.position = _position;
        breath.transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
    }

    IEnumerator CorTestDragonBaseBullet(Vector3 _position, float _time, int _amount)
    {
        for(int i = 0; i< _amount; i++)
        {
            Vector3 dir = (Player.instance.transform.position + Random.insideUnitSphere * 20.0f) - _position;
            GameObject bullet;
            PoolManager.Instance.PopObject(dragonBaseBulletTest, out bullet);
            bullet.transform.position = _position;
            bullet.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            bullet.GetComponent<BulletBaseDragon>().ChangeSpeed(30 + Random.Range(0, 20.0f));
            yield return new WaitForSeconds(_time - Random.Range(0, _time * 0.5f));
        }
    }


}
