using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour {

    public GameObject slowCircle;
    public PlayerHand.HandType slowHandType;

    public float slowCircleTime = 2.0f;
    public Vector3 slowCircleScale;
    public float slowTime = 10.0f;
    [Tooltip("0이면 멈춤, 1이면 원래 배속")]
    [Range(0, 1)]
    public float slowValue = 0.2f;
    public Material  noiseMaterial;
    public string noiseString;

    private PlayerHand playerHand;
    private bool isSlow;
    private IEnumerator corSlow;

    //public float 

	// Use this for initialization
	void Start ()
    {
        noiseMaterial = slowCircle.GetComponent<MeshRenderer>().material;
        if (slowHandType == PlayerHand.HandType.Left)
            playerHand = Player.instance.leftHand;
        else if (slowHandType == PlayerHand.HandType.Right)
            playerHand = Player.instance.rightHand;

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(playerHand.GetTouchPadDown())
        {
            if(isSlow == false)
            {
                noiseMaterial.SetFloat(noiseString, 1.0f);
                corSlow = CorSlow();
                StartCoroutine(corSlow);
            }
            //else
            //{
            //    if(corSlow != null)
            //    {
            //        //StopCoroutine(corSlow);
            //        //corSlow = null;
            //        //StartCoroutine(CorRetrunSlow());
            //    }
            //}
        }
	}

    IEnumerator CorSlow()
    {
        isSlow = true;
        //slowCircle.gameObject.transform.localScale = Vector3.zero;
        slowCircle.SetActive(true);
        slowCircle.transform.position = Player.instance.transform.position;
        slowCircle.transform.localScale = Vector3.zero;
        slowCircle.GetComponent<SlowCircle>().ScaleChange(slowCircleScale, slowCircleTime);
        for (float fTime = 0.0f; fTime <= slowCircleTime; fTime += Time.unscaledDeltaTime )
        {
            Time.timeScale = 1.0f - (1.0f - slowValue) * fTime / slowCircleTime;
            //Debug.Log(Time.timeScale);
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = slowValue;
        //yield return new WaitForSecondsRealtime(slowTime);

        for(float fTime = 0.0f; fTime <= slowTime; fTime += Time.unscaledDeltaTime)
        {
            Debug.Log(1.0f - fTime / slowTime);
            noiseMaterial.SetFloat(noiseString,1.0f - fTime / slowTime);
            //Debug.Log(noiseMaterial.GetFloat(noiseString));
            yield return new WaitForEndOfFrame();
        }
        
        Time.timeScale = 1.0f;
        slowCircle.transform.localScale = Vector3.zero;
        slowCircle.SetActive(false);
        isSlow = false;
        //StartCoroutine(CorRetrunSlow());

    }

    //IEnumerator CorRetrunSlow()
    //{
    //    corSlow = null;
    //    float recicleTime = slowCircleTime * 0.7f;
    //    slowCircle.GetComponent<SlowCircle>().ScaleChange(Vector3.zero, recicleTime);
    //    for (float fTime = 0.0f; fTime <= recicleTime; fTime += Time.unscaledDeltaTime)
    //    {
    //        Time.timeScale = 1.0f - (1.0f - slowValue) * (1.0f - (fTime / recicleTime));
    //        //Time.timeScale = 1.0f - slowValue * (1.0f - (fTime / slowCircleTime));
    //        Debug.Log(Time.timeScale);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    Time.timeScale = 1.0f;
    //    slowCircle.SetActive(false);
    //    yield return null;
    //    isSlow = false;
    //}

}