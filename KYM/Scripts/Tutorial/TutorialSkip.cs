using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSkip : MonoBehaviour {
    public string[] sceneNames;
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.F12))
        {
            LoadSceneManager.Instance.LoadScenes(2.0f, sceneNames);
        }
	}
}
