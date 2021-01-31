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
    public event Action<int> PlayerKilled;
    public event Action<int> PlayerLostLife;
    public event Action<int> PlayerAttacked;
    //Sub to this to see which phase was just activate then activate whatever stuff you want to swap
    public event Action<int> PhaseChanged;

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

    public void OnPlayerKilled(int id)
    {
        PlayerKilled?.Invoke(id);
    }

    public void OnPlayerLostLife(int livesLeft)
    {
        PlayerLostLife?.Invoke(livesLeft);
    }

    public void OnPlayerAttacked(int obj)
    {
        PlayerAttacked?.Invoke(obj);
    }

    //Call this with a number of phase you want to activate
    public void OnPhaseChanged(int phase)
    {
        PhaseChanged?.Invoke(phase);
    }
}
