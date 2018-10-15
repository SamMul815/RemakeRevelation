using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DragonController;

public class BaseAnimStateEventsCollection : MonoBehaviour
{
    protected DragonManager _manager; //변수형 타입은 DragonManager 변수명은 _manager;
    public DragonManager Manager { get { return _manager; } }

    protected BlackBoard _blackBoard;
    public BlackBoard BlackBoard { get { return _blackBoard; } }

    protected Dictionary<string, Action<EvnData>> _animEnterEventFunc = new Dictionary<string, Action<EvnData>>();
    public Dictionary<string, Action<EvnData>> AnimEnterEventFunc { get { return _animEnterEventFunc; } }

    protected Dictionary<string, Action<EvnData>> _animExitEventFunc = new Dictionary<string, Action<EvnData>>();
    public Dictionary<string, Action<EvnData>> AnimExitEventFunc { get { return _animExitEventFunc; } }

    protected Dictionary<string, List<Action<EvnData>>> _animTimeEventFunc = new Dictionary<string, List<Action<EvnData>>>();
    public Dictionary<string, List<Action<EvnData>>> AnimTimeEventFunc { get { return _animTimeEventFunc; } }

    protected Dictionary<string, List<bool>> isAnimTimeEventRun = new Dictionary<string, List<bool>>();
    public Dictionary<string, List<bool>> IsAnimTimeEventRun { get { return isAnimTimeEventRun; } }

    protected virtual void Awake()
    {
        _manager = DragonManager.Instance;
        _blackBoard = BlackBoard.Instance;
    }

    protected List<bool> GetIsAnimTimeEventRun(Dictionary<string, List<bool>> Target, string Key)
    {
        List<bool> IsAnimEventRun;
        if (Target.ContainsKey(Key))
        {
            IsAnimEventRun = Target[Key];
        }
        else
        {
            IsAnimEventRun = new List<bool>();
            Target[Key] = IsAnimEventRun;
        }
        return IsAnimEventRun;
    }

    public void AddIsAnimTimeEventRun(bool IsAnimRun, string Key)
    {
        List<bool> IsAnimRunList = GetIsAnimTimeEventRun(isAnimTimeEventRun, Key);
        IsAnimRunList.Add(IsAnimRun);
    }

    protected List<Action<EvnData>> GetAnimTimeEventFunc(Dictionary<string, List<Action<EvnData>>> Target, string Key)
    {
        List<Action<EvnData>> evnDataFunc;
        if (Target.ContainsKey(Key))
        {
            evnDataFunc = Target[Key];
        }
        else
        {
            evnDataFunc = new List<Action<EvnData>>();
            Target[Key] = evnDataFunc;
        }

        return evnDataFunc;
    }

    public void AddAnimTimeEventFunc(Action<EvnData> evnData, string Key)
    {
        List<Action<EvnData>> evnDataFunc = GetAnimTimeEventFunc(_animTimeEventFunc, Key);

        if (!evnDataFunc.Contains(evnData))
        {
            evnDataFunc.Add(evnData);
        }

    }

    protected Action<EvnData> GetAnimEnterEventFunc(Dictionary<string, Action<EvnData>> Target, string Key)
    {
        Action<EvnData> evnDataFunc;
        if (Target.ContainsKey(Key))
        {
            evnDataFunc = Target[Key];
        }
        else
        {
            return null;
        }

        return evnDataFunc;
    }

    public void AddAnimEnterEventFunc(Action<EvnData> evnData, string Key)
    {
        Action<EvnData> evnDataFunc = GetAnimEnterEventFunc(_animEnterEventFunc, Key);

        if (evnDataFunc == null)
        {
            _animEnterEventFunc.Add(Key, evnData);
        }
    }

    protected Action<EvnData> GetAnimExitEventFunc(Dictionary<string, Action<EvnData>> Target, string Key)
    {
        Action<EvnData> evnDataFunc;
        if (Target.ContainsKey(Key))
        {
            evnDataFunc = Target[Key];
        }
        else
        {
            return null;
        }

        return evnDataFunc;
    }

    public void AddAnimExitEventFunc(Action<EvnData> evnData, string Key)
    {
        Action<EvnData> evnDataFunc = GetAnimExitEventFunc(_animExitEventFunc, Key);
        if(evnDataFunc == null)
        {
            _animExitEventFunc.Add(Key, evnData);
        }
    }


}
