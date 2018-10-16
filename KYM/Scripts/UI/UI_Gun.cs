using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Gun : MonoBehaviour {

    public Gun gun;
    public Text text;
    public Image textOutline;
    public Image textImage1;
    public Image textImage2;

    public Scrollbar textscroll;
    public float twinkleTime = 1.0f;

    public Image skillOutline;
    public Image skillSymbol;

    //public Image rightSkillOutline;
    //public Image rightSkillSymbol;

    public Image holoImage1;
    public Image holoImage2;

    public float angleSpeed;

    public Color emptyColor;
    public Color baseColor;

    private Color baseSkillColor;
    public Color emptySkillColor;

    private Color textOutlineColor;

    private Color textImageColor1;
    private Color textIMageColor2;

    private Color holoColor1;
    private Color holoColor2;


    IEnumerator corText;
	// Use this for initialization
	void Start ()
    {
        holoColor1 = holoImage1.color;
        holoColor2 = holoImage2.color;

        baseSkillColor = skillSymbol.color;


        textOutlineColor = textOutline.color;

        textImageColor1 = textImage1.color;
        textIMageColor2 = textImage2.color;

        text.color = baseColor;
        if(gun.gunType == Gun.GunType.Right)
        {
            corText = CorTwinkle();
            StartCoroutine(corText);
        }
        else if(gun.gunType == Gun.GunType.Left)
        {
            corText = CorScroll();
            StartCoroutine(corText);
        }
	}

    private void OnEnable()
    {
        if (gun.gunType == Gun.GunType.Right)
        {
            corText = CorTwinkle();
            StartCoroutine(corText);
        }
        else if(gun.gunType == Gun.GunType.Left)
        {
            corText = CorScroll();
            StartCoroutine(corText);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(corText);
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
            holoImage1.color = emptyColor;
            holoImage2.color = emptyColor;
            textOutline.color = emptyColor;

            if(gun.gunType == Gun.GunType.Left)
            {
                textImage1.color = emptyColor;
                textImage2.color = emptyColor;
            }
            else if(gun.gunType == Gun.GunType.Right)
            {
                textImage1.color = emptyColor;
            }
        }
        else
        {
            text.color = baseColor;
            holoImage1.color = holoColor1;
            holoImage2.color = holoColor2;
            textOutline.color = textOutlineColor;

            holoImage1.transform.Rotate(0, 0, -angleSpeed);
            holoImage2.transform.Rotate(0, 0, angleSpeed * 0.5f);

            if (gun.gunType == Gun.GunType.Left)
            {
                textImage1.color = textImageColor1;
                textImage2.color = textIMageColor2;
            }
            else if (gun.gunType == Gun.GunType.Right)
            {
                textImage1.color = textImageColor1;
            }

        }

        if(gun.gunType == Gun.GunType.Left)
        {
            skillOutline.transform.Rotate(0, 0, angleSpeed * 0.25f);
        }

        if(gun.GetCanSkill)
        {
            skillSymbol.color = baseSkillColor;
        }
        else
        {
            skillSymbol.color = emptySkillColor;
        }

	}

    IEnumerator CorTwinkle()
    {
        float minTime = twinkleTime * 0.7f;
        float maxTime = twinkleTime;
        while(true)
        {
            textImage1.enabled = true;
            yield return new WaitForSeconds(Random.Range(minTime , maxTime));

            textImage1.enabled = false;
            yield return new WaitForSeconds(0.05f);

            textImage1.enabled = true;
            yield return new WaitForSeconds(0.1f);

            textImage1.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator CorScroll()
    {
        float value = textscroll.value;
        while(true)
        { 
            if(value <= 0.0f)
            {
                value = 1.0f;
            }
            value -= Time.deltaTime * 0.5f;
            textscroll.value = value;
            yield return new WaitForEndOfFrame();
        }
    }
}
