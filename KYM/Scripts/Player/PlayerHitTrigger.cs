﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitTrigger : MonoBehaviour {

    public GameObject hitEffect;
    public PlayerStat player;
    public float coolTime = 2.0f;
    public float delay = 0.0f;
    public UI_PlayerHP playerHpUI;

	//// Use this for initialization
	//void Start ()
 //   {
		
	//}
	
	// Update is called once per frame
	void Update ()
    {
		if(delay > 0.0f)
        {
            delay -= Time.unscaledDeltaTime;
        }
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DragonBullet" && delay <= 0.0f)
        {
            float damage = other.GetComponent<Bullet>().Damage;
            player.Hit(damage);
            playerHpUI.Hit();
            hitEffect.SetActive(true);
            delay = coolTime;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
        
    //}

}
