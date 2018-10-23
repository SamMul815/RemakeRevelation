using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

[CreateAssetMenu(menuName = "Test")]
public class ExampleClass : ScriptableObject
{
    [SerializeField]
    private GameObject _go;
    public GameObject Go { get { return _go; } }

    private void OnEnable()
    {
        SerializeNodes(_go);
        Debug.Log("Test");
    }

    void SerializeNodes(GameObject go)
    {
        MeshFilter[] meshFilters = go.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;    
        }
        go.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        go.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        go.transform.gameObject.SetActive(true);
    }

#if UNITY_EDITOR
    public static ExampleClass Create()
    {
        ExampleClass asset = CreateInstance<ExampleClass>();

        AssetDatabase.CreateAsset(asset, "Assets/BehaviroTree.asset");
        AssetDatabase.SaveAssets();

        return asset;
    }
#endif

}