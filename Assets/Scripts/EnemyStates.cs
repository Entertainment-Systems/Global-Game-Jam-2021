using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{

    [Header("State")]
    public enemyState state;
    public enum enemyState { patrol, chase, wait }
    [SerializeField] float patrolSpeed = 0.4f;
    [SerializeField] float chaseSpeed = 0.8f;

    [Header("Targets")]
    [SerializeField] GameObject waypointsParent;
    [SerializeField] float pauseTime = 1;

    GameObject Player;

    Transform[] waypointTransforms;
    Transform target;
    int currentTargetIndex;

    Animator anim;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

        int waypointCount = waypointsParent.transform.childCount;
        print("waypoints: " + waypointCount);

        waypointTransforms = new Transform[waypointCount];

        if (waypointsParent.transform.childCount > 0)
        {
            for (int i = 0; i < waypointsParent.transform.childCount; i++)
                waypointTransforms[i] = waypointsParent.transform.GetChild(i);

            target = waypointTransforms[0];
            currentTargetIndex = 0;
            agent.SetDestination(target.position);
            StartCoroutine(changeAnimation("Walk"));
        }
        else print("Error: no waypoints set for AI");
    }

    // Update is called once per frame
    void Update()
    {
        anim.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        switch (state)
        {
            case enemyState.patrol:
                if (Vector3.Distance(transform.position, target.position) < 1)
                    StartCoroutine(NextWaypoint());

                agent.SetDestination(target.position);
                agent.speed = patrolSpeed;
                break;

            case enemyState.chase:
                agent.SetDestination(Player.transform.position);
                agent.speed = chaseSpeed;
                break;

            case enemyState.wait:
                agent.speed = 0;
                break;
        }
    }

    IEnumerator NextWaypoint()
    {
        state = enemyState.wait;
        agent.isStopped = true;

        StartCoroutine(changeAnimation("Idle"));

        yield return new WaitForSecondsRealtime(pauseTime);

        agent.isStopped = false;

        if (currentTargetIndex == waypointTransforms.Length-1) {
            target = waypointTransforms[0];
            currentTargetIndex = 0;
        }
        else {
            currentTargetIndex++;
            target = waypointTransforms[currentTargetIndex];
        }

        StartCoroutine(changeAnimation("Walk"));

        state = enemyState.patrol;
    }

    IEnumerator changeAnimation(string animationName)
    {
        anim.SetBool(animationName, true);
        yield return new WaitForEndOfFrame();
        anim.SetBool(animationName, false);
    }
}
