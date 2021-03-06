﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

[Serializable]
public struct EvnData
{
    [Range(0.0f, 1.0f)]
    public float RunTime;
    public bool IsLoop;

    public EvnData(float runTime, bool isLoop)
    {
        this.RunTime = runTime;
        this.IsLoop = isLoop;
    }
}


[RequireComponent(typeof(DragonAnimStateEventCollection))]
public class AnimStateEventsManager : MonoBehaviour
{
    private Animator[] animators;

    private Dictionary<string, BaseSMB> animSMB = new Dictionary<string, BaseSMB>();

    private DragonAnimStateEventCollection stateEventCollection;
    public DragonAnimStateEventCollection StateEventCollection { get { return stateEventCollection; } }
  
    private void Awake()
    {
        animators = GetComponents<Animator>();
        stateEventCollection = GetComponent<DragonAnimStateEventCollection>();

    }

    private void Start()
    {
        Dictionary<string, Action<EvnData>> EnterEventFunc = stateEventCollection.AnimEnterEventFunc;
        Dictionary<string, Action<EvnData>> ExitEventFunc = stateEventCollection.AnimExitEventFunc;
        Dictionary<string, List<Action<EvnData>>> TimeEventFunc = stateEventCollection.AnimTimeEventFunc;

        foreach (Animator a in animators)
        {
            BaseSMB[] bsmbs = a.GetBehaviours<BaseSMB>();
            foreach (BaseSMB smb in bsmbs)
            {
                string SMBKey = smb.SMBKeyName;

                animSMB.Add(SMBKey, smb);

                if (EnterEventFunc.ContainsKey(SMBKey))
                { 
                    smb.SetStateEnterEvent(EnterEventFunc[SMBKey]);
                }

                if (TimeEventFunc.ContainsKey(SMBKey))
                {
                    foreach (Action<EvnData> action in TimeEventFunc[SMBKey])
                    {
                        stateEventCollection.AddAnimTimeEventFunc(action, SMBKey);
                        stateEventCollection.AddIsAnimTimeEventRun(false, SMBKey);
                    }
                    smb.IsRunning = stateEventCollection.IsAnimTimeEventRun[SMBKey];
                    smb.SetStateTimeEventLListener(stateEventCollection.AnimTimeEventFunc[SMBKey]);
                }

                if (ExitEventFunc.ContainsKey(SMBKey))
                { 
                    smb.SetStateExitEvent(ExitEventFunc[SMBKey]);
                }

            }
        }
    }
}
