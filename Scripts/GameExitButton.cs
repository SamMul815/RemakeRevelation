using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitButton : VRButton
{
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        ButtonEvent = LoadScene;
    }

    private void LoadScene ()
    {
        LoadSceneManager.Instance.LoadTitle(0.0f);
    }

}
