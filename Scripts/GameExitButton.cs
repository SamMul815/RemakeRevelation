using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitButton : VRButton
{
    public float waitTime;
    public string nextSceneName;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        ButtonEvent = LoadScene;
    }

    private void LoadScene ()
    {
        StartCoroutine(corLoading(waitTime, nextSceneName));
    }
    private static IEnumerator corLoading (float waitTime, string nextSceneName)
    {
        yield return null;
    }

}
