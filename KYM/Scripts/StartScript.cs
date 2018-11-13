using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        LoadSceneManager.Instance.LoadTitle(1.0f);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
