using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSkill : MonoBehaviour {

    public float maxSize = 100.0f;
    public float time = 10.0f;

    private float currentSize;
    private float currentTime;

	// Use this for initialization
	void Awake ()
    {
        currentTime = 0.0f;
        currentSize = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentTime += Time.fixedUnscaledDeltaTime;
        currentSize = currentTime * maxSize / time;
        this.transform.localScale = new Vector3(currentSize, 5.0f, currentSize);

        if(currentTime >= time)
        {
            currentTime = 0.0f;
            PoolManager.Instance.PushObject(this.gameObject);
        }

	}
}
