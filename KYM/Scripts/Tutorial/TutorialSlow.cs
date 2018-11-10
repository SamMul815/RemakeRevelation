using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSlow : TutorialBase
{
    public GameObject tutorialSlowObjects;
    //public GameObject tutorialClearObjects;

    bool isGrip = false;
    bool isClear = false;
    private void OnEnable()
    {
        tutorialSlowObjects.SetActive(true);
        TutorialEvent.Instance.LeftGun.SetSkillCoolTime(0.0f);
        StartCoroutine(corSlowTutorial());
    }

    private void OnDisable()
    {
        tutorialSlowObjects.SetActive(false);
        //tutorialClearObjects.SetActive(true);
    }

    protected override bool IsClear()
    {
        return isClear;
    }

    protected override void Update()
    {
        if(Player.instance.leftHand.GetGripButton())
        {
            isGrip = true;
        }
        base.Update();
    }

    IEnumerator corSlowTutorial()
    {
        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI13");

        while (!isGrip)
        {
           yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSecondsRealtime(7.0f);
        isClear = true;
        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI14");

    }
}
