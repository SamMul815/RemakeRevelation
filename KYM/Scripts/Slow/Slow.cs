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

        if (slowHandType == PlayerHand.HandType.Left)
            playerHand = Player.instance.leftHand;
        else if (slowHandType == PlayerHand.HandType.Right)
            playerHand = Player.instance.rightHand;

    }
	
	// Update is called once per frame
	void Update ()
    {
		//if(playerHand.GetGripButtonDown())
  //      {

  //      }
	}

    private void OnEnable()
    {
        if (isSlow == false)
        {
            noiseMaterial = slowCircle.GetComponent<MeshRenderer>().material;
            noiseMaterial.SetFloat(noiseString, 1.0f);
            corSlow = CorSlow();
            StartCoroutine(corSlow);
        }
    }

    IEnumerator CorSlow()
    {
        isSlow = true;
        slowCircle.SetActive(true);
        slowCircle.transform.position = Player.instance.transform.position;
        slowCircle.transform.localScale = Vector3.zero;
        slowCircle.GetComponent<SlowCircle>().ScaleChange(slowCircleScale, slowCircleTime);
        for (float fTime = 0.0f; fTime <= slowCircleTime; fTime += Time.unscaledDeltaTime )
        {
            Time.timeScale = 1.0f - (1.0f - slowValue) * fTime / slowCircleTime;
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = slowValue;

        yield return new WaitForSecondsRealtime(slowTime);

        //for(float fTime = 0.0f; fTime <= slowTime; fTime += Time.unscaledDeltaTime)
        //{
        //    yield return new WaitForEndOfFrame();
        //}

        float hideCircleTime = slowCircleTime = 0.5f;
        for (float fTime = 0.0f; fTime <= hideCircleTime; fTime += Time.unscaledDeltaTime)
        {
            noiseMaterial.SetFloat(noiseString, 1.0f - fTime / hideCircleTime);
            yield return new WaitForEndOfFrame();
        }

        Time.timeScale = 1.0f;
        slowCircle.transform.localScale = Vector3.zero;
        slowCircle.SetActive(false);
        isSlow = false;
        this.gameObject.SetActive(false);

    }
}