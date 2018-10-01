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

    [SerializeField]
    private readonly float maxHP;
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

        if (currentHP <= 0)
        {
            currentHP = 0;
            isPlayerDie = true;
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
