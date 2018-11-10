using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class Bullet : MonoBehaviour {

    [SerializeField]
    protected GameObject destroyParticle;

    [SerializeField]
    protected int damage;                 //데미지

    public int Damage { get { return damage; } }

    [SerializeField]
    protected int damagePlusMinusValue;

    [SerializeField]
    protected float moveSpeed;             //투사체 속도

    [SerializeField]
    protected LayerMask hitLayer;          //충돌되는 레이어들

    [SerializeField]
    private float maxMoveDistance = 20000.0f;
    private float moveDistance;
    public float MaxMoveDistance { get { return maxMoveDistance; } }
    public float MoveDistance { set { moveDistance = value; } }
    //protected Vector3 startPosition;

    protected SphereCollider col;   //컬라이더 정보
    protected Vector3 moveDir;             //이동 방향
    protected Vector3 prevPosition;        //이전 위치
    protected RaycastHit hitInfo;        //충돌 정보    
    protected Transform transform;
    protected virtual void Awake()
    {
        //Init();
        PoolObject pool = GetComponent<PoolObject>();
        if(pool != null)
        {
          pool.Init = Init;
        }

        transform = this.GetComponent<Transform>();
        col = GetComponent<SphereCollider>();
        moveDistance = maxMoveDistance;
        prevPosition = transform.position;

    }

    /// <summary>
    /// 총알 초기화
    /// </summary>
    public virtual void Init()
    {
        //if(transform == null)
        //{
        //    transform = this.GetComponent<Transform>();
        //}
        prevPosition = transform.position;
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
        float _radius = col.radius;
        //Vector3 _colDir;
        //Vector3 _p1;
        //Vector3 _p2;

        //충돌 확인할 캡슐 컬라이더 (방향)
        //if (col.direction == 0) _colDir = transform.right;
        //else if (col.direction == 1) _colDir = transform.up;
        //else _colDir = transform.forward;

        //충돌 확인할 캡슐컬라이더 정점
        //_p1 = prevPosition + Matrix4x4.Scale(transform.localScale).MultiplyPoint3x4(_colDir * col.height * 0.5f);
        //_p2 = prevPosition - Matrix4x4.Scale(transform.localScale).MultiplyPoint3x4(_colDir * col.height * 0.5f);
        //Physics.SphereCast()
        //hitInfo = Physics.CapsuleCastAll(_p1, _p2, capsuleCol.radius * transform.localScale.y, _dir, _dir.magnitude, hitLayer);

        Ray ray = new Ray(prevPosition, _dir);
        return Physics.SphereCast(ray, _radius, out hitInfo, moveSpeed* Time.fixedDeltaTime, hitLayer);
        //return Physics.CapsuleCast(_p1, _p2, col.radius * transform.localScale.y, _dir, out hitInfo, moveSpeed * Time.fixedDeltaTime, hitLayer);
    }

    private void FixedUpdate()
    {
        if (CollisionCheck())
            OnCollisionEvent();

        if (IsMaxDistance())
            PoolManager.Instance.PushObject(this.gameObject);

        prevPosition = transform.position;
        Move();

        moveDistance -= moveSpeed * Time.fixedDeltaTime;
    }

    protected virtual void DestroyHitBullet()
    {
        GameObject particle;
        if(destroyParticle != null)
        {
            PoolManager.Instance.PopObject(destroyParticle, out particle);
            particle.transform.position = hitInfo.point;
            particle.transform.rotation = Quaternion.LookRotation(hitInfo.normal, Vector3.up);
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
            particle.transform.position = transform.position;
            //particle.transform.rotation = Quaternion.LookRotation(hitInfo[0].normal, Vector3.up);
        }
        else
        {
            Debug.LogWarning(this.gameObject.name + "Not Found Destroy Particle");
        }
        PoolManager.Instance.PushObject(this.gameObject);
    }

    public virtual void FireEvent()
    { 
    }

}
