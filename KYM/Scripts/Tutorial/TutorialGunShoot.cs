using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGunShoot : TutorialBase {

    public GameObject tutorialGunShootObjects;

    public TutorialTarget target1;
    public TutorialTarget target2;
    public TutorialTarget target3;

    private void OnEnable()
    {
        tutorialGunShootObjects.SetActive(true);
        StartCoroutine(corGunShoot());
    }

    private void OnDisable()
    {
        TutorialEvent.Instance.OffNPC();
        tutorialGunShootObjects.SetActive(false);
    }

    protected override bool IsClear()
    {
        if(target1.IsDie() && target2.IsDie() && target3.IsDie())
        {
            return true;
        }
        return false;
    }

    IEnumerator corGunShoot()
    {
        TutorialEvent.Instance.OnNPC();
        yield return null;
    }

}
