using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathTriggerOn : MonoBehaviour {

    public float waitTime = 0.15f;
    public float closeTime = 3.0f;
    public float damage = 2.0f;
    private float currentWaitTime;
    private float currentCloseTime;

    Collider col;


    private void OnEnable()
    {
        col = this.GetComponent<Collider>();
        col.enabled = false;
        currentWaitTime = waitTime;
        currentCloseTime = closeTime;
    }

    // Update is called once per frame
    void Update ()
    {
		if(currentWaitTime > 0.0f && currentCloseTime > 0.0f)
        {
            currentWaitTime -= Time.deltaTime;
        }
        else
        {
            col.enabled = true;
        }

        if(currentCloseTime > 0.0f)
        {
            currentCloseTime -= Time.deltaTime;
        }
        else
        {
            col.enabled = false;
        }

	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.instance.playerStat.dotHit(damage);
        }
    }

}
