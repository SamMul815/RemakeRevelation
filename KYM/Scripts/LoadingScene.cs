using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : VRButton {

    public float waitTime;
    public string nextSceneName;

    protected override void Start()
    {
        base.Start();
        ButtonEvent = LoadScene;
    }

    private void LoadScene()
    {
        StartCoroutine(corLoading(waitTime, nextSceneName));
    }


    static IEnumerator corLoading(float waitTime, string nextSceneName)
    {
        yield return null;
        Player.instance.playerUI.FadeOut(2.0f);
        yield return new WaitForSecondsRealtime(3.0f);
        //Application.backgroundLoadingPriority = ThreadPriority.Low;
        //SceneManager.LoadSceneAsync("LoadingScene");
        
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);


        //SceneManager
        op.allowSceneActivation = false;

        while(!op.isDone)
        {
            yield return null;
            if(op.progress >= 0.9f)
            {
                Debug.Log("와일문 탈출");
                op.allowSceneActivation = true;
                break;
            }
        }
       

    }
    

}
