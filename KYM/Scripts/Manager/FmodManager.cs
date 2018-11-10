using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodManager : Singleton<FmodManager>
{
    [System.Serializable]
    public struct FmodSound
    {
        [SerializeField]  public string tag;

        [FMODUnity.EventRef]
        [SerializeField] public string path;
    }

    public List<FmodSound> AudioList;
    public Dictionary<string, string> AudioData;

    private void Start()
    {
        AudioData = new Dictionary<string, string>();
        for(int i = 0; i<AudioList.Count; i++)
        {
            AudioData.Add(AudioList[i].tag, AudioList[i].path);
        }
    }

    public void PlaySoundOneShot(Vector3 pos, string tag)
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioData[tag], pos); 
    }
    public void PlaySoundAttatch(GameObject pos, string tag)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(AudioData[tag], pos);
    }

}



