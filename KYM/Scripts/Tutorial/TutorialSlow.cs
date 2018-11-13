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

        TutorialEvent.Instance.LeftHand.gameObject.SetActive(true);
        TutorialEvent.Instance.RightHand.gameObject.SetActive(true);

        TutorialEvent.Instance.LeftHand.HighlightOnButton("lgrip");
        TutorialEvent.Instance.LeftHand.HighlightOnButton("rgrip");

        while (true)
        {
            Player.instance.leftHand.Vibration(0.1f, 4000);

            if (isGrip)
            {
                break;
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
        TutorialEvent.Instance.LeftHand.HighlightOffButton("lgrip");
        TutorialEvent.Instance.LeftHand.HighlightOffButton("rgrip");

        TutorialEvent.Instance.LeftHand.gameObject.SetActive(false);
        TutorialEvent.Instance.RightHand.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(7.0f);
        isClear = true;
        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI14");

    }
}
