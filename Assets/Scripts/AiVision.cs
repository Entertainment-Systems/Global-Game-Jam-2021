using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiVision : MonoBehaviour
{
    public float visionRange = 20f;
    public float visionCone = 90f;
    public LayerMask collisionMask;

    private Vector3 _lastSightingPos;
    private Transform _player;
    private Transform eyes;
    private EnemyStates aiStates;
    
    // Start is called before the first frame update
    void Start()
    {
        aiStates = GetComponent<EnemyStates>();
        _lastSightingPos = transform.position;
        //Set the eyes to always be first cause lazy
        eyes = transform.GetChild(0);
        _player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Set the player height somehow?
        Vector3 target = _player.position + Vector3.up * _player.localScale.y;   

        if(aiStates.state != EnemyStates.enemyState.eating)
        {
            float angle = Vector3.Angle(transform.forward, target - eyes.position);
            if (angle < visionCone / 2f)
            {
                RaycastHit hit;
                if (!Physics.Raycast(eyes.position, target - eyes.position, out hit, visionRange, collisionMask))
                {
                    Debug.DrawRay(eyes.position, target - eyes.position, Color.red);
                    _lastSightingPos = _player.position;
                }

                if (_lastSightingPos == _player.position)
                {
                    aiStates.setTarget(_player);
                    aiStates.state = EnemyStates.enemyState.chase;
                }
                else
                {
                    if (aiStates.state == EnemyStates.enemyState.chase)
                    {
                        aiStates.investigate(_lastSightingPos);
                    }
                }
            }
        }
    }
}
