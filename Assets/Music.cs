using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    public AudioClip music;
    public float pitchOffset = .5f;
    public float distortionStrength = .1f;
    
    private AudioSource _audioSource;

    private float targetPitch;
    private float currentPitch;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        targetPitch = 1f;
        currentPitch = targetPitch;

        _audioSource.clip = music;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        targetPitch = Random.Range(1 - GameStates.current.High * pitchOffset, 1 + GameStates.current.High * pitchOffset);
        
        if (_audioSource.pitch < targetPitch)
        {
            _audioSource.pitch += Time.deltaTime * distortionStrength;
            
        }
        else if(_audioSource.pitch > targetPitch)
        {
            _audioSource.pitch -= Time.deltaTime * distortionStrength;
            
        }
    }
}
