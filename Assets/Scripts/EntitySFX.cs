using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;


[RequireComponent(typeof(AudioPlayer))]
[RequireComponent(typeof(EnemyStates))]
public class EntitySFX : MonoBehaviour
{
    public float maxFrequency = 5f;

    public float minFrequency = 15f;

    private AudioPlayer _audioPlayer;
    private EnemyStates _enemyStates;
    private float timer, nextAudioTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        nextAudioTime = Random.Range(minFrequency, maxFrequency);
        _audioPlayer = GetComponent<AudioPlayer>();
        _enemyStates = GetComponent<EnemyStates>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_enemyStates.state != EnemyStates.enemyState.chase && _enemyStates.state != EnemyStates.enemyState.eating)
        {
            timer += Time.deltaTime;
            if (timer > nextAudioTime)
            {
                timer = 0;
                nextAudioTime = Random.Range(minFrequency, maxFrequency);

                //Choose random non screech
                int clip = Random.Range(15, 17);
                float pitch = Random.Range(.9f, 1.1f);
                float vol = Random.Range(.9f, 1.1f);
                _audioPlayer.Play(clip, vol, pitch);
            }
        }
    }
}
