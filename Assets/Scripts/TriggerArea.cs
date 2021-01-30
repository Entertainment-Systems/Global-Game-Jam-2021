﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            GameEvents.current.OnDoorwayTriggerEnter(id);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            GameEvents.current.OnDoorwayTriggerExit(id);
    }
}
