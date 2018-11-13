using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI관련되서 사용할 매니저입니다.
/// </summary>
public class UIManager : Singleton<UIManager>
{
    public GameObject popupText;
    public GameObject popupTextRed;
    public GameObject popupTextYellow;

    public GameObject GameClearUI;
    public GameObject GameOverUI;

    private PlayerStat player;
    private float playerMaxHP;
    private float playerCurrentHP;

	// Use this for initialization
	void Start ()
    {
        player = Player.instance.playerStat;
        playerMaxHP = player.GetMaxHP();
        playerCurrentHP = player.GetCurrentHP();

	}
	
	// Update is called once per frame
	void Update ()
    {
        playerCurrentHP = player.GetCurrentHP();    

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    CreatePopupTextRed("HeadShot", Vector3.zero);
        //    //CreatePopupTextRed("50", Vector3.zero);
        //}

	}

    public void CreatePopupText(string text, Vector3 position)
    {
        GameObject textObject;
        PoolManager.Instance.PopObject(popupText, out textObject);
        textObject.transform.position = position + new Vector3(Random.Range(-1,1),0.0f,Random.Range(-1,1));
        Vector3 dir = textObject.transform.position -  Player.instance.hmdTransform.position;
        textObject.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        textObject.GetComponent<PopupText>().CreatePopupText(text);
    }

    public void CreatePopupTextRed(string text, Vector3 position)
    {
        GameObject textObject;
        PoolManager.Instance.PopObject(popupTextRed, out textObject);
        textObject.transform.position = position + new Vector3(Random.Range(-1, 1), 0.0f, Random.Range(-1, 1));
        Vector3 dir = textObject.transform.position - Player.instance.hmdTransform.position;
        textObject.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        textObject.GetComponent<PopupText>().CreatePopupText(text);
    }

    public void CreatePopupTextYellow(string text, Vector3 position)
    {
        GameObject textObject;
        PoolManager.Instance.PopObject(popupTextYellow, out textObject);
        textObject.transform.position = position + new Vector3(Random.Range(-1, 1), 0.0f, Random.Range(-1, 1));
        Vector3 dir = textObject.transform.position - Player.instance.hmdTransform.position;
        textObject.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        textObject.GetComponent<PopupText>().CreatePopupText(text);
    }

}
