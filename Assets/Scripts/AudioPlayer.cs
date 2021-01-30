using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{ private AudioSource _audioSource;

    [SerializeField] private float obstacleSoundDampening = 2f;

    public AudioClip[] clips;
    public LayerMask enemiesMask;
    public LayerMask soundObstacles;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(int i)
    {
        if(clips.Length > 0)
        {
            _audioSource.clip = clips[i];
            _audioSource.Play();
            NotifyAI();
        }
    }

    private void NotifyAI()
    {
        //TODO: Get distance based on volume somehow
        float radius = 20f;
        Collider[] inRange = Physics.OverlapSphere(transform.position, radius, enemiesMask);
        List<int> alerted = new List<int>();
        

        foreach (var collider in inRange)
        {
            float distance = Vector3.Distance(collider.transform.position, transform.position);
            RaycastHit[] hits;

            hits = Physics.RaycastAll(transform.position, collider.transform.position - transform.position, distance, soundObstacles);
            Debug.DrawRay(transform.position,  collider.transform.position - transform.position, Color.green, 5f);
            if (distance + hits.Length * obstacleSoundDampening < radius)
            {
                alerted.Add(collider.gameObject.GetInstanceID());
            }
        }
        
        //Set noise event with list of alerted entities
        GameEvents.current.OnNoisePlayed(alerted, transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        NotifyAI();
    }
}
