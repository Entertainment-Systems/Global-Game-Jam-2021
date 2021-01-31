using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    public float pillValue = .2f;
    public AudioClip pickupSound;
    private void OnTriggerEnter(Collider other)
    {        
        if(other.CompareTag("Player"))
        {
            //Add pills value to the current high
            GameEvents.current.OnPillPicked(pillValue);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);
        }
    }
}
