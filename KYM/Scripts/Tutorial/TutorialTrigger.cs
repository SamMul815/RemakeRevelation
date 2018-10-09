using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    //public Event unityEvent;
    public TutorialEvent.TutorialState changeState;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TutorialEvent.Instance.EventChange(changeState);
            this.gameObject.SetActive(false);
            
        }
    }

}
