using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialThrowable : TutorialBase
{
    public GameObject tutorialThrowObjects;
    public TutorialTarget targetObject;
    public TutorialNPC tutorialNPC;
    public bool isClear = false;

    private void OnEnable()
    {
        tutorialThrowObjects.SetActive(true);
        TutorialEvent.Instance.LeftGun.SetCurrentBullet(0);
        TutorialEvent.Instance.RightGun.SetCurrentBullet(0);
        TutorialEvent.Instance.OffReload();
        TutorialEvent.Instance.RightGun.SetSkillCoolTime(999.0f);
        StartCoroutine(corThrow());
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
        return isClear;
        //return targetObject.IsDie();
        //return base.IsClear();
    }

    IEnumerator corThrow()
    {
        tutorialNPC.OnNPC();
        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI10");
        yield return new WaitForSecondsRealtime(2.0f);

        while(true)
        {
            if (!TutorialEvent.Instance.LeftGun.gameObject.activeInHierarchy |
                !TutorialEvent.Instance.RightGun.gameObject.activeInHierarchy)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI11");

        while(true)
        {
            if(targetObject.IsDie())
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI12");

        yield return new WaitForSecondsRealtime(1.0f);
        isClear = true;

        yield return null;
    }

}
