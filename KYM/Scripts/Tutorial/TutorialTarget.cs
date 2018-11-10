using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : MonoBehaviour {

    public float hp;

    public void Hit(float damage)
    {
        hp -= damage;
        if(hp <= 0.0f)
        {
            DestroyTarget();
        }
    }

    public bool IsDie() { return hp <= 0.0f; }

    void DestroyTarget()
    {
        this.gameObject.SetActive(false);
    }
}
