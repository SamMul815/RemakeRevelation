//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//    ///// <summary>
//    ///// 원본 사이트
//    ///// http://lonpeach.com/2017/02/04/unity3d-singleton-pattern-example/
//    ///// </summary>
//    //public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
//    //{
//    //    private static T _instance = null;
//    //    private static object _syncobj = new object();
//    //    private static bool applsClosing = false;

//    //    public static T Instance
//    //    {
//    //        get
//    //        {
//    //            if (applsClosing)
//    //                return null;
//    //            lock (_syncobj)
//    //            {
//    //                if (_instance == null)
//    //                {
//    //                    T[] objs = FindObjectsOfType<T>();
//    //                    if (objs.Length > 0)
//    //                        _instance = objs[0];

//    //                    if (objs.Length > 1)
//    //                        Debug.LogError(typeof(T).Name + "하나 이상의 오브젝트가 씬에 존재합니다.");

//    //                    if (_instance == null)
//    //                    {
//    //                        string goName = typeof(T).ToString();
//    //                        GameObject go = GameObject.Find(goName);
//    //                        if (go == null)
//    //                            go = new GameObject(goName);
//    //                        _instance = go.AddComponent<T>();
//    //                    }
//    //                }
//    //                return _instance;
//    //            }
//    //        }
//    //    }

//    //    protected virtual void OnApplicationQuit()
//    //    {
//    //        applsClosing = true;
//    //    }

//    //}