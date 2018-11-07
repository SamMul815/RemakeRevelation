using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialThrowable : TutorialBase
{
    public GameObject tutorialThrowObjects;
    public TutorialTarget targetObject;

    private void OnEnable()
    {
        tutorialThrowObjects.SetActive(true);
        TutorialEvent.Instance.LeftGun.SetCurrentBullet(0);
        TutorialEvent.Instance.RightGun.SetCurrentBullet(0);
        TutorialEvent.Instance.OffReload();
    }

    private void OnDisable()
    {
        TutorialEvent.Instance.OnReload();
        tutorialThrowObjects.SetActive(false);
    }

    protected override bool IsClear()
    {
        targetObject.IsDie();
        return base.IsClear();
    }

    //IEnumerator corThrowable()
    //{
    //    yield return null;
    //}

}
