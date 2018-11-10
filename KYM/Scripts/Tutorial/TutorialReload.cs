using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialReload : TutorialBase
{
    public GameObject tutorialGunPrefab;

    public GameObject tutorialReloadObjects;

    public TutorialNPC tutorialNPC;

    GameObject leftGun;
    GameObject rightGun;
    private const PlayerHand.AttachmentFlags attachmentFlags = 
                 PlayerHand.AttachmentFlags.ParentToHand | 
                 PlayerHand.AttachmentFlags.SnapOnAttach;

    private void OnEnable()
    {
        TutorialEvent.Instance.OffReload();
        tutorialReloadObjects.SetActive(true);
        leftGun = Instantiate(tutorialGunPrefab);
        rightGun = Instantiate(tutorialGunPrefab);
        Player.instance.leftHand.AttachObject(leftGun, attachmentFlags);
        Player.instance.rightHand.AttachObject(rightGun, attachmentFlags);

        TutorialEvent.Instance.LeftGun = leftGun.GetComponent<Gun>();
        TutorialEvent.Instance.RightGun = rightGun.GetComponent<Gun>();

        TutorialEvent.Instance.LeftHand.gameObject.SetActive(false);
        TutorialEvent.Instance.RightHand.gameObject.SetActive(false);

        StartCoroutine(corReload());
    }

    private void OnDisable()
    {
        //TutorialEvent.Instance.OffNPC();
        tutorialReloadObjects.SetActive(false);
    }

    protected override bool IsClear()
    {
        if (leftGun == null | rightGun == null) return false;

        if(leftGun.GetComponent<Gun>().CurrentBullet != 0 &&
            rightGun.GetComponent<Gun>().CurrentBullet != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator corReload()
    {
        tutorialNPC.OnNPC();
        //장전박스 ON
        TutorialEvent.Instance.OnReload();
        FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI03");
        //NPC 등장 끝
        yield return new WaitForSeconds(2.0f);


        //ReloadBox1.SetActive(true);
        //ReloadBox2.SetActive(true);

        yield return null;
    }


}
