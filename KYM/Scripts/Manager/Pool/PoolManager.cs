using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
struct PoolObjectData
{
    [SerializeField] public GameObject poolObject;
    [SerializeField] public int initialCount;
}

public class PoolManager : Singleton<PoolManager>
{
    //초기화 미리 만들어 사용할 오브젝트
    [SerializeField]
    private PoolObjectData[] poolObjects;

    [SerializeField]
    Dictionary<string, Stack<GameObject>> poolStacks;

    IEnumerator Start()
    {
        poolStacks = new Dictionary<string, Stack<GameObject>>();

        //미리 생성할 오브젝트들 생성하는 과정
        for(int i = 0; i < poolObjects.Length; i++)
        {
            yield return null;
            GameObject poolObject = poolObjects[i].poolObject;
            if (poolObject.GetComponent<PoolObject>() != null)
            {
                int initialCount = poolObjects[i].initialCount;
                for (int count = 0; count < initialCount; count++)
                {
                    GameObject copyObj = Instantiate(poolObject);
                    copyObj.SetActive(false);
                    //신 합치면서 다른곳에 생성될 수 있으니 MainScene으로 옮겨주는 작업
                    //SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName("MainScene"));
                    PushObject(copyObj);
                }
            }
            else
            {
                Debug.Log(poolObject.name + "PoolObject Init component is null");
            }
        }
        yield return null;
    }
    /// <summary>
    /// 사용 종료된 오브젝트를 PoolManager에 집어넣을때 사용하는 함수
    /// PoolObject 컴퍼넌트가 없으면 사용안됨
    /// </summary>
    /// <param name="_popObject">사용 종료된 오브젝트</param>
    public void PushObject(GameObject _pushObject)
    {
        PoolObject _poolObj = _pushObject.GetComponent<PoolObject>();
        if (_poolObj != null)
        {
            //if (_poolObj.Reset != null)
            //    _poolObj.Reset();
            //else
            //    Debug.Log("PooloObject Reset() NULL");

            if (poolStacks.ContainsKey(_poolObj.pooltag))
            {
                _pushObject.SetActive(false);
                poolStacks[_poolObj.pooltag].Push(_pushObject);
            }
            else
            {
                Stack<GameObject> stack = new Stack<GameObject>();
                _pushObject.SetActive(false);
                stack.Push(_pushObject);
                poolStacks.Add(_poolObj.pooltag, stack);
            }
        }
        else
        {
            Debug.LogError(_pushObject.name + " Not Found PoolObject Component");
        }
    }

    /// <summary>
    /// PoolManager로 관리하고 있는 오브젝트를 불러옵니다.
    /// PoolManager에 스택이 없을시 _original에 PoolObject 컴퍼넌트가 있으면
    /// 스택을 생성하고 _original 오브젝트를 복사하여 반환해줍니다.
    /// </summary>
    /// <param name="_original">원본 프리펩</param>
    /// <param name="_gameObject">반환 </param>
    public void PopObject(GameObject _original, out GameObject _gameObject)
    {
        PoolObject _poolObj = _original.GetComponent<PoolObject>();
        if(_poolObj != null)
        {
            if(poolStacks.ContainsKey(_poolObj.pooltag))
            {
                //_gameObject =  poolStacks[_poolObj.tag].Pop();
                if(poolStacks[_poolObj.pooltag].Count > 0)
                {
                    _gameObject = poolStacks[_poolObj.pooltag].Pop();
                    _gameObject.SetActive(true);
                }
                else
                {
                    _gameObject = Instantiate(_original); 
                }
            }
            else
            {
                Stack<GameObject> stack = new Stack<GameObject>();
                poolStacks.Add(_poolObj.pooltag, stack);
                _gameObject = Instantiate(_original);
            }
            if (_gameObject.GetComponent<PoolObject>().Init != null)
                _gameObject.GetComponent<PoolObject>().Init();
            else
                Debug.Log("PooloObject Init() NULL");
        }
        //객체에 PoolObject가 없음
        //이경우도 만들려면 만들수 있는데 너무 소모 비용이 커서 딱히 만들 필요가 없어보임...
        else
        {
            _gameObject = null;
            Debug.LogError(_original.name + "PoolObject Component is null");
        }
    }
    /// <summary>
    /// PoolManager로 관리하고 있는 오브젝트를 불러옵니다.
    /// PoolManager에 스택이 없을시 _original에 PoolObject 컴퍼넌트가 있으면
    /// 스택을 생성하고 _original 오브젝트를 복사하여 반환해줍니다.
    /// </summary>
    /// <param name="_original">원본 프리펩</param>
    /// <param name="_position">생성 위치</param>
    /// <param name="_gameObject">반환</param>
    public void PopObject(GameObject _original, Vector3 _position, out GameObject _gameObject)
    {
        PoolObject _poolObj = _original.GetComponent<PoolObject>();
        if (_poolObj != null)
        {
            if (poolStacks.ContainsKey(_poolObj.pooltag))
            {
                //_gameObject =  poolStacks[_poolObj.tag].Pop();
                if (poolStacks[_poolObj.pooltag].Count > 0)
                {
                    _gameObject = poolStacks[_poolObj.pooltag].Pop();
                    _gameObject.transform.position = _position;
                    _gameObject.SetActive(true);
                }
                else
                {
                    _gameObject = Instantiate(_original, _position, Quaternion.identity);
                }
            }
            else
            {
                Stack<GameObject> stack = new Stack<GameObject>();
                poolStacks.Add(_poolObj.pooltag, stack);
                _gameObject = Instantiate(_original, _position, Quaternion.identity);
            }
            if (_gameObject.GetComponent<PoolObject>().Init != null)
                _gameObject.GetComponent<PoolObject>().Init();
            else
                Debug.Log("PooloObject Init() NULL");
        }
        //객체에 PoolObject가 없음
        //이경우도 만들려면 만들수 있는데 너무 소모 비용이 커서 딱히 만들 필요가 없어보임...
        else
        {
            _gameObject = null;
            Debug.LogError(_original.name + "PoolObject Component is null");
        }
    }

    /// <summary>
    /// PoolManager로 관리하고 있는 오브젝트를 불러옵니다.
    /// PoolManager에 스택이 없을시 _original에 PoolObject 컴퍼넌트가 있으면
    /// 스택을 생성하고 _original 오브젝트를 복사하여 반환해줍니다.
    /// </summary>
    /// <param name="_original">원본 프리펩</param>
    /// <param name="_position">생성 위치</param>
    /// <param name="_rot">회전값</param>
    /// <param name="_gameObject">반환</param>
    public void PopObject(GameObject _original, Vector3 _position, Quaternion _rot,  out GameObject _gameObject)
    {
        PoolObject _poolObj = _original.GetComponent<PoolObject>();
        if (_poolObj != null)
        {
            if (poolStacks.ContainsKey(_poolObj.pooltag))
            {
                if (poolStacks[_poolObj.pooltag].Count > 0)
                {
                    _gameObject = poolStacks[_poolObj.pooltag].Pop();
                    _gameObject.transform.position = _position;
                    _gameObject.transform.rotation = _rot;
                    _gameObject.SetActive(true);
                }
                else
                {
                    _gameObject = Instantiate(_original, _position, _rot);
                    _gameObject.SetActive(true);
                }
            }
            else
            {
                Stack<GameObject> stack = new Stack<GameObject>();
                poolStacks.Add(_poolObj.pooltag, stack);
                _gameObject = Instantiate(_original, _position, _rot);
            }
            if (_gameObject.GetComponent<PoolObject>().Init != null)
                _gameObject.GetComponent<PoolObject>().Init();
            else
                Debug.Log("PooloObject Init() NULL");
        }
        //객체에 PoolObject가 없음
        //이경우도 만들려면 만들수 있는데 너무 소모 비용이 커서 딱히 만들 필요가 없어보임...
        else
        {
            _gameObject = null;
            Debug.LogError(_original.name + "PoolObject Component is null");
        }
    }

    /// <summary>
    /// 빠른 계산을 이용하기 위해서 만든 PopObject 미리 등록을 해둬야 사용가능
    /// 스택에 오브젝트가 없거나 생성된 스택이 없으면 Null반환
    /// </summary>
    /// <param name="_tag"></param>
    /// <param name="_gameobject"></param>
    public void PopObject(PoolObject _poolObj, out GameObject _gameObject)
    {
        if (poolStacks.ContainsKey(_poolObj.pooltag))
        {
            //_gameObject =  poolStacks[_poolObj.tag].Pop();
            if (poolStacks[_poolObj.pooltag].Count > 0)
            {
                _gameObject = poolStacks[_poolObj.pooltag].Pop();
                _gameObject.SetActive(true);
            }
            else
            {
                _gameObject = null;
                Debug.LogError("PoolManager Pop Error Stack Empty");
            }
        }
        else
        {
            _gameObject = null;
            Debug.LogError("PoolManager Pop Error Can't Found Stack");
        }
        if (_poolObj.Init != null)
            _poolObj.Init();
        else
            Debug.Log("PooloObject Init() NULL");
    }
}



