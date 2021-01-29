using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fovChanger : MonoBehaviour
{
    float high;

    void Start()
    {
        GameEvents.current.onPillPicked += OnPillPickUp;
    }

    private void OnPillPickUp(float high)
    {
        high = GameStates.current.High;
        Camera.main.fieldOfView = 120;
        Debug.Log("High A F" + high);
    }
}