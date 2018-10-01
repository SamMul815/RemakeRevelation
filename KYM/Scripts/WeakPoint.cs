using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeakPoint : MonoBehaviour {

    public float maxHP;
    public GameObject hideObject;
    public GameObject throwablePrefab;
    public GameObject breakEffect;
    public Transform attachPoint;
    private float currentHP;
    private Collider col;
	// Use this for initialization
	void Awake ()
    {
        currentHP = maxHP;
        col = this.GetComponent<Collider>();
        if (attachPoint == null)
            attachPoint = this.transform;
	}	
	// Update is called once per frame
	void Update ()
    {
	    if(ISBreak())
        {
            BreakEvent();
            col.enabled = false;
        }
	}

    protected virtual void BreakEvent()
    {
        if(breakEffect != null)
        {
            GameObject effectObject;
            PoolManager.Instance.PopObject(breakEffect, out effectObject);
            effectObject.transform.position = attachPoint.position;
        }

        if(throwablePrefab != null)
        {
            GameObject throwableObject;
            PoolManager.Instance.PopObject(throwablePrefab, out throwableObject);
            throwableObject.transform.position = attachPoint.position;
        }
        if(hideObject != null)
        {
            hideObject.SetActive(false);
        }
        this.enabled = false;

        //effectObject.transform.rotation = attachPoint.rotation;
        //Instantiate(throwablePrefab);
    }

    public void Hit(float damage)
    {
        currentHP -= damage;
    }

    bool ISBreak() { return currentHP <= 0.0f; }

}
