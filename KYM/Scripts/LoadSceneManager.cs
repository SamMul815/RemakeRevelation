using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonDonDestroy<LoadSceneManager> {

    //public GameObject loadingPlayer; 

    public void LoadScene(float fadeTime, string nextSceneName)
    {
        StartCoroutine(corLoadingScene(fadeTime, nextSceneName));
    }

    public void LoadScenes(float fadeTime, string[] nextSceneName)
    {
        StartCoroutine(corLoadingScenes(fadeTime, nextSceneName));
        //StartCoroutine(corLoadingScenesTest(fadeTime, nextSceneName));
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
                    //Player.instance.playerUI.FadeIn(2.0f);
                    break;
                }

                yield return null;
            }

        }
        Time.timeScale = 1.0f;
    }

    IEnumerator corLoadingScenesTest(float fadeTime, string[] nextSceneName)
    {
        yield return null;
        Player.instance.playerUI.FadeOut(fadeTime);
        yield return new WaitForSecondsRealtime(fadeTime);


        AsyncOperation[] aOps = new AsyncOperation[nextSceneName.Length];
        for (int i = 0; i < nextSceneName.Length; i++)
        {
            if(i == 0 )
                aOps[i] = SceneManager.LoadSceneAsync(nextSceneName[i], LoadSceneMode.Single);
            else
                aOps[i] = SceneManager.LoadSceneAsync(nextSceneName[i], LoadSceneMode.Additive);
            aOps[i].allowSceneActivation = false;
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
                break;
            }

            yield return null;
        }

    }

}
