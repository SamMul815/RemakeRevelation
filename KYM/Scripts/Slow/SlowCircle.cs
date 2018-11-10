using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCircle : MonoBehaviour
{

    //private float currentTime;
    //private float currentScale;

    IEnumerator corScaleChange;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScaleChange(Vector3 scale, float _time)
    {
        if(corScaleChange != null)
        {
            StopCoroutine(corScaleChange);
            corScaleChange = CorScaleChange(scale, _time);
            StartCoroutine(corScaleChange);
        }
        else
        {
            corScaleChange = CorScaleChange(scale, _time);
            StartCoroutine(corScaleChange);
        }

    }

    IEnumerator CorScaleChange(Vector3 scale, float _time)
    {
        Vector3 currentScale = this.transform.localScale;
        for (float currentTime = 0.0f; currentTime <= _time; currentTime += Time.unscaledDeltaTime)
        {
            transform.localScale =  Vector3.Lerp(currentScale, scale, currentTime / _time);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = scale;
        yield return null;
    }
}