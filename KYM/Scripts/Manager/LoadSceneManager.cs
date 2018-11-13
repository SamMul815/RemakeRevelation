using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonDonDestroy<LoadSceneManager> {

    public string[] TitleScenes = 
    {
        "lightmapscene_wall",
        "temple_lightmap",
         "TitleScene"
    };
    public string[] TutorialScenes =
    {
        "Tutorial"
    };
    public string[] MainGameScenes =
        {
        "lightmapscene_wall",
        "temple_lightmap",
        "KYM"

    };

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            LoadTitle(1.0f);
        }

        if(Input.GetKeyDown(KeyCode.F11))
        {
            LoadTutorial(1.0f);
        }
        if(Input.GetKeyDown(KeyCode.F12))
        {
            LoadMainGame(1.0f);
        }

    }

    public void LoadTitle(float waitTime)
    {
        LoadScenes(waitTime, TitleScenes);
    }

    public void LoadTutorial(float waitTime)
    {
        LoadScenes(waitTime, TutorialScenes);
    }

    public void LoadMainGame(float waitTime)
    {
        LoadScenes(waitTime, MainGameScenes);
    }

    public void LoadScene(float fadeTime, string nextSceneName)
    {
        StartCoroutine(corLoadingScene(fadeTime, nextSceneName));
    }

    public void LoadScenes(float fadeTime, string[] nextSceneName)
    {
        StartCoroutine(corLoadingScenes(fadeTime, nextSceneName));
    }

    IEnumerator corLoadingScene(float fadeTime, string nextSceneName)
    {
        yield return null;
        Player.instance.playerUI.FadeOut(fadeTime);
        yield return new WaitForSecondsRealtime(fadeTime);
        SceneManager.LoadSceneAsync(nextSceneName);
    }

    IEnumerator corLoadingScenes(float fadeTime, string[] nextSceneName)
    {
        yield return null;
        if(Player.instance != null)
            Player.instance.playerUI.FadeOut(fadeTime);
        yield return new WaitForSecondsRealtime(fadeTime);

        Time.timeScale = 0.0f;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName[0]); 
        while(!op.isDone)
        {
            yield return null;
        }

        if (nextSceneName.Length > 1)
        {
            AsyncOperation[] aOps = new AsyncOperation[nextSceneName.Length - 1];
            for (int i = 1; i < nextSceneName.Length; i++)
            {
                aOps[i - 1] = SceneManager.LoadSceneAsync(nextSceneName[i], LoadSceneMode.Additive);
                aOps[i - 1].allowSceneActivation = false;
            }

            while (true)
            {
                float progressValue = 0.0f;
                for (int i = 0; i < aOps.Length; i++)
                {
                    progressValue += aOps[i].progress;
                }

                if (progressValue >= 0.899f * aOps.Length)
                {
                    for (int i = 0; i < aOps.Length; i++)
                    {
                        aOps[i].allowSceneActivation = true;
                    }
                    //if(Player.instance != null)
                    //    Player.instance.playerUI.FadeIn(2.0f);
                    break;
                }

                yield return null;
            }

        }
        Time.timeScale = 1.0f;
    }

}
