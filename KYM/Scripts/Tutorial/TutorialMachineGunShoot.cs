using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMachineGunShoot : TutorialBase
{
    public GameObject tutorialMachineGunShootObjects;
    public GameObject target;
    public MachinGun machinGun;
    private bool isMachineGun = false;

    public bool isClear = false;

    private void OnEnable()
    {
        tutorialMachineGunShootObjects.SetActive(false);
        target.SetActive(false);
        StartCoroutine(corMachinGunShoot());
    }

    private void OnDisable()
    {
        tutorialMachineGunShootObjects.SetActive(false);
    }

    protected override bool IsClear()
    {
        return isClear;
    }

    protected override void  Update()
    {
        base.Update();
        if(Player.instance.rightHand.GetGripButton())
        {
            isMachineGun = true;
        }
    }

    IEnumerator corMachinGunShoot()
    {
        TutorialEvent.Instance.RightGun.SetSkillCoolTime(0.0f);
        
        TutorialEvent.Instance.LeftHand.gameObject.SetActive(true);
        TutorialEvent.Instance.RightHand.gameObject.SetActive(true);

        TutorialEvent.Instance.RightHand.HighlightOnButton("lgrip");
        TutorialEvent.Instance.RightHand.HighlightOnButton("rgrip");
        tutorialMachineGunShootObjects.SetActive(true);
        TutorialEvent.Instance.OnNPC();
        while (true)
        {
            Player.instance.rightHand.Vibration(0.1f, 4000);

            if (isMachineGun)
            {
                break;
            }
            yield return new WaitForSecondsRealtime(0.3f);
        }
        TutorialEvent.Instance.RightHand.HighlightOffButton("lgrip");
        TutorialEvent.Instance.RightHand.HighlightOffButton("rgrip");

        TutorialEvent.Instance.LeftHand.gameObject.SetActive(false);
        TutorialEvent.Instance.RightHand.gameObject.SetActive(false);

        target.SetActive(true);

        while(true)
        {
            if(machinGun.CurrentGauge > 0.0f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        while(true)
        {
            if(machinGun.CurrentGauge <=0.0f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        target.SetActive(false);
        isClear = true;



        //TutorialEvent.Instance.OnNPC();

        yield return null;
    }
}
