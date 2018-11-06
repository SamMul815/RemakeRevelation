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
        OnButtonClick(_hand, _distance);

        if (_isOver)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (ButtonEvent != null)
                {
                    ButtonEvent();
                }
            }
        }
	}

    private void GameStartScene()
    {
        //SceneManager.LoadScene(1);
        Debug.Log("GaemStart");

    }

    protected override void OnButtonClick (PlayerHand hand, float distance)
    {
        base.OnButtonClick(hand, distance);
    }


}
