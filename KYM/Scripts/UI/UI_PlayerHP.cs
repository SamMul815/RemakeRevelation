using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHP : MonoBehaviour {

    private PlayerStat player;
    public Text hpText;
    public Image hpImage;

    //public Image sliderImage;
    //public Image sliderBackImage;

    public float alphaValue;
    public float hideTime = 2.0f;

	// Use this for initialization
	void Start ()
    {
        player = Player.instance.playerStat;

        //Color color;
        //float alpha = 0.0f;
        //color = sliderFillImage.color;
        //color.a = alpha;
        //sliderFillImage.color = color;

        //color = sliderBackImage.color;
        //color.a = alpha;
        //sliderBackImage.color = color;

        //color = hpText.color;
        //color.a = alpha;
        //hpText.color = color;

        //color = hpImage.color;
        //color.a = alpha;
        //hpImage.color = color;

    }
	
	// Update is called once per frame
	void Update ()
    {
        hpText.text = player.GetCurrentHP().ToString();
        hpImage.fillAmount = player.GetCurrentHP() / player.GetMaxHP();

    }

    public void Hit()
    {
        //StartCoroutine(CorHit(alphaValue, hideTime));
    }

    //IEnumerator CorHit(float alphaValue, float hideTime)
    //{

    //    for(float t = hideTime; t > 0.0f; t -= Time.deltaTime)
    //    {
    //        Color color;
    //        float alpha = alphaValue * t / hideTime;
    //        color = sliderFillImage.color;
    //        color.a = alpha;
    //        sliderFillImage.color = color;

    //        color = sliderBackImage.color;
    //        color.a = alpha;
    //        sliderBackImage.color = color;

    //        color = hpText.color;
    //        color.a = alpha;
    //        hpText.color = color;

    //        color = hpImage.color;
    //        color.a = alpha;
    //        hpImage.color = color;
    //        yield return new WaitForEndOfFrame();
    //    }
    //    yield return null;
    //}

}
