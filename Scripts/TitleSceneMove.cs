using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneMove : MonoBehaviour
{
    public float TrunSpeed = 10.0f;

    float _curTrun;
    public float MaxTrun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_curTrun >= MaxTrun)
        {
            MaxTrun *= -1;
        }
        transform.Rotate(transform.up, TrunSpeed * Time.deltaTime, Space.World);
		
	}
}
