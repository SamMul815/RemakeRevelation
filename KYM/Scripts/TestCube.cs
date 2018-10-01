using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour {

    public float FireTime = 5.0f;
    private float currentTime = 5.0f;
    public int amount = 100;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BulletManager.Instance.CreateDragonBreath(this.transform.position, this.transform.forward);
            //BulletManager.Instance.CreateDragonSlashSkill(this.transform.position);
            //currentTime = FireTime;
        }

        currentTime -= Time.deltaTime;

	}
}
