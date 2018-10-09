using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour {

    public Transform playerCamera;
    private Vector3 cameraShakePos;
    private Vector3 cameraShakeRot;

	// Use this for initialization
	void Start ()
    {
        cameraShakePos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    PlayerShake();
        //    //StartCoroutine(CorShakeCamera(2.5f, 0.75f, 0.02f));
        //}
        //this.transform.localPosition = cameraShakePos;
        this.transform.localRotation = Quaternion.Euler(cameraShakeRot);

    }

    void PlayerHitFilter()
    {

    }

    public void PlayerShake(float _playTime = 0.3f, float _radius = 0.2f, float _waitTime = 0.01f)
    {
        StartCoroutine(CorShakeCamera(_playTime, _radius, _waitTime));
    }

    IEnumerator CorShakeCamera(float _playTime, float _radius, float _waitTime)
    {
        yield return new WaitForEndOfFrame();

        float _shakingPlayTime = _playTime;
        float _shakingRadius = _radius;
        float _shkaingWaitTime = _waitTime;
        float _time = _shakingPlayTime;
        while (_time > 0)
        {
            Vector3 shakingPos = (Random.insideUnitSphere + new Vector3(-0.5f, -0.5f, -0.5f)) * _shakingRadius;
            cameraShakePos = (
                playerCamera.up * shakingPos.y + 
                playerCamera.right * shakingPos.x + 
                playerCamera.forward * shakingPos.z) * _time / _shakingPlayTime;

            cameraShakeRot = new Vector3(
                shakingPos.x * 10.0f,
                shakingPos.y * 10.0f,
                0.0f
                ) * _time / _shakingPlayTime;


            //cameraShakePos.x = shakingPos.x * _time / _shakingPlayTime;
            //cameraShakePos.y = shakingPos.y * _time / _shakingPlayTime;
            _time -= _shkaingWaitTime;
            yield return new WaitForSeconds(_shkaingWaitTime);
        }
        cameraShakePos = Vector3.zero;
        cameraShakeRot = Vector3.zero;
        yield return null;
    }


}
