using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWingCollision : MonoBehaviour
{
    [SerializeField]
    private Transform[] _wingTransform;
    public Transform[] WingTransform { get { return _wingTransform; } }
    
    private MeshCollider _wingCollider;
    private Mesh _wingMesh;

    private Vector3[] vertices;

    private int[] triangles;

    private void Awake()
    {

        _wingCollider = GetComponent<MeshCollider>();
        _wingMesh = new Mesh();
        vertices = new Vector3[_wingTransform.Length];

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
    }

    private void Update()
    {
        WingCollider();
    }

    private void WingCollider()
    {

        for (int i = 0; i < _wingTransform.Length; i++)
        {
            vertices[i] = _wingTransform[i].position;
        }

        _wingMesh.vertices = vertices;
        _wingMesh.triangles = triangles;

        _wingMesh.MarkDynamic();
        _wingMesh.name = this.gameObject.name;
        _wingCollider.sharedMesh = _wingMesh;

    }

}
