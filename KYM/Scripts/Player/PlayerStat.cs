using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public enum PlayerVRState
    {
        Idle,
        Pushing,
        TeleportSelect,
        Teleporting
    }
    public PlayerVRState playerVRState = PlayerVRState.Idle;
    public UI_Player playerUI;
    public float hitTime = 0.5f;
    public float dotTime = 0.1f;

    [SerializeField]
    private float maxHP;
    private float currentHP;
    bool isPlayerDie;

    private float playerHitDelay;

	void Start ()
    {
        currentHP = maxHP;
        isPlayerDie = false;
        playerHitDelay = 0.0f;

    }

    public void Hit(float damage)
    {
        if (playerHitDelay > 0) return;

        currentHP -= damage;
        Debug.Log(currentHP);
        if (currentHP <= 0)
        {
            currentHP = 0;
            isPlayerDie = true;
        }
        playerUI.Hit();
        playerHitDelay = hitTime;
    }

    public void dotHit(float damage)
    {
        if (playerHitDelay > 0) return;

        currentHP -= damage;
        Debug.Log(currentHP);
        if (currentHP <= 0)
        {
            currentHP = 0;
            isPlayerDie = true;
        }
        playerUI.Hit();
        playerHitDelay = dotTime;
    }


    private void Update()
    {
        if(playerHitDelay > 0)
        {
            playerHitDelay -= Time.deltaTime;
        }
    }

    public float GetCurrentHP()
    {
        return currentHP;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }
	
    public bool IsPlayerDie()
    {
        return isPlayerDie;
    }
}
