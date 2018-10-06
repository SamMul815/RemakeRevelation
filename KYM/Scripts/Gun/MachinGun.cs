using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinGun : MonoBehaviour {
    public PlayerHand.AttachmentFlags attachmentFlags = PlayerHand.AttachmentFlags.ParentToHand;
    private GameObject parentObject;

    [SerializeField]
    private int maxGauge;
    public int MaxGauge { get { return maxGauge; } }
    public int shootGauge;
    public int timeGauge;

    private int currentGauge;
    public int CurrentGauge { get { return currentGauge; } }

    private float secondTime = 0.0f;

    public GameObject gunBarrelFront;
    public GameObject gunBarrelBack;
    public GameObject shootGunBarrelPos;
    public GameObject baseGunBarrelPos;
    public Transform shootPos;

    [SerializeField]
    private float startDelay;
    public float StartDelay { get { return startDelay; } }

    public float shootDealy;

    IEnumerator startShoot;
    IEnumerator stopShoot;
    // Use this for initialization
    void Start ()
    {
        currentGauge = maxGauge;
	}

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.rightHand.GetGripButtonDown())
        {
            parentObject = Player.instance.rightHand.currentAttachedObject;
            Player.instance.rightHand.currentAttachedObject.SetActive(false);
            Player.instance.leftHand.currentAttachedObject.SetActive(false);
            Player.instance.rightHand.AttachObject(gameObject, attachmentFlags);
        }

        if (Player.instance.leftHand.GetGripButtonDown())
        {
            Player.instance.rightHand.DetachObject(gameObject, false);
            Player.instance.rightHand.currentAttachedObject.SetActive(true);
            Player.instance.leftHand.currentAttachedObject.SetActive(true);
        }
    }

    private void OnAttachedToHand(PlayerHand hand)
    {
        currentGauge = maxGauge;
    }

    private void HandAttachedUpdate(PlayerHand hand)
    {
        this.transform.position = hand.transform.position;
        Vector3 upDir = Quaternion.Euler(-15.0f, 0.0f, 0.0f) * hand.transform.up;
        this.transform.rotation =
            Quaternion.LookRotation(hand.otherHand.transform.position - hand.transform.position, Vector3.up);

        secondTime += Time.unscaledDeltaTime;

        if (hand.GetTriggerButtonDown())
        {
            startShoot = CorShoot(hand);
            StartCoroutine(startShoot);
        }
        else if (hand.GetTriggerButtonUp())
        {
            StopCoroutine(startShoot);
            StartCoroutine(CorStopShoot());
        }

        if(secondTime > 1.0f)
        {
            secondTime -= 1.0f;
            currentGauge -= timeGauge;
        }
        

    }

    IEnumerator CorShoot(PlayerHand hand)
    {
        gunBarrelFront.transform.position = baseGunBarrelPos.transform.position;
        for (float fTime = 0.0f; fTime < startDelay; fTime+= Time.unscaledDeltaTime)
        {
            gunBarrelFront.transform.position = Vector3.Slerp(baseGunBarrelPos.transform.position, shootGunBarrelPos.transform.position, fTime / startDelay);
            yield return new WaitForEndOfFrame();
        }
        float currentShootDelay = shootDealy * 3.0f; 
        while (true)
        {
            Vector2 circle = Random.insideUnitCircle * 0.05f;
            Vector3 dir = shootPos.forward + shootPos.right * circle.x + shootPos.up * circle.y;
            BulletManager.Instance.CreatePlayerBaseBullet(shootPos.position, dir.normalized);
            currentGauge -= shootGauge;
            hand.Vibration(0.1f, 4000.0f);
            hand.otherHand.Vibration(0.1f, 4000.0f);
            Player.instance.playerHead.PlayerShake(0.1f, 0.05f);

            Quaternion rot = gunBarrelBack.transform.localRotation;
            Quaternion nextRot = rot * Quaternion.Euler(0.0f, 0.0f, 180.0f);

            for(float fTime = 0.0f; fTime <= currentShootDelay; fTime += Time.fixedUnscaledDeltaTime)
            {
                gunBarrelBack.transform.localRotation = Quaternion.Lerp(rot, nextRot, fTime / currentShootDelay);
                yield return new WaitForEndOfFrame();
            }

            if(currentShootDelay > shootDealy)
            {
                currentShootDelay -= shootDealy * 0.5f;
            }
            else
            {
                currentShootDelay = shootDealy;
            }
            
            //yield return new WaitForSecondsRealtime(0.1f);
            //yield return new WaitForSecondsRealtime(shootDealy);
        }
    }

    IEnumerator CorStopShoot()
    {
        Quaternion rot = gunBarrelBack.transform.localRotation;
        Quaternion nextRot = Quaternion.Euler(0, 0, 0);
        for (float fTime = 0.0f; fTime <= shootDealy; fTime += Time.fixedUnscaledDeltaTime)
        {
            gunBarrelBack.transform.localRotation = Quaternion.Lerp(rot, nextRot, fTime / shootDealy);
            yield return new WaitForEndOfFrame();
        }

        gunBarrelFront.transform.position = shootGunBarrelPos.transform.position;
        for (float fTime = 0.0f; fTime < startDelay; fTime += Time.unscaledDeltaTime)
        {
            gunBarrelFront.transform.position = Vector3.Slerp(shootGunBarrelPos.transform.position, baseGunBarrelPos.transform.position, fTime / startDelay);
            yield return new WaitForEndOfFrame();
        }
    }
}
