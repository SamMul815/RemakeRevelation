using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGunShoot : TutorialBase {

    public GameObject tutorialGunShootObjects;

    public TutorialTarget target1;
    public TutorialTarget target2;

    private void OnEnable()
    {
        tutorialGunShootObjects.SetActive(true);
        StartCoroutine(corGunShoot());
    }

    private void OnDisable()
    {
        //TutorialEvent.Instance.OffNPC();
        tutorialGunShootObjects.SetActive(false);
    }

    protected override bool IsClear()
    {
        if(target1.IsDie() && target2.IsDie())
        {
            FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI06");
            return true;
        }
        return false;
    }

    IEnumerator corGunShoot()
    {
        //TutorialEvent.Instance.OnNPC();
        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI04");
        yield return new WaitForSeconds(1.0f);
        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI05");


        yield return null;
    }

}
