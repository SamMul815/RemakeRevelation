using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMachineGunShoot : TutorialBase
{
    public GameObject tutorialMachineGunShootObjects;
    
    private void OnEnable()
    {
        StartCoroutine(corMachinGunShoot());
        TutorialEvent.Instance.OnNPC();
        tutorialMachineGunShootObjects.SetActive(true);
    }

    private void OnDisable()
    {
        tutorialMachineGunShootObjects.SetActive(false);
    }

    protected override bool IsClear()
    {
        return base.IsClear();
    }

    IEnumerator corMachinGunShoot()
    {
        TutorialEvent.Instance.RightGun.SetSkillCoolTime(0.0f);
        yield return null;
    }



}
