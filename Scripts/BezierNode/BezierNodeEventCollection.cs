using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class BezierNodeEventCollection : MonoBehaviour
{
    private MovementManager _manager;

    private void Awake()
    {
        _manager = MovementManager.Instance;
    }

    private void Rotation()
    {
        //Vector3 forward = this.transform.position - _manager.transform.position;
    }
}
