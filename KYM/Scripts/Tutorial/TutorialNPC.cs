using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPC : MonoBehaviour {

    public enum NPCState
    {
        IDLE,
        THROW,
        MACHINE,
        BASEGUN 
    }

    public Material[] npcTelMaterials;
    public Material[] npcBaseMaterials;

    public float minValue;
    public float maxValue;
    public float onTime;
    public NPCState AniType;

    public SkinnedMeshRenderer meshRenderer;
    public Animator anim;

    public GameObject GunLeft;
    public GameObject GunRight;
    public GameObject MachineGun;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        GunLeft.SetActive(false);
        GunRight.SetActive(false);
        MachineGun.SetActive(false);
    }

    private void Start()
    {
        switch (AniType)
        {
            case NPCState.IDLE:
                GunLeft.SetActive(true);
                GunRight.SetActive(true);
                break;
            case NPCState.THROW:
                break;
            case NPCState.MACHINE:
                MachineGun.SetActive(true);
                break;
            case NPCState.BASEGUN:
                GunLeft.SetActive(true);
                GunRight.SetActive(true);
                break;
            default:
                break;
        }
    }


    void Update ()
    {
        switch (AniType)
        {
            case NPCState.IDLE:
                break;
            case NPCState.THROW:
                anim.SetTrigger("Throw");
                break;
            case NPCState.MACHINE:
                anim.SetTrigger("MachineGun");
                break;
            case NPCState.BASEGUN:
                anim.SetTrigger("BaseGun");
                break;
            default:
                break;
        }
    }

    public void OnNPC()
    {
        StartCoroutine(corOnNPC());
    }

    IEnumerator corOnNPC()
    {
        meshRenderer.materials = npcTelMaterials;
        //NPC 등장
        for (float time = 0.0f; time < onTime; time += Time.unscaledDeltaTime)
        {
            for (int i = 0; i < npcTelMaterials.Length; i++)
            {
                npcTelMaterials[i].SetFloat("_warf", Mathf.Lerp(minValue, maxValue, time / onTime));
            }
            yield return new WaitForEndOfFrame();
        }

        //meshRenderer.materials = npcBaseMaterials;

    }
}
