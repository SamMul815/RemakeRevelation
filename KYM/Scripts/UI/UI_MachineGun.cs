using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MachineGun : MonoBehaviour {

    public MachinGun gun;
    public Text gunGauage;

    public GameObject aim1;
    public GameObject aim2;
    public GameObject aim3;

    public float zSpace = 0.5f;

    IEnumerator startShoot;
    IEnumerator stopShoot;

    //public GameObject aim1Pos;
    //public GameObject aim2Pos;
    //public GameObject aim3Pos;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gun.CurrentGauge < 1000)
            gunGauage.text = "0" + gun.CurrentGauge.ToString();
        else if (gun.CurrentGauge < 100)
            gunGauage.text = "00" + gun.CurrentGauge.ToString();
        else if (gun.CurrentGauge < 10)
            gunGauage.text = "000" + gun.CurrentGauge.ToString();
        else if (gun.CurrentGauge <= 0)
            gunGauage.text = "0000";
        else
            gunGauage.text = gun.CurrentGauge.ToString();

        if(Player.instance.rightHand.GetTriggerButton())
        {
            if(stopShoot == null && startShoot == null)
            {
                startShoot = CorShootAim(gun.StartDelay, gun.ShootDelay);
                StartCoroutine(startShoot);
            }
        }
        if(Player.instance.rightHand.GetTriggerButtonUp())
        {
            if(startShoot != null)
            {
                StopCoroutine(startShoot);
                startShoot = null;
                stopShoot = CorStopAim(gun.StartDelay);
                StartCoroutine(stopShoot);
            }

        }

    }

    IEnumerator CorShootAim(float startDelay, float shootDelay)
    {
        float delay = startDelay * 0.5f;

        Vector3 pos = aim1.transform.localPosition;

        for (float fTime = 0.0f; fTime < startDelay; fTime += Time.unscaledDeltaTime)
        {
            //aim1.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / startDelay);
            aim2.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / startDelay);
            aim3.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace * 2), fTime / startDelay);
            yield return new WaitForEndOfFrame();
        }

        while(true)
        {
            float currentShootDelay = shootDelay * 3.0f;

            //Quaternion aim2rot = aim2.transform.localRotation;
            //Quaternion nextAim2Rot = aim2rot * Quaternion.Euler(0.0f, 0.0f, 90.0f);
            //Quaternion aim3rot = aim3.transform.localRotation;
            //Quaternion nextAim3Rot = aim3rot * Quaternion.Euler(0.0f, 0.0f, -120.0f);

            //for (float fTime = 0.0f; fTime <= currentShootDelay; fTime += Time.fixedUnscaledDeltaTime)
            //{
            //    aim2.transform.localRotation = Quaternion.Lerp(aim2rot, nextAim2Rot, fTime / currentShootDelay);
            //    aim3.transform.localRotation = Quaternion.Lerp(aim3rot, nextAim3Rot, fTime / currentShootDelay);
            //    yield return new WaitForEndOfFrame();
            //}

            Quaternion aim1rot = aim1.transform.localRotation;
            Quaternion nextAim1Rot = aim1rot * Quaternion.Euler(0.0f, 0.0f, 90.0f);
            Quaternion aim2rot = aim2.transform.localRotation;
            Quaternion nextAim2Rot = aim2rot * Quaternion.Euler(0.0f, 0.0f, -120.0f);

            for (float fTime = 0.0f; fTime <= currentShootDelay; fTime += Time.fixedUnscaledDeltaTime)
            {
                aim1.transform.localRotation = Quaternion.Lerp(aim1rot, nextAim1Rot, fTime / currentShootDelay);
                aim2.transform.localRotation = Quaternion.Lerp(aim2rot, nextAim2Rot, fTime / currentShootDelay);
                yield return new WaitForEndOfFrame();
            }


            if (currentShootDelay > shootDelay)
            {
                currentShootDelay -= shootDelay * 0.5f;
            }
            else
            {
                currentShootDelay = shootDelay;
            }
        }


    }

    IEnumerator CorStopAim(float startDelay)
    {
        float delay = startDelay * 0.5f;
        Vector3 aim3Pos = aim3.transform.localPosition;
        Vector3 aim2Pos = aim2.transform.localPosition;
        Vector3 aim1Pos = aim1.transform.localPosition;


        for (float fTime = 0.0f; fTime < startDelay; fTime += Time.unscaledDeltaTime)
        {
            //aim1.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / startDelay);
            aim2.transform.localPosition = Vector3.Slerp(aim2Pos, aim1Pos, fTime / startDelay);
            aim3.transform.localPosition = Vector3.Slerp(aim3Pos, aim1Pos, fTime / startDelay);

            aim2.transform.localRotation = Quaternion.Euler(0,0,0);
            aim3.transform.localRotation = Quaternion.Euler(0,0,0);
            yield return new WaitForEndOfFrame();
        }

        if(stopShoot != null)
        {
            stopShoot = null;
        }

    }

}
