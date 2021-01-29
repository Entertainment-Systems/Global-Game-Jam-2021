using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    //EVENTS
    public event Action<int> onDoorwayTriggerEnter;
    public event Action<int> onDoorwayTriggerExit;
    public event Action<float> onPillPicked;

    //CALLS
    public void DoorwayTriggerEnter(int id)
    {
        if (onDoorwayTriggerEnter != null)
            onDoorwayTriggerEnter(id);
    }
    
    public void DoorwayTriggerExit(int id)
    {
        if (onDoorwayTriggerExit != null)
            onDoorwayTriggerExit(id);
    }


    public void OnOnPillPicked(float val)
    {
        if (onPillPicked != null) 
            onPillPicked(val);
    }
}
