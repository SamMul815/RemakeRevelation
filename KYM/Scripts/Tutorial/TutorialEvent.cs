using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : Singleton<TutorialEvent>
{
    public enum TutorialState
    {
        IDLE = 0,
        TELEPORT1,
        TELEPORT2,
        MOVEGUNROOM,
        GUNROOM,
        GUN1,
        GUN2,
        MOVESKILLROOM,
        SKILLROOM,
        SKILL1,
        SKILL2,
        END
    }

    public GameObject[] Images;
    public GameObject logo;
    public GameObject teleportImage1;
    public GameObject teleportPoint1;
    public GameObject teleportImage2;
    public GameObject teleportPoint2;
    public GameObject teleporter;

    public GameObject gunPrefab;
    public TutorialState currentState;

    private TutorialHand leftHand;
    public TutorialHand LeftHand { get { return leftHand; } set { leftHand = value; } }
    private TutorialHand rightHand;
    public TutorialHand RightHand { get { return rightHand; } set { rightHand = value; } }
    

	// Use this for initialization
	void Start ()
    {
        //Images = new GameObject[(int)TutorialState.END];
        //tutorialObject = new List<GameObject>();

        logo.SetActive(false);
        teleportImage1.SetActive(false);
        teleportImage2.SetActive(false);
        teleporter.SetActive(false);
        teleportPoint1.SetActive(false);
        teleportPoint2.SetActive(false);

        StartCoroutine(CorTutorial());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void EventChange(TutorialState state)
    {
        currentState = state;
    }


    IEnumerator CorTutorial()
    {
        logo.SetActive(true);

        yield return new WaitForSeconds(5.0f);
        logo.SetActive(false);
        teleportImage1.SetActive(true);
        teleporter.SetActive(true);
        teleportPoint1.SetActive(true);
        leftHand.HighlightOnButton("trackpad");
        rightHand.HighlightOnButton("trackpad");

        while (currentState < TutorialState.TELEPORT1)
        {
            Player.instance.rightHand.Vibration(0.1f, 3000.0f);
            Player.instance.leftHand.Vibration(0.1f, 3000.0f);
            yield return new WaitForSecondsRealtime(0.2f);
        }

        teleportImage1.SetActive(false);
        teleportImage2.SetActive(true);
        teleportPoint1.SetActive(false);
        teleportPoint2.SetActive(true);

        while (currentState < TutorialState.TELEPORT2)
        {
            Player.instance.rightHand.Vibration(0.1f, 3000.0f);
            Player.instance.leftHand.Vibration(0.1f, 3000.0f);
            yield return new WaitForSecondsRealtime(0.2f);
        }

        leftHand.HighlightOffButton("trackpad");
        rightHand.HighlightOffButton("trackpad");
        teleportImage2.SetActive(false);
        teleportPoint2.SetActive(false);


        while (currentState < TutorialState.MOVEGUNROOM)
        {
            yield return new WaitForEndOfFrame();
        }

        GameObject gun1 = Instantiate(gunPrefab);
        GameObject gun2 = Instantiate(gunPrefab);
        Player.instance.leftHand.AttachObject(gun1);
        Player.instance.rightHand.AttachObject(gun2);



        while (currentState < TutorialState.GUN1)
        {
            yield return new WaitForEndOfFrame();
        }


        yield return null;


    }
}
