using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField] private GameObject playerBaseBullet;
    [SerializeField] private GameObject dragonBaseBullet;
    [SerializeField] private GameObject dragonRotateBullet;
    [SerializeField] private GameObject dragonBreathPrefab;

    public float DragonBaseBulletSpeed = 50.0f;
    
    public void CreatePlayerBaseBullet(Vector3 _position, Vector3 _dir)
    {
        Quaternion rot = Quaternion.LookRotation(_dir, Vector3.up);
        GameObject bullet;
        PoolManager.Instance.PopObject(playerBaseBullet,_position, rot, out bullet);
        bullet.GetComponent<Bullet>().FireEvent();
    } 

    public void CreatePlayerBaseBullet(Transform _firePos)
    {
        GameObject bullet;
        PoolManager.Instance.PopObject(playerBaseBullet, _firePos.position, _firePos.rotation, out bullet);
        bullet.GetComponent<Bullet>().FireEvent();
    }

    public void CreateDragonBaseBullet(Vector3 _position,int amount)
    {
        float f_amount = amount;
        for (int i = 0; i< amount; i++)
        {
            GameObject bullet;
            PoolManager.Instance.PopObject(dragonBaseBullet, out bullet);
            if (bullet == null) return;
            bullet.transform.position = _position;
            bullet.transform.rotation = Quaternion.Euler(0f, i * (360.0f / f_amount),0f);
            bullet.GetComponent<BulletBaseDragon>().ChangeSpeed(DragonBaseBulletSpeed);

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
        float range = (_position - Player.instance.transform.position).magnitude / 10;
        for(int i = 0; i< _amount; i++)
        {
            Vector3 dir = (Player.instance.transform.position + Random.insideUnitSphere * range) - _position;
            GameObject bullet;
            PoolManager.Instance.PopObject(dragonBaseBullet, out bullet);
            bullet.transform.position = _position;
            bullet.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            bullet.GetComponent<BulletBaseDragon>().ChangeSpeed(30 + Random.Range(10, 20.0f));
            yield return new WaitForSeconds(_time - Random.Range(0, _time * 0.5f));
        }
    }
}
