using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseDoorController : MonoBehaviour
{
    private Animator animator;

    public int phase;
    string doorOpenAnim = "DoorOpen";

    void Start()
    {
        animator = GetComponent<Animator>();
        GameEvents.current.PhaseChanged += doorOpen;
    }

    // Update is called once per frame
    private void doorOpen(int phase)
    {
        if (phase == this.phase)
            animator.Play(doorOpenAnim);
    }
}
