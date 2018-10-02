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

    public GameObject magazine;
    public GameObject cartridgePrefab;
    public GameObject muzzlePrefab;

    //[SerializeField] private string fireSound;
    //[SerializeField] Text gunBulletCountText;
    //[SerializeField] Color noBulletUIColor;
    //[SerializeField] Slider gunBulletCountSlider;

    //Color yesBulletUIColor;
    private int currentBullet;
    public int CurrentBullet { get { return currentBullet; } }
    private float fireCoolTime;
    private Animator handAnimator;
    private PlayerHand playerHand;



    private void Awake()
    {
        currentBullet = maxBullet;
        fireCoolTime = 0.0f;
        //handAnimator = GetComponentInChildren<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        //gunBulletCountSlider.value = currentBullet / maxBullet;
        //gunBulletCountText.text = currentBullet.ToString();
        //yesBulletUIColor = gunBulletCountText.color;
        //gunAni = GetComponent<GunAnimation>();
	}
	
	// Update is called once per  frame
	void Update ()
    {       
	    if(fireCoolTime > 0.0f)
            fireCoolTime -= Time.unscaledDeltaTime;
    }

    public void Fire()
    {
        if(firePos == null || fireCoolTime > 0.0f || currentBullet <= 0)
        {
            if(firePos == null)
                Debug.LogWarning("Not FirePos");
            return;
        }

        //FMODSoundManager.Instance.PlayFireSound(firePos.transform.position);
        BulletManager.Instance.CreatePlayerBaseBullet(firePos);
        fireCoolTime = fireDelay;
        currentBullet -= 1;

        GameObject cartridge;
        PoolManager.Instance.PopObject(cartridgePrefab, out cartridge);

        if(gunType == GunType.Left)
        {
            cartridge.transform.rotation = magazine.transform.rotation * Quaternion.Euler(0, -40.0f, 0);
        }
        else if(gunType == GunType.Right)
        {
            cartridge.transform.rotation = magazine.transform.rotation * Quaternion.Euler(0, 40.0f, 0);
        }

        cartridge.transform.position = magazine.transform.position;

        GameObject muzzle;
        PoolManager.Instance.PopObject(muzzlePrefab, out muzzle);

        muzzle.transform.position = firePos.position;

        //gunBulletCountSlider.value = (float)currentBullet / maxBullet;
        //gunBulletCountText.text = currentBullet.ToString();
        //if(currentBullet <= 0)
        //{
        //    gunBulletCountText.color = noBulletUIColor;
        //}
        //if (gunAni != null)
        //{
        //    gunAni.MagazieTurn(0.1f, maxBullet);
        //    gunAni.ShakeGun(fireDelay * 0.9f, 10.0f, 0.05f);
        //    gunAni.FireParticle(firePos.position + firePos.forward * 0.1f);

        //    if (isRight)
        //        gunAni.Cartridge(transform.rotation * Quaternion.Euler(0, 40, 0));
        //    else
        //        gunAni.Cartridge(transform.rotation * Quaternion.Euler(0, -40, 0));
        //}

    }

    public void Reload()
    {
        currentBullet = maxBullet;
        //gunBulletCountText.text = currentBullet.ToString();
        //gunBulletCountSlider.value = (float)currentBullet / maxBullet;
        //gunBulletCountText.color = yesBulletUIColor;
    }

    private void OnAttachedToHand(PlayerHand hand)
    {
        playerHand = hand;
        if (hand.GetHandType() == PlayerHand.HandType.Right)
        {
            gunType = GunType.Right;
            handRight.SetActive(true);
            handLeft.SetActive(false);
            handAnimator = handRight.GetComponent<Animator>();
        }
        else if(hand.GetHandType() == PlayerHand.HandType.Left)
        {
            gunType = GunType.Left;
            handLeft.SetActive(true);
            handRight.SetActive(false);
            handAnimator = handLeft.GetComponent<Animator>();
        }
        StartCoroutine(CorTriggerAxisAnim());
    }

    private void HandAttachedUpdate(PlayerHand hand)
    {
        if (hand.GetTriggerButton())
        {
            Fire();
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

}