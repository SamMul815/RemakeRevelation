using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSlowTarget : MonoBehaviour {

    public float moveDistance;
    public float moveSpeed;
    public float currentMoveDistance;
    

    private bool isRight = true;
    private Vector3 centerPos;
	// Use this for initialization
	void Start ()
    {
        currentMoveDistance = 0.0f;
        centerPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (currentMoveDistance > moveDistance)
        {
            isRight = false;
        }
        if (currentMoveDistance < -moveDistance)
        {
            isRight = true;
        }

        if (isRight)
        {
            currentMoveDistance += moveSpeed * Time.deltaTime;
        }
        else
        {
            currentMoveDistance -= moveSpeed * Time.deltaTime;
        }


        this.transform.position =centerPos+ this.transform.right * currentMoveDistance;
	}
}
