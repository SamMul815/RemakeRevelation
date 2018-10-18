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


    // Use this for initialization
    void Start ()
    {
        player = Player.instance.playerStat;
    }
	

    public void Hit()
    {
        hpText.text = player.GetCurrentHP().ToString();
        hpImage.fillAmount = player.GetCurrentHP() / player.GetMaxHP();

        bloodEffect.SetActive(false);
        bloodEffect.SetActive(true);
    }

}
