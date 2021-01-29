using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;
    
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayClose;
    }

    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
            transform.Translate(Vector3.up * -10);
    }

    private void OnDoorwayOpen(int id)
    {
        if (id == this.id)
            transform.Translate(Vector3.up * 10);
    }
}
