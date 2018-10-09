using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWingCollision : MonoBehaviour
{
    [SerializeField]
    private Transform[] _wingTransform;
    public Transform[] WingTransform { get { return _wingTransform; } }
    
    private MeshCollider _wingCollider;
    Mesh _wingMesh;

    List<Vector3> vertices = new List<Vector3>();

    int[] triangles;

    IEnumerator _wingColliderCor;

    private void Awake()
    {
        _wingColliderCor = WingColliderUpdate();

        _wingCollider = GetComponent<MeshCollider>();
        _wingMesh = new Mesh();

        CoroutineManager.DoCoroutine(_wingColliderCor);
    }

    private void WingCollider()
    {
        _wingMesh.Clear();
        vertices.Clear();

        for (int i = 0; i < _wingTransform.Length; i++)
        {
            vertices.Add(_wingTransform[i].position);
            vertices[i] -= this.transform.position;
        }

        triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3,
            2, 3, 4,

            5, 6, 7,
            6, 7, 1,
            7, 1, 8,

            1, 8, 3,
            3, 8, 9,
            5, 7, 10,

            7, 8, 10,
            8, 9, 10,
            10, 9, 11,

            10, 11 ,12,
            11, 12, 13

        };

        _wingMesh.vertices = vertices.ToArray();
        _wingMesh.triangles = triangles;

        _wingMesh.MarkDynamic();
        _wingMesh.name = this.gameObject.name;
        _wingCollider.sharedMesh = _wingMesh;
    }

    IEnumerator WingColliderUpdate()
    {
        while (true)
        {
            yield return CoroutineManager.EndOfFrame;
            WingCollider();

        }
    }


}
