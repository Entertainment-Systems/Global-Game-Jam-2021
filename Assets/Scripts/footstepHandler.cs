using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class footstepHandler : MonoBehaviour
{
    [SerializeField] AudioSource indoorSource;
    [SerializeField] AudioSource outdoorSource;
    [SerializeField] LayerMask groundLayer;
    bool isPlayer, moving;
    Rigidbody controllerRB;
    NavMeshAgent agent;


    private void Start()
    {
        if (GetComponentInParent<NavMeshAgent>())
        {
            isPlayer = false;
            agent = GetComponentInParent<NavMeshAgent>();
        }
        else
        {
            isPlayer = true;
            controllerRB = GetComponentInParent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlayer)
        {
            if (controllerRB.velocity.magnitude > 0.1)
                moving = true;
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

        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, groundLayer);

        print(controllerRB.velocity.magnitude);

        switch (hit.collider.tag)
        {
            case "concrete":
                if (moving)
                {
                    outdoorSource.volume = 1;
                    indoorSource.volume = 0;
                }
                else
                {
                    outdoorSource.volume = 0;
                    indoorSource.volume = 0;
                }
                    break;

            case "wood":
                if (moving)
                {
                    outdoorSource.volume = 0;
                    indoorSource.volume = 1;
                }
                else
                {
                    outdoorSource.volume = 0;
                    indoorSource.volume = 0;
                }
                break;

            default:
                if (moving)
                {
                    outdoorSource.volume = 0;
                    indoorSource.volume = 1;
                }
                else
                {
                    outdoorSource.volume = 0;
                    indoorSource.volume = 0;
                }
                break;
        }

    }
}
