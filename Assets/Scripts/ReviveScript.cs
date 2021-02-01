using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class ReviveScript : MonoBehaviour
{
    [SerializeField] private Transform teleportPoint;
    [SerializeField] public float teleportTime = .8f;

    private AudioPlayer _audioPlayer;
    private IEnumerator coroutine;

    void Start()
    {
        _audioPlayer = GetComponent<AudioPlayer>();
        GameEvents.current.PlayerLostLife += TeleportPlayer;
    }

   void TeleportPlayer(int liveLeft)
    {
        coroutine = movePlayer(teleportTime);
        StartCoroutine(coroutine);
    }

    IEnumerator movePlayer(float waitTime)
    {
        _audioPlayer.Play(14);
        yield return new WaitForSeconds(waitTime);
        transform.position = teleportPoint.position;
    }
}