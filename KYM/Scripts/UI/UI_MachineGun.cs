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
	
	// Update is called once per frame
	void Update ()
    {
        if (gun.CurrentGauge <= 0)
            gunGauage.text = "0000";
        else if (gun.CurrentGauge < 10)
            gunGauage.text = "000" + gun.CurrentGauge.ToString();
        else if (gun.CurrentGauge < 100)
            gunGauage.text = "00" + gun.CurrentGauge.ToString();
        else if (gun.CurrentGauge < 1000)
            gunGauage.text = "0" + gun.CurrentGauge.ToString();
        else
            gunGauage.text = gun.CurrentGauge.ToString();

        if(Player.instance.rightHand.GetTriggerButton() && gun.CurrentGauge > 0)
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

    public void StopAim()
    {
        if (startShoot != null)
        {
            StopCoroutine(startShoot);
            startShoot = null;
        }
        stopShoot = CorStopAim(gun.StartDelay);
        StartCoroutine(stopShoot);
    }

    public void Clear()
    {
        if(startShoot != null)
        {
            StopCoroutine(startShoot);
        }
        if(stopShoot != null)
        {
            StopCoroutine(stopShoot);
        }

        Vector3 pos2 = aim2.transform.localPosition;
        Vector3 pos3 = aim3.transform.localPosition;

        pos2.z = 0;
        pos3.z = 0;

        aim2.transform.localRotation = Quaternion.Euler(0, 0, 0);
        aim3.transform.localRotation = Quaternion.Euler(0, 0, 0);
        aim2.transform.localPosition = pos2;
        aim3.transform.localPosition = pos3;
    }

    IEnumerator CorShootAim(float startDelay, float shootDelay)
    {
        float delay = startDelay * 0.5f;

        Vector3 pos1 = aim1.transform.localPosition;
        Vector3 pos2 = aim2.transform.localPosition;
        Vector3 pos3 = aim3.transform.localPosition;

        for (float fTime = 0.0f; fTime < startDelay; fTime += Time.unscaledDeltaTime)
        {
            //aim1.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / startDelay);
            aim2.transform.localPosition = Vector3.Lerp(pos2, pos2 + new Vector3(0, 0, zSpace), fTime / startDelay);
            aim3.transform.localPosition = Vector3.Lerp(pos3, pos3 + new Vector3(0, 0, zSpace * 2), fTime / startDelay);
            yield return new WaitForEndOfFrame();
        }
        aim2.transform.localPosition = Vector3.Lerp(pos2, pos2 + new Vector3(0, 0, zSpace), 1.0f);
        aim3.transform.localPosition = Vector3.Lerp(pos3, pos3 + new Vector3(0, 0, zSpace * 2), 1.0f);
        while (true)
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

        Vector3 pos2 = aim2.transform.localPosition;
        Vector3 pos3 = aim3.transform.localPosition;

        pos2.z = 0;
        pos3.z = 0;


        for (float fTime = 0.0f; fTime < startDelay; fTime += Time.unscaledDeltaTime)
        {
            //aim1.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / startDelay);
            aim2.transform.localPosition = Vector3.Lerp(aim2Pos, pos2, fTime / startDelay);
            aim3.transform.localPosition = Vector3.Lerp(aim3Pos, pos3, fTime / startDelay);

            aim2.transform.localRotation = Quaternion.Euler(0,0,0);
            aim3.transform.localRotation = Quaternion.Euler(0,0,0);
            yield return new WaitForEndOfFrame();
        }
        aim2.transform.localPosition = Vector3.Lerp(aim2Pos, pos2, 1.0f);
        aim3.transform.localPosition = Vector3.Lerp(aim3Pos, pos3, 1.0f);

        if (stopShoot != null)
        {
            stopShoot = null;
        }

    }
}
