using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSound : MonoBehaviour
{
    AudioSource audioSource;
    //public 
    bool isPlay = false;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetClip(AudioClip _audioClip)
    {
        audioSource.clip = _audioClip;
    }

    public void Change2D()
    {
        audioSource.spatialBlend = 0.0f;
    }
    public void Change3D()
    {
        audioSource.spatialBlend = 1.0f;
    }
    //public void Play()
    //{
    //    audioSource.Play();
    //    isPlay = true;
    //}

    public void Play(float startTime = 0.0f)
    {
        audioSource.Play();
        isPlay = true;
        audioSource.time = startTime;
    }

    public void LoopOn()
    {
        audioSource.loop = true;
    }

    private void Update()
    {
        if (!audioSource.isPlaying && isPlay)
        {
            SoundManagerNormal.Instance.AddEmptyAudioSourceObject(this.gameObject);
        }

        //audioSource.time
    }

    public void Reset()
    {
        isPlay = false;
        audioSource.Stop();
        audioSource.loop = false;
    }
}