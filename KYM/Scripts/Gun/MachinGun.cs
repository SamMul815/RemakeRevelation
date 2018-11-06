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

    public GameObject MuzzleFlash;

    public bool canRot = false;

    public float onTime = 0.5f;
    public Material[] machinegunOnMaterials;
    public GameObject MachineGunBody;
    public GameObject MachineGunUIObject;
    public GameObject MachineGunOnObject;

    private PoolManager poolManager;
    private BulletManager bulletManager;
    private PlayerHead playerHead;
    private FmodManager fmodManager;

    IEnumerator startShoot;
    IEnumerator stopShoot;
    IEnumerator endMachingun;


    void Start ()
    {
        currentGauge = maxGauge;
        poolManager = PoolManager.Instance;
        bulletManager = BulletManager.Instance;
        playerHead = Player.instance.playerHead;
        fmodManager = FmodManager.Instance;
    }

    private void OnEnable()
    {
        currentGauge = maxGauge;
        
        for(int i = 0; i< machinegunOnMaterials.Length; i++)
        {
            machinegunOnMaterials[i].SetFloat("_warf", -10.0f);
        }
        StartCoroutine(CorMachineGunOn());
    }

    private void OnAttachedToHand(PlayerHand hand)
    {
        currentGauge = maxGauge;
    }

    private void HandAttachedUpdate(PlayerHand hand)
    {
        this.transform.position = hand.transform.position;
        this.transform.rotation =
            Quaternion.LookRotation(hand.otherHand.transform.position - hand.transform.position, Vector3.up);

        secondTime += Time.unscaledDeltaTime;

        if (hand.GetTriggerButton() && currentGauge > 0)
        {
            if(stopShoot == null && startShoot == null)
            {
                startShoot = CorShoot(hand);
                StartCoroutine(startShoot);
                canRot = true;
            }
        }
        else if (hand.GetTriggerButtonUp() && currentGauge > 0)
        {
            if(startShoot != null)
            {
                canRot = false;
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
        }
    }

    IEnumerator CorShoot(PlayerHand hand)
    {
        gunBarrelFront.transform.position = baseGunBarrelPos.transform.position;
        for (float fTime = 0.0f; fTime < startDelay; fTime+= Time.unscaledDeltaTime)
        {
            gunBarrelFront.transform.position = Vector3.Lerp(baseGunBarrelPos.transform.position, shootGunBarrelPos.transform.position, fTime / startDelay);
            yield return new WaitForEndOfFrame();
        }
        float currentShootDelay = shootDelay * 3.0f;
        Transform headcol = Player.instance.headCollider.transform;
        while (true)
        {
            Vector3 sphere = Random.insideUnitSphere * 0.1f;
            Vector3 dir =  Aim.position + sphere - headcol.position;

            GameObject muzzleFlash;
            fmodManager.PlaySoundOneShot(shootPos.position, "MachineGun");
            poolManager.PopObject(MuzzleFlash, shootPos.position + shootPos.forward*0.1f,shootPos.rotation, out muzzleFlash);
            bulletManager.CreatePlayerMachinBullet(shootPos.position, dir.normalized);
            currentGauge -= shootGauge;
            hand.Vibration(0.2f, 6000.0f);
            hand.otherHand.Vibration(0.2f, 6000.0f);
            playerHead.PlayerShake(0.1f, 0.05f);

            Quaternion rot = gunBarrelBack.transform.localRotation;
            Quaternion nextRot = rot * Quaternion.Euler(0.0f, 0.0f, 60.0f);

            for(float fTime = 0.0f; fTime <= currentShootDelay; fTime += Time.fixedUnscaledDeltaTime)
            {
                gunBarrelBack.transform.localRotation = Quaternion.Lerp(rot, nextRot, fTime / currentShootDelay);
                yield return new WaitForFixedUpdate();
            }

            if(currentShootDelay > shootDelay)
            {
                currentShootDelay -= shootDelay * 0.5f;
            }
            else
            {
                currentShootDelay = shootDelay;
            }         
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
            gunBarrelFront.transform.position = Vector3.Lerp(shootGunBarrelPos.transform.position, baseGunBarrelPos.transform.position, fTime / startDelay);
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
            gunBarrelFront.transform.position = Vector3.Lerp(shootGunBarrelPos.transform.position, baseGunBarrelPos.transform.position, fTime / startDelay);
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

    IEnumerator CorMachineGunOn()
    {
        
        MachineGunBody.SetActive(false);
        MachineGunUIObject.SetActive(false);
        MachineGunOnObject.SetActive(true);

        this.transform.position =
            Player.instance.headCollider.transform.position +
            Player.instance.headCollider.transform.forward * 1.0f;
        this.transform.rotation = 
            Quaternion.LookRotation(
                Player.instance.headCollider.transform.forward, Vector3.up);

        Vector3 savePosition = this.transform.position;
        Quaternion saveRot = this.transform.rotation;

        parentObject = Player.instance.rightHand.currentAttachedObject;
        Player.instance.rightHand.currentAttachedObject.SetActive(false);
        Player.instance.leftHand.currentAttachedObject.SetActive(false);

        for (float _t = 0.0f; _t < onTime; _t += Time.fixedUnscaledDeltaTime)
        {
            for (int i = 0; i < machinegunOnMaterials.Length; i++)
            {
                machinegunOnMaterials[i].SetFloat("_warf",-10.0f +  10.0f * _t / onTime);
            }
            this.transform.position = 
                Vector3.Lerp(savePosition, parentObject.transform.position, _t / onTime);
            this.transform.rotation = 
                Quaternion.Lerp(saveRot, 
                Quaternion.LookRotation(Player.instance.leftHand.transform.position - Player.instance.rightHand.transform.position, Vector3.up), _t / onTime);
       
            yield return new WaitForFixedUpdate();
        }

        Player.instance.rightHand.AttachObject(gameObject, attachmentFlags);
        MachineGunOnObject.SetActive(false);
        MachineGunBody.SetActive(true);
        MachineGunUIObject.SetActive(true);



        yield return null;
    }

}
