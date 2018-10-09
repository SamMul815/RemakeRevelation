using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachingunAim : MonoBehaviour {

    //public GameObject aim;
   public GameObject playerHead;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 dir = transform.position - playerHead.transform.position; //playerHead.transform.forward;
        transform.rotation = Quaternion.LookRotation(dir,Vector3.up);
	}
}
