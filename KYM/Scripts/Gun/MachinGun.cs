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
    public Transform Aim;

    [SerializeField]
    private float startDelay;
    public float StartDelay { get { return startDelay; } }
 
    [SerializeField]
    private float shootDelay;
    public float ShootDelay { get { return shootDelay; } }

    public UI_MachineGun machingunUI;

    
    IEnumerator startShoot;
    IEnumerator stopShoot;
    IEnumerator endMachingun;
    // Use this for initialization
    void Start ()
    {
        currentGauge = maxGauge;
	}

    private void OnEnable()
    {
        currentGauge = maxGauge;
        parentObject = Player.instance.rightHand.currentAttachedObject;
        Player.instance.rightHand.currentAttachedObject.SetActive(false);
        Player.instance.leftHand.currentAttachedObject.SetActive(false);
        Player.instance.rightHand.AttachObject(gameObject, attachmentFlags);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //if (Player.instance.rightHand.GetGripButtonDown())
    //    //{
    //    //    parentObject = Player.instance.rightHand.currentAttachedObject;
    //    //    Player.instance.rightHand.currentAttachedObject.SetActive(false);
    //    //    Player.instance.leftHand.currentAttachedObject.SetActive(false);
    //    //    Player.instance.rightHand.AttachObject(gameObject, attachmentFlags);
    //    //}

    //    //if (Player.instance.leftHand.GetGripButtonDown())
    //    //{
    //    //    Player.instance.rightHand.DetachObject(gameObject, false);
    //    //    Player.instance.rightHand.currentAttachedObject.SetActive(true);
    //    //    Player.instance.leftHand.currentAttachedObject.SetActive(true);
    //    //}
    //}

    private void OnAttachedToHand(PlayerHand hand)
    {
        currentGauge = maxGauge;
    }

    private void HandAttachedUpdate(PlayerHand hand)
    {
        //Vector3 posDir = hand.transform.position - hand.otherHand.transform.position;
        //this.transform.position = hand.transform.position + posDir * 0.5f;
        this.transform.position = hand.transform.position;
        //Vector3 upDir = Quaternion.Euler(-15.0f, 0.0f, 0.0f) * hand.transform.up;
        //this.transform.rotation =
        //    Quaternion.LookRotation(this.transform.position - Player.instance.headCollider.transform.position, Vector3.up);
        this.transform.rotation =
            Quaternion.LookRotation(hand.otherHand.transform.position - hand.transform.position, Vector3.up);

        secondTime += Time.unscaledDeltaTime;

        if (hand.GetTriggerButton() && currentGauge > 0)
        {
            if(stopShoot == null && startShoot == null)
            {
                startShoot = CorShoot(hand);
                StartCoroutine(startShoot);
            }
        }
        else if (hand.GetTriggerButtonUp() && currentGauge > 0)
        {
            if(startShoot != null)
            {
                StopCoroutine(startShoot);
                startShoot = null;

                stopShoot = CorStopShoot();
                StartCoroutine(stopShoot);
            }
        }

        if (secondTime > 1.0f)
        {
            secondTime -= 1.0f;
            currentGauge -= timeGauge;
        }

        if (currentGauge <= 0)
        {
            if(endMachingun == null)
            {
                endMachingun = CorEndMachinGun();
                StartCoroutine(endMachingun);
            }
            //StopCoroutine(startShoot);
            //startShoot = null;
            //stopShoot = null;
            //stopShoot = CorStopShoot();
            //StartCoroutine(stopShoot);
            //Player.instance.rightHand.DetachObject(gameObject, false);
            //Player.instance.rightHand.currentAttachedObject.SetActive(true);
            //Player.instance.leftHand.currentAttachedObject.SetActive(true);
            //this.gameObject.SetActive(false);
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
        float currentShootDelay = shootDelay * 3.0f;
        Transform headcol = Player.instance.headCollider.transform;
        while (true)
        {
            Vector3 sphere = Random.insideUnitSphere * 0.1f;
            Vector3 dir =  Aim.position + sphere - headcol.position;
            
            //Ray ray = new Ray(Aim.position, dir);
            //RaycastHit rayHit;
            //if (Physics.Raycast(ray,out rayHit, 1000.0f))
            //{
            //    dir =  rayHit.point - shootPos.position;
            //    Debug.Log(rayHit.collider.name +  "rayray");
            //}
            //else
            //{
            //   // dir = Aim.position - shootPos.position;
            //}

            BulletManager.Instance.CreatePlayerMachinBullet(shootPos.position, dir.normalized);
            currentGauge -= shootGauge;
            hand.Vibration(0.2f, 6000.0f);
            hand.otherHand.Vibration(0.2f, 6000.0f);
            Player.instance.playerHead.PlayerShake(0.1f, 0.05f);

            Quaternion rot = gunBarrelBack.transform.localRotation;
            Quaternion nextRot = rot * Quaternion.Euler(0.0f, 0.0f, 180.0f);

            for(float fTime = 0.0f; fTime <= currentShootDelay; fTime += Time.fixedUnscaledDeltaTime)
            {
                gunBarrelBack.transform.localRotation = Quaternion.Lerp(rot, nextRot, fTime / currentShootDelay);
                yield return new WaitForEndOfFrame();
            }

            if(currentShootDelay > shootDelay)
            {
                currentShootDelay -= shootDelay * 0.5f;
            }
      
            
            //yield return new WaitForSecondsRealtime(0.1f);
            //yield return new WaitForSecondsRealtime(shootDealy);
        }
    }

    IEnumerator CorStopShoot()
    {
        Quaternion rot = gunBarrelBack.transform.localRotation;
        Quaternion nextRot = Quaternion.Euler(0, 0, 0);
        for (float fTime = 0.0f; fTime <= shootDelay; fTime += Time.fixedUnscaledDeltaTime)
        {
            gunBarrelBack.transform.localRotation = Quaternion.Lerp(rot, nextRot, fTime / shootDelay);
            yield return new WaitForEndOfFrame();
        }

        gunBarrelFront.transform.position = shootGunBarrelPos.transform.position;
        for (float fTime = 0.0f; fTime < startDelay; fTime += Time.unscaledDeltaTime)
        {
            gunBarrelFront.transform.position = Vector3.Slerp(shootGunBarrelPos.transform.position, baseGunBarrelPos.transform.position, fTime / startDelay);
            yield return new WaitForEndOfFrame();
        }

        if(stopShoot != null)
        {
            stopShoot = null;
        }
    }

    IEnumerator CorEndMachinGun()
    {
        if(startShoot != null)
        {
            StopCoroutine(startShoot);
            startShoot = null;

        }

        if(stopShoot != null)
        {
            StopCoroutine(stopShoot);
            stopShoot = null;
        }

        if(machingunUI != null)
        {
            machingunUI.StopAim();
        }

        Quaternion rot = gunBarrelBack.transform.localRotation;
        Quaternion nextRot = Quaternion.Euler(0, 0, 0);
        for (float fTime = 0.0f; fTime <= shootDelay; fTime += Time.fixedUnscaledDeltaTime)
        {
            gunBarrelBack.transform.localRotation = Quaternion.Lerp(rot, nextRot, fTime / shootDelay);
            yield return new WaitForEndOfFrame();
        }

        gunBarrelFront.transform.position = shootGunBarrelPos.transform.position;
        for (float fTime = 0.0f; fTime < startDelay; fTime += Time.unscaledDeltaTime)
        {
            gunBarrelFront.transform.position = Vector3.Slerp(shootGunBarrelPos.transform.position, baseGunBarrelPos.transform.position, fTime / startDelay);
            yield return new WaitForEndOfFrame();
        }

        Player.instance.rightHand.DetachObject(gameObject, false);
        Player.instance.rightHand.currentAttachedObject.SetActive(true);
        Player.instance.leftHand.currentAttachedObject.SetActive(true);
        endMachingun = null;

        if (machingunUI != null)
            machingunUI.Clear();

        this.gameObject.SetActive(false);

    }
}
