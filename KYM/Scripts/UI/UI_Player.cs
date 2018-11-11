using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour {

    private PlayerStat player;
    public Text hpText;
    public Image hpImage;
    //public float alphaValue;
    //public float hideTime = 2.0f;
    public GameObject bloodEffect;
    public Image blackScreen;

    public bool isStartFadeIn;
    public float fadeTime = 2.0f;
    // Use this for initialization
    void Start ()
    {
        player = Player.instance.playerStat;
        if(isStartFadeIn)
        {
            FadeIn(fadeTime);
        }
    }
	

    public void Hit()
    {
        hpText.text = player.GetCurrentHP().ToString();
        hpImage.fillAmount = player.GetCurrentHP() / player.GetMaxHP();

        bloodEffect.SetActive(false);
        bloodEffect.SetActive(true);
    }


    public void FadeIn(float time)
    {
        StartCoroutine(corChangeFade(1.0f, 0.0f, time));
    }
    public void FadeOut(float time)
    {
        StartCoroutine(corChangeFade(0.0f, 1.0f, time));
    }


    IEnumerator corChangeFade(float startA, float endA, float fadeTime)
    {
        yield return null;
        Color screenColor = Color.black;
        //screenColor.a = startA;

        for (float t = 0.0f; t < fadeTime; t += Time.unscaledDeltaTime)
        {
            screenColor.a = Mathf.Lerp(startA, endA, t / fadeTime);
            blackScreen.color = screenColor;
            yield return new WaitForEndOfFrame();
        }

        screenColor.a = endA;
        blackScreen.color = screenColor;
    }

}
