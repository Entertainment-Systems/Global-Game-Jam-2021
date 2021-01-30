using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{

    [Header("States")]
    public enemyState state;
    public enum enemyState { patrol, chase, investigate, wait }
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
        GameEvents.current.NoisePlayed += OnNoisePlayed;
        
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

    private void OnNoisePlayed(List<int> list, Vector3 pos)
    {
        if(list.Contains(gameObject.GetInstanceID()))
            investigate(pos);
    }

    // Update is called once per frame
    void Update()
    {
        anim.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        switch (state)
        {
            case enemyState.patrol:
                if (Vector3.Distance(transform.position, target.position) < agent.stoppingDistance)
                    StartCoroutine(NextWaypoint());

                agent.SetDestination(target.position);
                agent.speed = patrolSpeed;
                break;

            case enemyState.chase:
                setTarget(Player.transform, chaseSpeed);
                break;

            case enemyState.investigate:
                print(Vector3.Distance(transform.position, target.position));
                if (Vector3.Distance(transform.position, target.position) < agent.stoppingDistance)
                    StartCoroutine(NextWaypoint());
                break;


            case enemyState.wait:
                agent.speed = 0;
                break;
        }
    }

    void setTarget(Transform t, float speed)
    {
        target = t;
        agent.SetDestination(target.position);
        agent.speed = speed;
    }

    public void testInvestigate()
    {
        investigate(Player.transform);
    }

    public void investigate(Transform t)
    {
        setTarget(t, chaseSpeed);
        state = enemyState.investigate;
    }
    
    //Need an investigate at position, since transform will always point to the object even when it moved after making noise
    public void investigate(Vector3 pos)
    {
        Debug.Log(name + " alerted of noise at " + pos);
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
