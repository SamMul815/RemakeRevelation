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
        TutorialEvent.Instance.RightGun.SetSkillCoolTime(999.0f);
    }

    private void OnDisable()
    {
        TutorialEvent.Instance.OnReload();
        TutorialEvent.Instance.LeftGun.SetCurrentBullet(10);
        TutorialEvent.Instance.RightGun.SetCurrentBullet(10);
        tutorialThrowObjects.SetActive(false);
    }

    protected override bool IsClear()
    {
        return targetObject.IsDie();
        //return base.IsClear();
    }

}
