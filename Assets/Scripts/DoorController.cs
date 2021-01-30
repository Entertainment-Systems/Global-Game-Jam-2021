using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;

    private Animator animator;
    string doorCloseAnim = "DoorClose";
    string doorOpenAnim = "DoorOpen";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameEvents.current.DoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.DoorwayTriggerExit += OnDoorwayClose;
    }

    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
            animator.Play(doorCloseAnim);
            //transform.Translate(Vector3.up * -10);
    }

    private void OnDoorwayOpen(int id)
    {
        if (id == this.id)
            animator.Play(doorOpenAnim);
        //transform.Translate(Vector3.up * 10);
    }
}
