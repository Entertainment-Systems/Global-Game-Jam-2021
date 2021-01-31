using System;
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
        _audioSource.spatialBlend = 1f;
    }


    public void Play(int i, float volume = 1f, float pitch = 1f)
    {
        if(clips.Length > 0)
        {
            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(clips[i].clip, volume);
            GameEvents.current.OnNoisePlayed(clips[i].audibleRange, transform.position);
        }
    }

    public void Play(float volume = 1f, float pitch = 1f)
    {
        if(clips.Length > 0)
        {
            Random rand = new Random();
            int clip = rand.Next(clips.Length);

            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(clips[clip].clip, volume);
            GameEvents.current.OnNoisePlayed(clips[clip].audibleRange, transform.position);
        }
    }
}
