using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    public float pillValue = .2f;
    private void OnTriggerEnter(Collider other)
    {
        //Add pills value to the current high
        GameEvents.current.OnOnPillPicked(pillValue);
        Destroy(this);
    }
}
