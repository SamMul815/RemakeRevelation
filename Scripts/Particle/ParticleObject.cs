using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : PoolObject
{
    [SerializeField]
    private float _holdingTime;
    public float HoldingTime { get { return _holdingTime; } }

    private float _currentTime;

    public void Awake()
    {
        this.Init = Initialize;
        Initialize();
    }

    private void Initialize()
    {
        _currentTime = _holdingTime;
    }
	
	void Update ()
    {
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0.0f)
        {
            PoolManager.Instance.PushObject(this.gameObject);
        }
    }
}
