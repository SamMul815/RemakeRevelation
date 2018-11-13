using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTrigger : MonoBehaviour
{
    public Gun gun;
    public Gun.GunType handLR;
    public bool isAttatch;

    private void Start()
    {
        if (gun != null)
        {
            handLR = gun.gunType;
        }
    }

    private void OnEnable()
    {
        if(gun != null)
        {
            handLR = gun.gunType;
        }
        //isAttatch = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Throwable"))
        {
            other.GetComponent<Throwable>().OnMaterialChange();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Throwable"))
        {
            other.GetComponent<Throwable>().OffMaterialChange();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(handLR == Gun.GunType.Left)
        {
            if(Player.instance.leftHand.GetTriggerButton())
            {
                other.GetComponent<Throwable>().Attatching(Player.instance.leftHand);
            }
        }

        if(handLR == Gun.GunType.Right)
        {
            if(Player.instance.rightHand.GetTriggerButton())
            {
                other.GetComponent<Throwable>().Attatching(Player.instance.rightHand);

            }
        }

    }

}
