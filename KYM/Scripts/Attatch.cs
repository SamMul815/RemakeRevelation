using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attatch : MonoBehaviour {

    Transform target;

	
	// Update is called once per frame
	void Update ()
    {
        if(target != null)
        {
            transform.position = target.transform.position;
            transform.rotation = target.transform.rotation;
        }
	}
}
