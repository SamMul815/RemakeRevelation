using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;
[RequireComponent(typeof(Collider))]
public class WeakPoint : MonoBehaviour {

    public GameObject baseObject;
    public GameObject brokenObject;
    public GameObject throwablePrefab;
    public GameObject brokenEffectPrefab;
    //public Transform attachPoint;


    public float maxHP;
    private float currentHP;
    private Collider col;
    private Transform weakTransform;
    private DragonManager dragonManager;
    // Use this for initialization
	void Awake ()
    {

        if(baseObject != null)
            baseObject.SetActive(true);

        if(brokenObject != null)
            brokenObject.SetActive(false);

        currentHP = maxHP;
        col = GetComponent<Collider>();
        //GetComponents<Collider>
        weakTransform = this.transform;

        dragonManager = DragonManager.Instance;

        //if (attachPoint == null)
        //{
        //    attachPoint = this.transform;
        //}
        //else
        //{
        //    //weakTransform.parent = attachPoint;
        //    //weakTransform.localPosition = Vector3.zero;
        //    //weakTransform.localRotation = Quaternion.identity;
        //    baseObject.transform.parent = attachPoint;
        //    //baseObject.transform.localPosition = Vector3.zero;
        //    //baseObject.transform.localRotation = Quaternion.identity;
        //}

    }	

    protected virtual void BreakEvent()
    {
        if (brokenObject != null)
        {
            brokenObject.SetActive(true);
            brokenObject.transform.parent = baseObject.transform.parent;
        }
        if (baseObject != null)
        {
            baseObject.SetActive(false);
        }
        if (throwablePrefab != null)
        {
            GameObject throwableObject;
            PoolManager.Instance.PopObject(throwablePrefab, out throwableObject);
            throwableObject.transform.position = weakTransform.position;
        }

        if (brokenEffectPrefab != null)
        {
            GameObject effectObject;
            PoolManager.Instance.PopObject(brokenEffectPrefab, out effectObject);
            effectObject.transform.position = weakTransform.position;
        }

        this.enabled = false;
        //this.gameObject.SetActive(false);
    }

    public void Hit(float damage)
    {
        currentHP -= damage;

        if(dragonManager != null)
        {
            dragonManager.Hit(damage);
        }

        if(currentHP <= 0.0f)
        {
            BreakEvent();
            //dragonManager.OnDestroyPart(damage * 10);
        }
    }
}
