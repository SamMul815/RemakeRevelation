using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolParticle : PoolObject {

    public float waitTime = 5.0f;
    private float currentTime;
	// Use this for initialization
	void Awake ()
    {
        Init = InitParticle;
        currentTime = waitTime;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentTime <= 0.0f)
        {
            PoolManager.Instance.PushObject(this.gameObject);
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    void InitParticle()
    {
        currentTime = waitTime;
    }


}
