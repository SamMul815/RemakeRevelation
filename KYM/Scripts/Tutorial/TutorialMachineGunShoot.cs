using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMachineGunShoot : TutorialBase
{
    public GameObject tutorialMachineGunShootObjects;
    public GameObject target;
    public MachinGun machinGun;
    public TutorialNPC tutorialNPC;
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
        
        TutorialEvent.Instance.RightHand.gameObject.SetActive(true);

        TutorialEvent.Instance.RightHand.HighlightOnButton("lgrip");
        TutorialEvent.Instance.RightHand.HighlightOnButton("rgrip");
        tutorialMachineGunShootObjects.SetActive(true);
        //TutorialEvent.Instance.OnNPC();
        tutorialNPC.OnNPC();
        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI07");

        while (true)
        {
            Player.instance.rightHand.Vibration(0.1f, 4000);

            if (isMachineGun)
            {
                break;
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
        TutorialEvent.Instance.RightHand.HighlightOffButton("lgrip");
        TutorialEvent.Instance.RightHand.HighlightOffButton("rgrip");



        target.SetActive(true);

        while(true)
        {
            if(machinGun.CurrentGauge > 0.0f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI08");
        TutorialEvent.Instance.RightHand.HighlightOnButton("trigger");

        while (true)
        {
            if(machinGun.CurrentGauge <=0.0f)
            {
                TutorialEvent.Instance.RightHand.HighlightOffButton("trigger");
                TutorialEvent.Instance.RightHand.gameObject.SetActive(false);
                break;
            }
            else
            {
                Player.instance.rightHand.Vibration(0.1f, 4000);
                yield return new WaitForSecondsRealtime(0.2f);
            }
            //yield return new WaitForEndOfFrame();
        }

        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI09");
        yield return new WaitForSecondsRealtime(1.0f);
        target.SetActive(false);
        isClear = true;

        yield return null;
    }
}
