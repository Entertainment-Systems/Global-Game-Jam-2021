using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHearing : MonoBehaviour
{
    public float hearingRange = 20f;
    float obstacleSoundDampening = 2f;
    public LayerMask soundObstacles;

    private EnemyStates aiStates;
    // Start is called before the first frame update
    void Start()
    {
        aiStates = GetComponent<EnemyStates>();
        GameEvents.current.NoisePlayed += OnNoisePlayed;
    }

    private void OnNoisePlayed(float range, Vector3 pos)
    {

        float distance = Vector3.Distance(pos, transform.position);

        if (distance < hearingRange)
        {
            RaycastHit[] hits;

            hits = Physics.RaycastAll(transform.position, pos - transform.position, distance, soundObstacles);
            Debug.DrawRay(transform.position, pos - transform.position, Color.green, 5f);
            
            if (distance + hits.Length * obstacleSoundDampening < range)
            {
                aiStates.investigate(pos);
            }
        }
    }
}
