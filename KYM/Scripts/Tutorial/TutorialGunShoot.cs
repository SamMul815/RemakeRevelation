using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGunShoot : TutorialBase {

    public GameObject tutorialGunShootObjects;

    private void OnEnable()
    {
        tutorialGunShootObjects.SetActive(true);
        StartCoroutine(corGunShoot());
    }

    private void OnDisable()
    {
        tutorialGunShootObjects.SetActive(false);
    }

    IEnumerator corGunShoot()
    {
        TutorialEvent.Instance.OnNPC();
        yield return null;
    }

}
