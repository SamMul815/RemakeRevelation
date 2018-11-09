using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBase : MonoBehaviour {

    public TutorialBase nextTutorial;

    protected virtual bool IsClear()
    {
        return false;
    }

    protected virtual void Clear()
    {
        if(nextTutorial != null)
        {
            nextTutorial.enabled = true;
        }
        this.enabled = false;
    }

    protected virtual void Update()
    {
        if(IsClear() || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Clear();
        }
    }
}
