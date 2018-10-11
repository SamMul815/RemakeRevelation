using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopupText : MonoBehaviour {

    Text popupText;
    Animator animator;
    AnimatorClipInfo clip;
    float time = 0;
    float liveTime = 0;
    private void Awake()
    {
        popupText = GetComponentInChildren<Text>();
        animator = GetComponent<Animator>();
        clip = animator.GetCurrentAnimatorClipInfo(0)[0];
        time = clip.clip.length;
        liveTime = time;
        GetComponent<PoolObject>().Init = Init;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(liveTime < 0.0f)
        {
            PoolManager.Instance.PushObject(this.gameObject);
        }
        liveTime -= Time.deltaTime;
	}
    public void CreatePopupText(string _text)
    {
        popupText.text = _text;
    }

    void Init()
    {
        liveTime = time;
    }

}
