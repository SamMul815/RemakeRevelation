using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GunBulletCount : MonoBehaviour {

    float angle;
    public Gun gun;
    public Text text;

    public Color emptyColor;
    public Color baseColor;

    public Image backImage1;
    public Image backImage2;

	void Start ()
    {
        text.color = baseColor;
        angle = 0.0f;

    }

    private void Update()
    {
        angle += 1.0f;
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
        backImage1.transform.Rotate(0, 0, -0.5f);
        backImage2.transform.Rotate(0, 0, 0.25f);
        //backImage1.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        //backImage2.rectTransform.rotation = Quaternion.Euler(0, 0, -angle);
        //backImage1.rectTransform.Rotate(-5.0f, 0.0f, 0.0f);
        //backImage2.rectTransform.Rotate(-5.0f, 0.0f, 0.0f);
        //backImage1.transform.localRotation = Quaternion.Euler(0, 0, angle);
        //backImage2.transform.localRotation = Quaternion.Euler(0, 0, -angle);
    }
}
