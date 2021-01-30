using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ReviveScript : MonoBehaviour
{
    [SerializeField] private Transform teleportPoint;
    [SerializeField] public float teleportTime = .8f;


    private IEnumerator coroutine;

    void Start()
    {

        GameEvents.current.PlayerLostLife += TeleportPlayer;
    }

   void TeleportPlayer(int liveLeft)
    {
        coroutine = movePlayer(teleportTime);
        StartCoroutine(coroutine);
    }

    IEnumerator movePlayer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = teleportPoint.position;
    }
}