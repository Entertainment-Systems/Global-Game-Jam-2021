using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private RequireComponent AudioSource;
    private AudioSource _audioSource;

    [SerializeField] private float obstacleSoundDampening = 2f;

    public AudioClip[] clips;
    public LayerMask enemiesMask;
    
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
        _audioSource.clip = clips[i];
        _audioSource.Play();
        
        NotifyAI();
    }

    private void NotifyAI()
    {
        //TODO: Get distance based on volume somehow
        float radius = 20f;
        Collider[] inRange = Physics.OverlapSphere(transform.position, radius, enemiesMask);

        foreach (var collider in inRange)
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.position - collider.transform.position, radius);
            if(radius - hits.Length * obstacleSoundDampening > 0)
                Debug.Log("Notifying " + collider.gameObject.name + " of sound at " + transform.position);
        }
    }
}
