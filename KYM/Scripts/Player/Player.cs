using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SteamVR Plugin 소스코드 참고 및 가져다 사용
/// </summary>
public class Player : MonoBehaviour
{
    public Transform trackingOriginTransform;

    public Transform hmdTransform;

    public PlayerHand[] hands;

    public Collider headCollider;

    public PlayerHand leftHand
    {
        get
        {
            for (int i = 0; i < hands.Length; i++)
            {
                if (!hands[i].gameObject.activeInHierarchy)
                {
                    continue;
                }
                if (hands[i].GetHandType() != PlayerHand.HandType.Left)
                {
                    continue;
                }
                return hands[i];
            }
            return null;
        }
    }

    public PlayerHand rightHand
    {
        get
        {
            for (int i = 0; i < hands.Length; i++)
            {
                if (!hands[i].gameObject.activeInHierarchy)
                {
                    continue;
                }
                if (hands[i].GetHandType() != PlayerHand.HandType.Right)
                {
                    continue;
                }
                return hands[i];
            }
            return null;
        }
    }

    private static Player _instance;

    public static Player instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<Player>();
            }
            return _instance;
        }
    }

    public PlayerStat playerStat;

    public PlayerRigid playerRigid;

    public PlayerHead playerHead;

    public UI_Player playerUI;

    private void Start()
    {
        if(playerStat == null)
        {
            playerStat = GetComponent<PlayerStat>();
        }
        if(playerRigid == null)
        {
            playerRigid = GetComponent<PlayerRigid>();
        }
        if(playerHead == null)
        {
            playerHead = GetComponentInChildren<PlayerHead>();
        }
    }

}
