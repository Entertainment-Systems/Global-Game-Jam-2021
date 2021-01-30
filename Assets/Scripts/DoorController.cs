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
        GameEvents.current.DoorwayTriggerEnter += DoorwayOpen;
        GameEvents.current.DoorwayTriggerExit += DoorwayClose;
    }

    private void DoorwayClose(int id)
    {
        if (id == this.id)
            transform.Translate(Vector3.up * -10);
    }

    private void DoorwayOpen(int id)
    {
        if (id == this.id)
            transform.Translate(Vector3.up * 10);
    }
}
