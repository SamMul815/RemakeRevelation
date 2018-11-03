using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMachineGunMove : TutorialBase
{
    public GameObject tutorialMachineGunObjects;
    public TutorialDoor openDoor;
    public TutorialDoor closeDoor;
    public TutorialTrigger movePoint;

    private void OnEnable()
    {
        tutorialMachineGunObjects.SetActive(true);
        StartCoroutine(corMachineGunMove());        
    }

    private void OnDisable()
    {
        closeDoor.Close();
        tutorialMachineGunObjects.SetActive(false);
        TutorialEvent.Instance.OffNPC();
    }

    protected override bool IsClear()
    {
        return movePoint.IsClear();  
    }

    IEnumerator corMachineGunMove()
    {
        openDoor.Open();
        yield return null;
    }
}
