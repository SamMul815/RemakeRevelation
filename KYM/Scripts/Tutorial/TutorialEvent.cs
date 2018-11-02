using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : Singleton<TutorialEvent>
{

    private TutorialHand leftHand;
    private TutorialHand rightHand;

    public TutorialHand LeftHand
    {
        get
        {
            if(leftHand != null)
            {
                return leftHand;
            }
            return null;
        }
        set
        {
            leftHand = value;
        }
    }

    public TutorialHand RightHand
    {
        get
        {
            if(rightHand != null)
            {
                return rightHand;
            }
            return null;
        }
        set
        {
            rightHand = value;
        }
    }

    public Material[] npcMaterials;
    public float minValue;
    public float maxValue;
    public float onTime;


    public void OnNPC()
    {
        StartCoroutine(corOnNPC());
    }

    public void OffNPC()
    {
        for (int i = 0; i < npcMaterials.Length; i++)
        {
            npcMaterials[i].SetFloat("_warf", minValue);
        }
    }

    IEnumerator corOnNPC()
    {
        //NPC 등장
        for (float time = 0.0f; time < onTime; time += Time.unscaledDeltaTime)
        {
            for (int i = 0; i < npcMaterials.Length; i++)
            {
                npcMaterials[i].SetFloat("_warf", Mathf.Lerp(minValue, maxValue, time / onTime));
            }
            yield return new WaitForEndOfFrame();
        }
    }



}
