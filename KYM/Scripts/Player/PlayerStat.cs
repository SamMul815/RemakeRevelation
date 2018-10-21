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


    [SerializeField]
    private float maxHP;
    private float currentHP;

    bool isPlayerDie;

	void Start ()
    {
        currentHP = maxHP;
        isPlayerDie = false;
	}

    public void Hit(float damage)
    {
        currentHP -= damage;
        Debug.Log(currentHP);
        if (currentHP <= 0)
        {
            currentHP = 0;
            isPlayerDie = true;
        }
        playerUI.Hit();
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
