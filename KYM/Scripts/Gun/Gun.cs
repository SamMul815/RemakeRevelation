using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(GunAnimation))]
public class Gun : MonoBehaviour
{
    public enum GunType
    {
        Left,
        Right
    }
    public GunType gunType;
    [SerializeField]private int maxBullet;
    public int MaxBullet { get { return maxBullet; } }
    public float fireDelay;
    public Transform firePos;

    public GameObject handLeft;
    public GameObject handRight;

    public GameObject UILeft;
    public GameObject UIRight;

    public float rightSkillCoolTime;
    public float leftSkillCoolTime;
    private float skillCoolTime;
    public GameObject magazine;
    public GameObject cartridgePrefab;
    public GameObject muzzlePrefab;

    private int currentBullet;
    public int CurrentBullet { get { return currentBullet; } }
    private float fireCoolTime;
    private Animator handAnimator;
    private PlayerHand playerHand;

    private float currentSkillCoolTime;
    public bool GetCanSkill { get { return currentSkillCoolTime <= 0.0f; } }

    public bool IsTutorial = false;

    private IEnumerator corTriggerAni;
    private bool canFire;
    private void Awake()
    {
        if(!IsTutorial)
        {
            currentBullet = maxBullet;
            fireCoolTime = 0.0f;
            currentSkillCoolTime = 0.0f;
        }
        else
        {
            currentBullet = 0;
            fireCoolTime = 0.0f;
            currentSkillCoolTime = 9999.0f;
        }

    }
    
	// Update is called once per  frame
	void Update ()
    {       
	    if(fireCoolTime > 0.0f)
            fireCoolTime -= Time.unscaledDeltaTime;

        if (currentSkillCoolTime > 0.0f)
            currentSkillCoolTime -= Time.unscaledDeltaTime;
    }

    private void Fire(PlayerHand hand)
    {
        if(firePos == null || fireCoolTime > 0.0f )
        {
            if(firePos == null)
                Debug.LogWarning("Not FirePos");
            return;
        }

        if(currentBullet <= 0)
        {
            FmodManager.Instance.PlaySoundOneShot(transform.position, "Ammo");
            return;
        }

        FmodManager.Instance.PlaySoundOneShot(transform.position, "Gun");
        BulletManager.Instance.CreatePlayerBaseBullet(firePos);
        fireCoolTime = fireDelay;
        currentBullet -= 1;

        GameObject muzzle;
        PoolManager.Instance.PopObject(muzzlePrefab,firePos.position,firePos.rotation,out muzzle);
        hand.Vibration(0.15f, 4000);
    }

    public void Reload()
    {
        currentBullet = maxBullet;
    }

    private void OnEnable()
    {
        if(corTriggerAni == null)
        {
            corTriggerAni = CorTriggerAxisAnim();
            StartCoroutine(corTriggerAni);
        }

    }

    private void OnDisable()
    {
        if(corTriggerAni != null)
        {
            StopCoroutine(corTriggerAni);
            corTriggerAni = null;
        }
    }


    private void OnAttachedToHand(PlayerHand hand)
    {
        playerHand = hand;
        canFire = true;
        if (hand.GetHandType() == PlayerHand.HandType.Right)
        {
            gunType = GunType.Right;
            handRight.SetActive(true);
            handLeft.SetActive(false);
            UILeft.SetActive(false);
            UIRight.SetActive(true);
            handAnimator = handRight.GetComponent<Animator>();
        }
        else if(hand.GetHandType() == PlayerHand.HandType.Left)
        {
            gunType = GunType.Left;
            handLeft.SetActive(true);
            handRight.SetActive(false);
            UILeft.SetActive(true);
            UIRight.SetActive(false);
            handAnimator = handLeft.GetComponent<Animator>();
        }
        //StartCoroutine(CorTriggerAxisAnim());
    }

    private void HandAttachedUpdate(PlayerHand hand)
    {
        if(currentSkillCoolTime > 0.0f)
        {
            currentSkillCoolTime -= Time.unscaledDeltaTime;
        }
        if (hand.GetTriggerButtonDown() && canFire)
        {
            Fire(hand);
            canFire = false;
        }
        if(hand.GetTriggerButtonUp())
        {
            canFire = true;
        }


        if(GetCanSkill && hand.GetGripButtonDown())
        {
            if (gunType == GunType.Left)
            {
                SkillManager.Instance.UseLeftSkill();
                currentSkillCoolTime = leftSkillCoolTime;
            }
            else if (gunType == GunType.Right)
            {
                SkillManager.Instance.UseRightSkill();
                currentSkillCoolTime = rightSkillCoolTime;
            }
        }
    }

    IEnumerator CorTriggerAxisAnim()
    {
        yield return new WaitForEndOfFrame();
        float axis;
        while(handAnimator != null)
        {
            axis = playerHand.GetTriggerAxis();
            handAnimator.SetFloat("HandValue", axis);
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetCurrentBullet(int bulletCount)
    {
        if(bulletCount >= maxBullet)
        {
            currentBullet = maxBullet;
        }
        else
        {
            currentBullet = bulletCount;
        } 
    }

    public void SetSkillCoolTime(float skillTime) { currentSkillCoolTime = skillTime; }
}