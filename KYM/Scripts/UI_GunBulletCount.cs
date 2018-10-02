using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GunBulletCount : MonoBehaviour {

    public Gun gun;
    public Slider slider;
    public Text text;

    public Color emptyColor;
    public Color baseColor;

    public float updateTime = 0.1f;

	// Use this for initialization
	void Start ()
    {
        text.color = baseColor;
        StartCoroutine(CorUIUpdate());
        slider.maxValue = gun.MaxBullet;
        slider.minValue = 0;
	}	

    IEnumerator CorUIUpdate()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(updateTime);
            text.text = gun.CurrentBullet.ToString();
            slider.value = gun.CurrentBullet;
            if(gun.CurrentBullet <= 0)
            {
                text.color = emptyColor;
            }
            else
            {
                text.color = baseColor;
            }
        }
    }


}
