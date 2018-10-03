using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMachinGun : MonoBehaviour {

    public Transform firepos;
    PlayerHand rightHand;
    PlayerHand leftHand;

    IEnumerator shot;

	// Use this for initialization
	void Start ()
    {
		rightHand = Player.instance.rightHand;
        leftHand = Player.instance.leftHand;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = rightHand.transform.position;
        this.transform.rotation = 
            Quaternion.LookRotation(leftHand.transform.position - rightHand.transform.position, Vector3.up);

        if(rightHand.GetTriggerButtonDown())
        {
            shot = corShot();
            StartCoroutine(shot);
        }
        else if(rightHand.GetTriggerButtonUp())
        {
            StopCoroutine(shot);
        }
      
	}

    IEnumerator corShot()
    {
        while(true)
        {
            BulletManager.Instance.CreatePlayerBaseBullet(firepos);

            rightHand.Vibration(0.1f,4000.0f);
            leftHand.Vibration(0.1f,4000.0f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }


}
