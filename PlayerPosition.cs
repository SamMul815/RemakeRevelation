using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour {

    public Transform Player;
    public Transform Head;

	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = Head.position;
        pos.y = Player.position.y;
	}
}
