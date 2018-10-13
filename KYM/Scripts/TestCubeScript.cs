using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            BulletManager.Instance.CreateDragonMeteoBullet(transform, 30, 10);
        }
	}
}
