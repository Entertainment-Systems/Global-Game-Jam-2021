using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fovChanger : MonoBehaviour
{
    public float highMod = 45f;
    public float fovChangeSpeed = .01f;

    private float baseFov;
    private float targetFov;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        baseFov = Camera.main.fieldOfView;
        targetFov = baseFov;
    }

    private void Update()
    {
        float high = GameStates.current.High;
        targetFov = baseFov + highMod * high;
        
        if (_camera.fieldOfView - targetFov < 0)
        {
            Debug.Log(targetFov + " , " + _camera.fieldOfView);
            _camera.fieldOfView = _camera.fieldOfView + highMod * high * fovChangeSpeed * Time.deltaTime;
        }
    }
}