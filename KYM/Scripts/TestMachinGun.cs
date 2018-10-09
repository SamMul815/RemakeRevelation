using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMachinGun : MonoBehaviour {

    public PlayerHand.AttachmentFlags attachmentFlags = PlayerHand.AttachmentFlags.ParentToHand;
    public Transform firepos;
    public int bulletCount;
    public GameObject parentObject;
    private int currentBulletCount;
    IEnumerator shot;

	// Use this for initialization
	void Start ()
    {
		//rightHand = Player.instance.rightHand;
        //leftHand = Player.instance.leftHand;
    }

    private void OnAttachedToHand(PlayerHand hand)
    {
        currentBulletCount = bulletCount;
    }

    private void HandAttachedUpdate(PlayerHand hand)
    {
        this.transform.position = hand.transform.position;
        Vector3 upDir = Quaternion.Euler(-15.0f, 0.0f, 0.0f) * hand.transform.up;
        this.transform.rotation =
            Quaternion.LookRotation(hand.otherHand.transform.position - hand.transform.position, Vector3.up);

        if (hand.GetTriggerButtonDown())
        {
            shot = corShot(hand);
            StartCoroutine(shot);
        }
        else if (hand.GetTriggerButtonUp())
        {
            StopCoroutine(shot);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(Player.instance.rightHand.GetGripButtonDown())
        {
            parentObject = Player.instance.rightHand.currentAttachedObject;
            Player.instance.rightHand.currentAttachedObject.SetActive(false);
            Player.instance.leftHand.currentAttachedObject.SetActive(false);
            Player.instance.rightHand.AttachObject(gameObject, attachmentFlags);
        }

        if (Player.instance.leftHand.GetGripButtonDown())
        {
            Player.instance.rightHand.DetachObject(gameObject,false);
            Player.instance.rightHand.currentAttachedObject.SetActive(true);
            Player.instance.leftHand.currentAttachedObject.SetActive(true);
        }
    }

    IEnumerator corShot(PlayerHand hand)
    {
        while(true)
        {
            Vector2 circle = Random.insideUnitCircle * 0.05f;
            Vector3 dir = firepos.forward + firepos.right * circle.x + firepos.up * circle.y;
            BulletManager.Instance.CreatePlayerBaseBullet(firepos.position, dir.normalized);
            currentBulletCount -= 1;
            hand.Vibration(0.1f,4000.0f);
            hand.otherHand.Vibration(0.1f,4000.0f);
            Player.instance.playerHead.PlayerShake(0.1f, 0.05f);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
