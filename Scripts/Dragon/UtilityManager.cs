using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;


/// <summary>
/// 작성일 : 2018 - 05 - 09
/// 직상지 : 김영민
/// 작업내용 : 유틸리티 매니저 제작
/// 다른곳에서 자주 불리는 애들 또는 자주 하는 계산 여기서 가져다 놓고 사용
/// </summary>

public class UtilityManager :  Singleton<UtilityManager>
{
    [SerializeField] private Transform _player;
    public Transform Player { get { return _player; } }


    public static bool DistanceCalc(Transform This, Transform Target, float Range)
    {
        if (Vector3.Distance(This.position, Target.position) <= Range)
            return true;

        return false;
    }

    public static bool DistanceCalc(Vector3 This, Vector3 Target, float Range)
    {
        float Distance = (This - Target).sqrMagnitude;

        if (Distance <= Range * Range)
            return true;

        return false;

    }

}
