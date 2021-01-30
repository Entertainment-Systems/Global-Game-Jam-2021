﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{ private AudioSource _audioSource;

    [SerializeField] private float obstacleSoundDampening = 2f;

    [Serializable]
    public struct AudioWithVolume
    {
        public AudioClip clip;
        public float audibleRange;
    }

    public AudioWithVolume[] clips;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    

    public void Play(int i)
    {
        if(clips.Length > 0)
        {
            _audioSource.clip = clips[i].clip;
            _audioSource.Play();
            GameEvents.current.OnNoisePlayed(clips[i].audibleRange, transform.position);
        }
    }

    public void Play()
    {
        if(clips.Length > 0)
        {
            Random rand = new Random();
            int clip = rand.Next(clips.Length);
            
            _audioSource.clip = clips[clip].clip;
            _audioSource.Play();
            GameEvents.current.OnNoisePlayed(clips[clip].audibleRange, transform.position);
        }
    }

    

    private void OnCollisionEnter(Collision other)
    {
        GameEvents.current.OnNoisePlayed(20f, transform.position);
    }
}
