using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager> {

    public GameObject leftSkill;
    public GameObject rightSkill;

    public GameObject DragonLeftPaw;
    public GameObject DragonRightPaw;


    public void UseLeftSkill()
    {
        leftSkill.SetActive(true);
    }

    public void UseRightSkill()
    {
        rightSkill.SetActive(true);
    }

    public void UseDragonRightPaw()
    {
        DragonRightPaw.SetActive(true);
    }

    public void UseDragonLefttPaw()
    {
        DragonLeftPaw.SetActive(true);
    }

    public void DragonLeftPawOff()
    {
        DragonLeftPaw.SetActive(false);
    }

    public void DragonRighttPawOff()
    {
        DragonRightPaw.SetActive(false);
    }

}
