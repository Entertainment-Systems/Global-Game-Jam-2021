using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseEnable : MonoBehaviour
{
    [SerializeField] private GameObject Objects;
    public int phase;

    void Start()
    {
        Objects.SetActive(false);
        GameEvents.current.PhaseChanged += ObjectsEnable;
    }

    void ObjectsEnable(int phase)
    {
        if (phase == this.phase)
            Objects.SetActive(true);

    }
}
