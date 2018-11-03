using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoor : MonoBehaviour
{

    public GameObject LeftDoor;
    public GameObject RightDoor;

    public float openTime = 0.5f;
    public float moveDistance = 1.25f;

    public bool isOpen = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Open();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            Close();
        }
    }

    public void Open()
    {
        StartCoroutine(corOpenDoor());
        
    }

    public void Close()
    {
        StartCoroutine(corCloseDoor());
    }

    IEnumerator corOpenDoor()
    {
        if (isOpen) yield break;

        Vector3 leftPos = LeftDoor.transform.localPosition;
        Vector3 rightPos = RightDoor.transform.localPosition;

        Vector3 leftMovePos = leftPos - new Vector3(0, 0, moveDistance);
        Vector3 rightMovepos = rightPos + new Vector3(0, 0, moveDistance);

        for (float t = 0.0f; t < openTime; t += Time.fixedDeltaTime)
        {
            LeftDoor.transform.localPosition = Vector3.Lerp(leftPos, leftMovePos, t / openTime);
            RightDoor.transform.localPosition = Vector3.Lerp(rightPos, rightMovepos, t / openTime);
            yield return new WaitForFixedUpdate();
        }
        LeftDoor.transform.localPosition = leftMovePos;
        RightDoor.transform.localPosition = rightMovepos;
        isOpen = true;
    }

    IEnumerator corCloseDoor()
    {
        if (!isOpen) yield break;


        Vector3 leftPos = LeftDoor.transform.localPosition;
        Vector3 rightPos = RightDoor.transform.localPosition;

        Vector3 leftMovePos = leftPos + new Vector3(0, 0, moveDistance);
        Vector3 rightMovepos = rightPos - new Vector3(0, 0, moveDistance);

        for (float t = 0.0f; t < openTime; t += Time.fixedDeltaTime)
        {
            LeftDoor.transform.localPosition = Vector3.Lerp(leftPos, leftMovePos, t / openTime);
            RightDoor.transform.localPosition = Vector3.Lerp(rightPos, rightMovepos, t / openTime);
            yield return new WaitForFixedUpdate();
        }
        LeftDoor.transform.localPosition = leftMovePos;
        RightDoor.transform.localPosition = rightMovepos;
        isOpen = false;
    }
}
