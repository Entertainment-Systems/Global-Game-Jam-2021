using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fovChanger : MonoBehaviour
{
    public float highMod = 45f;

    private float baseFov;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        baseFov = Camera.main.fieldOfView;
    }

    private void Update()
    {
        _camera.fieldOfView = baseFov + highMod * GameStates.current.High;
    }
}