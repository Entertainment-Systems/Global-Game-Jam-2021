using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(AudioPlayer))]
[RequireComponent(typeof(Rigidbody))]
public class Throwable : MonoBehaviour
{
    public float velocityNoiseThreshold = 1f;
    
    private AudioPlayer _audioPlayer;
    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioPlayer = GetComponent<AudioPlayer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(_rigidbody.velocity.magnitude >= velocityNoiseThreshold)
        {
            float pitch = Random.Range(.9f, 1.1f);
            float vol = Random.Range(.9f, 1.1f);

            _audioPlayer.Play(vol, pitch);
        }
    }
}
