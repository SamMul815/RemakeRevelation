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

    Vector3[] vertices;

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

        vertices = new Vector3[]
        {
            _wingTransform[0].position,
            _wingTransform[1].position,
            _wingTransform[2].position,
            _wingTransform[3].position,
            _wingTransform[4].position,
            _wingTransform[5].position,
            _wingTransform[6].position,
            _wingTransform[7].position,
            _wingTransform[8].position,
            _wingTransform[9].position,
            _wingTransform[10].position,
            _wingTransform[11].position,
            _wingTransform[12].position,
            _wingTransform[13].position,
        };

        for (int i = 0; i < vertices.Length; i++)
        {
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

        _wingMesh.vertices = vertices;
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
