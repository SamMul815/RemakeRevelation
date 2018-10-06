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
        else if (gun.CurrentGauge < 0)
            gunGauage.text = "0000";
        else
            gunGauage.text = gun.CurrentGauge.ToString();

        if(Player.instance.rightHand.GetTriggerButtonDown())
        {
            StartCoroutine(CorShootAim(gun.StartDelay));
        }
        if(Player.instance.rightHand.GetTriggerButtonUp())
        {
            StartCoroutine(CorStopAim(gun.StartDelay));
        }

    }

    IEnumerator CorShootAim(float startDelay)
    {
        float delay = startDelay * 0.5f;

        Vector3 pos = aim1.transform.localPosition;

        //for (float fTime = 0.0f; fTime < delay; fTime += Time.unscaledDeltaTime)
        //{
        //    aim2.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / delay);
        //    aim3.transform.localPosition = aim2.transform.position;
        //    yield return new WaitForEndOfFrame();
        //}
        //pos += new Vector3(0, 0, zSpace);

        //for (float fTime = 0.0f; fTime < delay; fTime += Time.unscaledDeltaTime)
        //{
        //    aim3.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / delay);
        //    yield return new WaitForEndOfFrame();
        //}

        for (float fTime = 0.0f; fTime < startDelay; fTime += Time.unscaledDeltaTime)
        {
            //aim1.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / startDelay);
            aim2.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / startDelay);
            aim3.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace * 2), fTime / startDelay);
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator CorStopAim(float startDelay)
    {
        float delay = startDelay * 0.5f;
        Vector3 aim3Pos = aim3.transform.localPosition;
        Vector3 aim2Pos = aim2.transform.localPosition;
        Vector3 aim1Pos = aim1.transform.localPosition;

        //for (float fTime = 0.0f; fTime < delay; fTime += Time.unscaledDeltaTime)
        //{
        //    aim3.transform.localPosition = Vector3.Slerp(aim3Pos, aim2Pos, fTime / delay);
        //    yield return new WaitForEndOfFrame();
        //}

        //for (float fTime = 0.0f; fTime < delay; fTime += Time.unscaledDeltaTime)
        //{
        //    aim2.transform.localPosition = Vector3.Slerp(aim2Pos, aim1Pos, fTime / delay);
        //    aim3.transform.localPosition = aim2.transform.localPosition;
        //    yield return new WaitForEndOfFrame();
        //}

        for (float fTime = 0.0f; fTime < startDelay; fTime += Time.unscaledDeltaTime)
        {
            //aim1.transform.localPosition = Vector3.Slerp(pos, pos + new Vector3(0, 0, zSpace), fTime / startDelay);
            aim2.transform.localPosition = Vector3.Slerp(aim2Pos, aim1Pos, fTime / startDelay);
            aim3.transform.localPosition = Vector3.Slerp(aim3Pos, aim1Pos, fTime / startDelay);
            yield return new WaitForEndOfFrame();
        }

    }

}
