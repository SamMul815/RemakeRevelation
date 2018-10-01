using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRigid : MonoBehaviour
{
    public TeleportPointer playerTeleportPointer;
    public float minVelocity;

    private PlayerStat playerStat;
    private Rigidbody rigid;

    private void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        playerStat = Player.instance.playerStat;
    }

    public void PlayerPush(Vector3 dir, float power, ForceMode mode = ForceMode.VelocityChange)
    {
        rigid.AddForce(dir.normalized * power, mode);
        if (playerTeleportPointer != null)
        {
            playerTeleportPointer.StopTeleport();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(rigid)
        {
            //Debug.Log(rigid.velocity.magnitude);
            if(rigid.velocity.magnitude > minVelocity &&
                playerStat.playerVRState != PlayerStat.PlayerVRState.Teleporting)
            {
                playerStat.playerVRState = PlayerStat.PlayerVRState.Pushing;
            }
            else if(rigid.velocity.magnitude  <= minVelocity &&
                playerStat.playerVRState == PlayerStat.PlayerVRState.Pushing)
            {
                playerStat.playerVRState = PlayerStat.PlayerVRState.Idle;
                Player.instance.playerHead.PlayerShake();
            }

        }

        //if (Player.instance.rightHand.GetTouchPadDown())
        //{
        //    Vector3 dir = Player.instance.rightHand.transform.forward;
        //    PlayerPush(dir + Vector3.up * 0.5f, 20.0f);
        //}
    }


}
