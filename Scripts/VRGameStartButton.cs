using UnityEngine;
using UnityEngine.SceneManagement;

public class VRGameStartButton : VRButton
{

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        ButtonEvent = GameStartScene;
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
	}

    private void GameStartScene()
    {
        //SceneManager.LoadScene(1);
        Debug.Log("GaemStart");

    }
}
