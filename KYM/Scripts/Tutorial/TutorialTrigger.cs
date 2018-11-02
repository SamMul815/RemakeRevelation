using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    public bool isClear = false;

    //public Event unityEvent;
    //public TutorialEvent.TutorialState changeState;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isClear = true;
        }
    }

    public bool IsClear() { return isClear; }


}
