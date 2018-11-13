using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRetryButton : VRButton
{
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        ButtonEvent = LoadScene;
    }

    protected override void Update ()
    {
        base.Update();
    }

    private void LoadScene ()
    {
        LoadSceneManager.Instance.LoadMainGame(0.0f);
    }

    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(_leftTransform.position, _leftTransform.forward * _rayDistance);
        Gizmos.DrawRay(_rightTransform.position, _rightTransform.forward * _rayDistance);
    }

}
