using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fovChanger : MonoBehaviour
{
    public float highMod = 45f;

    private float baseFov;

    void Start()
    {
        baseFov = Camera.main.fieldOfView;
        GameEvents.current.onPillPicked += PillPickIncreaseFOV;
    }

    private void Update()
    {
        Camera.main.fieldOfView = baseFov + highMod * GameStates.current.High;
    }

    private void PillPickIncreaseFOV(float val)
    {
        //Start coroutine
    }

    private IEnumerator IncreaseFov()
    {
        yield return null;
    }
}