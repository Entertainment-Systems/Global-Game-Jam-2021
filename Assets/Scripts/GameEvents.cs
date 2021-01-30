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
    public event Action<int> DoorwayTriggerEnter;
    public event Action<int> DoorwayTriggerExit;
    public event Action<float> PillPicked;
    public event Action<float, Vector3> NoisePlayed;

    //CALLS
    public void OnDoorwayTriggerEnter(int id)
    {
        if (DoorwayTriggerEnter != null)
            DoorwayTriggerEnter(id);
    }
    
    public void OnDoorwayTriggerExit(int id)
    {
        if (DoorwayTriggerExit != null)
            DoorwayTriggerExit(id);
    }


    public void OnPillPicked(float val)
    {
        PillPicked?.Invoke(val);
    }

    public void OnNoisePlayed(float range, Vector3 position)
    {
        NoisePlayed?.Invoke(range, position);
    }
}
