﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class Bullet : MonoBehaviour {

    [SerializeField]
    protected GameObject destroyParticle;

    [SerializeField]
    protected int damage;                 //데미지

    [SerializeField]
    protected int damagePlusMinusValue;

    [SerializeField]
    protected float moveSpeed;             //투사체 속도

    [SerializeField]
    LayerMask hitLayer;                    //충돌되는 레이어들

    [SerializeField]
    private float maxMoveDistance = 20000.0f;
    private float moveDistance;
    //protected Vector3 startPosition;

    protected CapsuleCollider capsuleCol;  //컬라이더 정보
    protected Vector3 moveDir;             //이동 방향
    protected Vector3 prevPosition;        //이전 위치
    protected RaycastHit[] hitInfo;        //충돌 정보    

    protected virtual void Awake()
    {
        //Init();
        GetComponent<PoolObject>().Init = Init;
        //Debug.Log(gameObject.name + "Awake호출");
        //GetComponent<PoolObject>().Reset = Reset;
        capsuleCol = GetComponent<CapsuleCollider>();
        moveDistance = maxMoveDistance;
        prevPosition = this.transform.position;
    }

    /// <summary>
    /// 총알 초기화
    /// </summary>
    public virtual void Init()
    {
        prevPosition = this.transform.position;
        moveDistance = maxMoveDistance;
        //capsuleCol = GetComponent<CapsuleCollider>();
    }

    protected virtual void Reset() { }


    /// <summary>
    /// 투사체 이동 관련 
    /// </summary>
    protected virtual void Move(){}
    /// <summary>
    /// 부딪칠때 이벤트
    /// </summary>
    protected virtual void OnCollisionEvent() {}

    private bool IsMaxDistance() { return moveDistance <= 0.0f; }

    /// <summary>
    /// Move후 충돌 체크 구문 prevPosition에 이전 위치가 들어가있어야 된다.
    /// </summary>
    protected virtual bool CollisionCheck()
    {
        Vector3 _dir = transform.position - prevPosition;
        Vector3 _colDir;
        Vector3 _p1;
        Vector3 _p2;

        //충돌 확인할 캡슐 컬라이더 (방향)
        if (capsuleCol.direction == 0) _colDir = transform.right;
        else if (capsuleCol.direction == 1) _colDir = transform.up;
        else _colDir = transform.forward;

        //충돌 확인할 캡슐컬라이더 정점
        _p1 = prevPosition + Matrix4x4.Scale(transform.localScale).MultiplyPoint3x4(_colDir * capsuleCol.height * 0.5f);
        _p2 = prevPosition - Matrix4x4.Scale(transform.localScale).MultiplyPoint3x4(_colDir * capsuleCol.height * 0.5f);
        hitInfo = Physics.CapsuleCastAll(_p1, _p2, capsuleCol.radius * transform.localScale.y, _dir.normalized, _dir.magnitude, hitLayer);
        return hitInfo.Length > 0 ? true : false;
    }

    private void FixedUpdate()
    {
        prevPosition = this.transform.position;
        Move();
        moveDistance -= moveSpeed * Time.fixedDeltaTime;
        if (CollisionCheck())
         OnCollisionEvent();

        if (IsMaxDistance())
            PoolManager.Instance.PushObject(this.gameObject);
    }

    protected virtual void DestroyHitBullet()
    {
        GameObject particle;
        if(destroyParticle != null)
        {
            PoolManager.Instance.PopObject(destroyParticle, out particle);
            particle.transform.position = hitInfo[0].point;
            particle.transform.rotation = Quaternion.LookRotation(hitInfo[0].normal, Vector3.up);
        }
        else
        {
            Debug.LogWarning(this.gameObject.name + "Not Found Destroy Particle");
        }
        PoolManager.Instance.PushObject(this.gameObject);
    }

    protected virtual void DestroyDieBullet()
    {
        GameObject particle;
        if (destroyParticle != null)
        {
            PoolManager.Instance.PopObject(destroyParticle, out particle);
            particle.transform.position = this.transform.position;
            //particle.transform.rotation = Quaternion.LookRotation(hitInfo[0].normal, Vector3.up);
        }
        else
        {
            Debug.LogWarning(this.gameObject.name + "Not Found Destroy Particle");
        }
        PoolManager.Instance.PushObject(this.gameObject);
    }

}
