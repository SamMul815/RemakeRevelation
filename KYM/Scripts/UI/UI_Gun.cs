using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Gun : MonoBehaviour {

    public Gun gun;
    public Text text;
    public Image textImage;
    public float twinkleTime = 1.0f;

    public Image holoImage1;
    public Image holoImage2;

    public float angleSpeed;

    public Color emptyColor;
    public Color baseColor;

    IEnumerator corTwinkle;
	// Use this for initialization
	void Start ()
    {
        text.color = baseColor;
        corTwinkle = CorTwinkle();
        StartCoroutine(corTwinkle);
	}

    private void OnEnable()
    {
        corTwinkle = CorTwinkle();
        StartCoroutine(corTwinkle);
    }

    private void OnDisable()
    {
        StopCoroutine(corTwinkle);
    }

    // Update is called once per frame
    void Update ()
    {
        if (gun == null)
            return;

        text.text = gun.CurrentBullet.ToString();

        if (gun.CurrentBullet <= 0)
        {
            text.color = emptyColor;
        }
        else
        {
            text.color = baseColor;
        }


        holoImage1.transform.Rotate(0, 0, -angleSpeed);
        holoImage2.transform.Rotate(0, 0, angleSpeed * 0.5f);
	}

    IEnumerator CorTwinkle()
    {
        float minTime = twinkleTime * 0.7f;
        float maxTime = twinkleTime;
        while(true)
        {
            textImage.enabled = true;
            yield return new WaitForSeconds(Random.Range(minTime , maxTime));

            textImage.enabled = false;
            yield return new WaitForSeconds(0.05f);

            textImage.enabled = true;
            yield return new WaitForSeconds(0.1f);

            textImage.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
