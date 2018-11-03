using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMove : TutorialBase
{
    public GameObject tutorialMoveObjects;
    public TutorialTrigger moveTrigger;
    public GameObject TeleportPointer;
    public GameObject MovePoint;
    public TutorialDoor door;
    private bool isTeleport;

    private void OnEnable()
    {
        isTeleport = false;
        tutorialMoveObjects.SetActive(true);
        TeleportPointer.SetActive(false);
        MovePoint.SetActive(false);
        StartCoroutine(corMove());
    }

    private void OnDisable()
    {
        tutorialMoveObjects.SetActive(false);
    }

    protected override bool IsClear()
    {
        return moveTrigger.IsClear();
    }

    IEnumerator corMove()
    {
        TutorialHand leftHand;
        TutorialHand rightHand;
        //연결 대기
        while (true)
        {
            leftHand = TutorialEvent.Instance.LeftHand;
            rightHand = TutorialEvent.Instance.RightHand;

            if (leftHand != null  || rightHand != null)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        //시작 사운드 재생 
        //사운드 재생 대기
        yield return new WaitForSecondsRealtime(3.0f);

        while(true)
        {
            if(leftHand.enabled && rightHand.enabled)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        TeleportPointer.SetActive(true);
        MovePoint.SetActive(true);
        door.Open();
        TutorialEvent.Instance.LeftHand.HighlightOnButton("trackpad");
        TutorialEvent.Instance.RightHand.HighlightOnButton("trackpad");
        isTeleport = false;

        while (true)
        {
            Player.instance.leftHand.Vibration(0.1f, 2000);
            Player.instance.rightHand.Vibration(0.1f, 2000);

            if(isTeleport)
            {
                break;
            }
            yield return new WaitForSecondsRealtime(0.3f);
        }
        TutorialEvent.Instance.LeftHand.HighlightOffButton("trackpad");
        TutorialEvent.Instance.RightHand.HighlightOffButton("trackpad");
 
    }

    protected override void Update()
    {
        base.Update();
        if(Player.instance.leftHand.GetTouchPadUp() || 
            Player.instance.rightHand.GetTouchPadUp())
        {
            isTeleport = true;
        }
    }

}
