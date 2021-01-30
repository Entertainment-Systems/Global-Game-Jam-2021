using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fovChanger : MonoBehaviour
{
    [SerializeField]
    private float maxFov = 30;

    void Start()
    {
        GameEvents.current.onPillPicked += OnPillPickUp;
    }
    private void Update()
    {
        //Camera.main.fieldOfView = 75 + (GameStates.current.High * maxFov);

    }

    private void OnPillPickUp(float high)
    {
        //Debug.Log("High A F" + high);
    }
}