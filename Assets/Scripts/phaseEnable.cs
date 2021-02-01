using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phaseEnable : MonoBehaviour
{
    [SerializeField] private GameObject AppearingObjects;
    [SerializeField] private GameObject DisappearingObjects;

    public int phase;

    void Start()
    {
        AppearingObjects.SetActive(false);
        DisappearingObjects.SetActive(true);
        GameEvents.current.PhaseChanged += ObjectsEnable;
    }

    void ObjectsEnable(int phase)
    {
        if (phase == this.phase)
        {
            AppearingObjects.SetActive(true);
            DisappearingObjects.SetActive(false);
        }
    }
}
