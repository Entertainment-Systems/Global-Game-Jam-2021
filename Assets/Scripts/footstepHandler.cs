using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioPlayer))]
public class footstepHandler : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    public float stepFrequency = .5f;
    public int stepClips = 7;
    
    private AudioPlayer _audioPlayer;
    private FirstPersonAIO characterController;
    bool isPlayer, moving, isRunning, isSneaking;
    Rigidbody controllerRB;
    NavMeshAgent agent;

    private int stepIndexOutside = 0;
    private int stepIndexInside = 0;
    private float timer = 0;
    private void Start()
    {
        _audioPlayer = GetComponent<AudioPlayer>();
        if (GetComponentInParent<NavMeshAgent>())
        {
            isPlayer = false;
            agent = GetComponent<NavMeshAgent>();
        }
        else
        {
            isPlayer = true;
            controllerRB = GetComponent<Rigidbody>();
            characterController = GetComponent<FirstPersonAIO>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float vol = 1f;
        float range = 1f;
        float frequency = stepFrequency;
        
        if (isPlayer)
        {
            if (controllerRB.velocity.magnitude > 0.1)
            {
                moving = true;
                if (characterController.isCrouching)
                {
                    vol = .7f;
                    range = .5f;
                    frequency *= 2f;
                }
                else if (characterController.isSprinting)
                {
                    vol = 1.3f;
                    range = 2f;
                    frequency /= 2f;
                }
            }
            else
                moving = false;
        }
        else
        {
            if (agent.velocity.magnitude > 0)
                moving = true;
            else
                moving = false;
        }


        timer += Time.deltaTime;
        if(timer > frequency)
        {
            timer = 0f;
            RaycastHit hit;
            Physics.Raycast(transform.position + (Vector3.up * .2f), Vector3.down, out hit);

            switch (hit.collider.tag)
            {
                case "concrete":
                    if (moving)
                    {
                        _audioPlayer.Play(stepIndexOutside++ % (stepClips - 1), vol, 1f, range);
                    }

                    break;

                case "wood":
                    if (moving)
                    {
                        _audioPlayer.Play(stepIndexInside++ % (stepClips - 1) + stepClips, vol, 1f, range);
                    }

                    break;

                default:
                    if (moving)
                    {
                        _audioPlayer.Play(stepIndexInside++ % (stepClips - 1) + stepClips);
                    }

                    break;
            }
        }

    }
}
