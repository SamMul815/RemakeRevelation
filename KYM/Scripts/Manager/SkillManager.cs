using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : Singleton<SkillManager> {

    public GameObject leftSkill;
    public GameObject rightSkill;

    public void UseLeftSkill()
    {
        leftSkill.SetActive(true);
    }

    public void UseRightSkill()
    {
        rightSkill.SetActive(true);
    }

}
