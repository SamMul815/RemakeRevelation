using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClear : TutorialBase
{
    public GameObject ClearObjects;
    public TutorialTrigger centerTrigger;

    private void OnEnable()
    {
        ClearObjects.SetActive(true);
    }

    private void OnDisable()
    {
        ClearObjects.SetActive(false);
    }

    protected override bool IsClear()
    {
        if(centerTrigger.IsClear())
        {
            
            FmodManager.Instance.PlaySoundOneShot(this.transform.position, "AI15");
            return true;
        }
        return false;
    }


}
