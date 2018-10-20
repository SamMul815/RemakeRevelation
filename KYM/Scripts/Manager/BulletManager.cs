using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField] private GameObject playerBaseBullet;
    [SerializeField] private GameObject playerMachinBullet;
    [SerializeField] private GameObject dragonBaseBullet;
    [SerializeField] private GameObject dragonHowlingBullet;
    [SerializeField] private GameObject dragonBreathPrefab;
    [SerializeField] private GameObject dragonMeteoPrefab;


    private Transform player;
    private MeteoSkill meteoSkill;
    //private Transform dragon;

    private void Start()
    {
        player = Player.instance.transform;
        meteoSkill = this.GetComponent<MeteoSkill>();
    }

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

    public void CreatePlayerMachinBullet(Vector3 _position, Vector3 _dir)
    {
        Quaternion rot = Quaternion.LookRotation(_dir, Vector3.up);
        GameObject bullet;
        PoolManager.Instance.PopObject(playerMachinBullet, _position, rot, out bullet);
        bullet.GetComponent<Bullet>().FireEvent();
    }

    public void CreatePlayerMachinBullet(Transform _firePos)
    {
        GameObject bullet;
        PoolManager.Instance.PopObject(playerMachinBullet, _firePos.position, _firePos.rotation, out bullet);
        bullet.GetComponent<Bullet>().FireEvent();
    }

    public void CreateDragonBaseBullet(Vector3 _position,int amount)
    {
        float randomAngle = Random.Range(0, 360);
        float f_amount = amount;
        for (int i = 0; i< amount; i++)
        {
            GameObject bullet;
            PoolManager.Instance.PopObject(dragonHowlingBullet, out bullet);
            if (bullet == null) return;
            bullet.transform.position = _position;
            bullet.transform.rotation = Quaternion.Euler(0f, i * (360.0f / f_amount) + randomAngle,0f);
        }
    }


    public void CreateDragonBaseBulletTest(Vector3 _position, float _time, int _amount)
    {
        StartCoroutine(CorTestDragonBaseBullet(_position, _time, _amount));
    }

    public void CreateDragonBaseBulletTest(Transform trans, float _time, int _amount)
    {
        StartCoroutine(CorTestDragonBaseBullet(trans, _time, _amount));
    }


    public void CreateDragonBreath(Vector3 _position, Vector3 _dir)
    {
        GameObject breath;
        PoolManager.Instance.PopObject(dragonBreathPrefab, out breath);
        breath.transform.position = _position;
        breath.transform.rotation = Quaternion.LookRotation(_dir, Vector3.up);
    }

    public void CreateDragonMeteoBullet(Transform _dragon, float _fireRadius, float _hitRadius, int _amount)
    {
        StartCoroutine(CorDragonMeteoBullet(_dragon, _fireRadius, _hitRadius, _amount)); 
    }

    public void CreateDragonMeteoBullet(Transform _dragon, float _fireRadius, int _amount,float dealyTime)
    {
        StartCoroutine(CorDragonMeteoBullet(_dragon, _fireRadius, _amount, dealyTime));
    }

    IEnumerator CorTestDragonBaseBullet(Vector3 _position, float _time, int _amount)
    {
        float range = Vector3.Distance(_position, player.transform.position) * 0.1f;
        for(int i = 0; i< _amount; i++)
        {
            Vector3 dir = (player.transform.position + Random.insideUnitSphere * range) - _position;
            GameObject bullet;
            PoolManager.Instance.PopObject(dragonBaseBullet, out bullet);
            bullet.transform.position = _position;
            bullet.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            bullet.GetComponent<BulletBaseDragon>().ChangeSpeed(30 + Random.Range(10, 20.0f));
            yield return new WaitForSeconds(_time - Random.Range(0, _time * 0.5f));
        }
    }

    IEnumerator CorTestDragonBaseBullet(Transform _trans, float _time, int _amount)
    {
        Transform player = Player.instance.transform;
        for (int i = 0; i < _amount; i++)
        {           
            for(int j = 0; j< 4; j++)
            {
                GameObject bullet;
                PoolManager.Instance.PopObject(dragonBaseBullet, out bullet);
                //Vector3 dir = player.position - _trans.position;
                //Vector3 lerp = Vector3.Lerp(_trans.forward,dir, 0.8f);

                
                Vector3 rnd = Random.insideUnitSphere * 15.0f;
                rnd.x = Random.Range(-20.0f, -5.0f);
                float distance = Vector3.Distance(_trans.position, Player.instance.transform.position);
                if(distance < 50)
                    rnd.x = Random.Range(-20.0f, -5.0f) + distance *0.5f;



                bullet.transform.position = _trans.position;
                bullet.transform.rotation = Quaternion.LookRotation(_trans.forward, Vector3.up);
                bullet.transform.rotation *= Quaternion.Euler(rnd);

                //if(Vector3.Dot(bullet.transform.forward,dir) <0.3f)
                //{
                //    bullet.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
                //    //bullet.transform.rotation *= Quaternion.Euler(Random.insideUnitSphere * 5f);
                //}



                bullet.GetComponent<BulletBaseDragon>().ChangeSpeed(30 + Random.Range(10, 20.0f));
            }
            yield return new WaitForSeconds(_time - Random.Range(0, _time * 0.5f));
        }
    }

    IEnumerator CorDragonMeteoBullet(Transform _dragon, float _fireRadius, float _hitRadius, int _amount)
    {
        Vector3 up = _dragon.up;
        Vector3 right = _dragon.right;
        Vector3 position = _dragon.position;

        for(int i = 0; i<_amount; i++)
        {
            GameObject meteo;
            PoolManager.Instance.PopObject(dragonMeteoPrefab, out meteo);
            Vector2 fireCircle = Random.insideUnitCircle * _fireRadius;
            meteo.transform.position = position + up * fireCircle.y + right * fireCircle.x;
            Vector2 hitCircle = Random.insideUnitCircle * _hitRadius;
            Vector3 playerPos = player.transform.position + Vector3.forward * hitCircle.y + Vector3.right * hitCircle.x;
            Vector3 dir = playerPos - meteo.transform.position;
            meteo.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            meteo.GetComponent<BulletMeteoDragon>().ChangeSpeed(200 - Random.Range(0, 30.0f));
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }

    //IEnumerator CorDragonMeteoBullet(Transform _dragon, float _fireRadius, int _amount, float dealyTime)
    //{
    //    Vector3 up = _dragon.up;
    //    Vector3 right = _dragon.right;
    //    Vector3 position = _dragon.position;

    //    for (int i = 0; i < _amount; i++)
    //    {
    //        GameObject meteo;
    //        PoolManager.Instance.PopObject(dragonMeteoPrefab, out meteo);
    //        Vector2 fireCircle = Random.insideUnitCircle * _fireRadius;
    //        Vector3 dir = player.transform.position - position;
    //        if(i % 10 == 0)
    //        {
    //            meteo.transform.position = position + up * fireCircle.y*0.1f + right * fireCircle.x*0.1f;
    //        }
    //        else
    //        {
    //            meteo.transform.position = position + up * fireCircle.y + right * fireCircle.x;
    //        }
    //        meteo.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    //        meteo.GetComponent<BulletMeteoDragon>().ChangeSpeed(300 - Random.Range(0, 50.0f));
    //        yield return new WaitForSeconds(dealyTime);
    //    }
    //    yield return null;
    //}

    IEnumerator CorDragonMeteoBullet(Transform _dragon, float _fireRadius, int _amount, float dealyTime)
    {
        //Vector3 up = _dragon.up;
        //Vector3 right = _dragon.right;
        //Vector3 position = _dragon.position;
        for (int i = 0; i < _amount; i++)
        {
            GameObject meteo;
            PoolManager.Instance.PopObject(dragonMeteoPrefab,_dragon.position, out meteo);
            //meteo.transform.position = _dragon.position;

            Vector3 throwPos = meteoSkill.GetRndPos();
            Vector3 dir = throwPos - meteo.transform.position;
            meteo.transform.rotation = Quaternion.LookRotation(dir,Vector3.up);
            meteo.GetComponent<BulletMeteoDragon>().ChangeSpeed(250);
            //meteo.GetComponent<BulletMeteoDragon>().ChangeSpeed(250 - Random.Range(0, 50.0f));
            yield return new WaitForSeconds(dealyTime);
        }
        yield return null;
    }
}
