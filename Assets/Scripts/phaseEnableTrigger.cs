using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseEnableTrigger : MonoBehaviour
{
    public int phase;
    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.OnPhaseChanged(phase);
    }
}
