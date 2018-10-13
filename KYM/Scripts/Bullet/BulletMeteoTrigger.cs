﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMeteoTrigger : MonoBehaviour {

    private bool isTrigger;
    public float pushPower;


    private void OnEnable()
    {
        GetComponent<SphereCollider>().enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Player.instance.playerRigid.PlayerPush(Vector3.up, pushPower);
            //Debug.Log("Push Trigger");
            Player.instance.playerHead.PlayerShake(0.7f, 1.0f);
            Player.instance.rightHand.Vibration(0.7f, 4000.0f);
            Player.instance.leftHand.Vibration(0.7f, 4000.0f);
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}