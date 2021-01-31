using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{

    [Header("States")] private enemyState _state;
    public enemyState state
    {
        get => _state;
        set
        {
            //Check if coroutine empty or running
            if(pauseCoroutine != null)
            {
                StopCoroutine(pauseCoroutine);
                pauseCoroutine = null;
            }
            // Debug.Log(name + " going to " + value);
            _state = value;
            switch (value)
            {
                case enemyState.chase:
                    agent.speed = chaseSpeed;
                    if(screechTimer <= 0)
                    {
                        _audioPlayer.Play();
                        screechTimer = 30f;
                    }
                    break;
                case enemyState.wait:
                    agent.speed = 0;
                    break;
                case enemyState.eating:
                    agent.speed = 0;
                    break;
                default:
                    agent.speed = patrolSpeed;
                    break;
            }            
        }
    }

    public enum enemyState { patrol, chase, investigate, wait, eating }
    [SerializeField] float patrolSpeed = 1f;    
    [SerializeField] private float chaseSpeed = 3f;

    [Header("Targets")]
    [SerializeField] GameObject waypointsParent;
    [SerializeField] float pauseTime = 1;
    [SerializeField] float screechCooldown = 30f;

    private float screechTimer = 0;

    GameObject Player;

    Transform[] waypointTransforms;
    Transform target;
    int currentTargetIndex;

    Animator anim;
    NavMeshAgent agent;
    private AudioPlayer _audioPlayer;

    // private IEnumerator pauseCoroutine;
    private Coroutine pauseCoroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        _audioPlayer = GetComponent<AudioPlayer>();

        GameEvents.current.PlayerKilled += OnPlayerKilled;

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
            // StartCoroutine(changeAnimation("Walk"));
        }
        else print("Error: no waypoints set for AI");

        state = enemyState.patrol;
    }

    private void OnPlayerKilled(int id)
    {
        if(gameObject.GetInstanceID() == id)
        {
            anim.SetBool("Eat", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (screechTimer > 0)
            screechTimer -= Time.deltaTime;
        
        anim.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        switch (state)
        {
            case enemyState.patrol:
                if (Vector3.Distance(transform.position, target.position) < agent.stoppingDistance)
                {
                    //Check if coroutine empty or running
                    if(pauseCoroutine == null)
                    {
                        pauseCoroutine = StartCoroutine(NextWaypoint());
                    }
                }                
                break;

            case enemyState.chase:
                setTarget(Player.transform, chaseSpeed);
                break;

            case enemyState.investigate:
                // print(Vector3.Distance(transform.position, target.position));
                agent.speed = patrolSpeed;

                if (Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance)
                {
                    //Check if coroutine empty or running
                    if(pauseCoroutine == null)
                    {
                        pauseCoroutine = StartCoroutine(NextWaypoint());
                    }
                }
                break;


            case enemyState.wait:
                agent.speed = 0;
                break;
        }
        anim.SetFloat("Speed", agent.desiredVelocity.magnitude, .2f, Time.deltaTime);
    }

    void setTarget(Transform t, float speed)
    {
        target = t;
        agent.SetDestination(target.position);
        agent.speed = speed;
    }
    
    public void setTarget(Transform t)
    {
        target = t;
        agent.SetDestination(target.position);
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
        state = enemyState.investigate;
        agent.SetDestination(pos);
    }

    IEnumerator NextWaypoint()
    {
        state = enemyState.wait;
        // agent.isStopped = true;

        // StartCoroutine(changeAnimation("Idle"));

        yield return new WaitForSecondsRealtime(pauseTime);

        // agent.isStopped = false;

        if (currentTargetIndex == waypointTransforms.Length-1) {
            target = waypointTransforms[0];
            currentTargetIndex = 0;
        }
        else {
            currentTargetIndex++;
            target = waypointTransforms[currentTargetIndex];
        }

        agent.SetDestination(target.position);
        agent.speed = patrolSpeed;
        state = enemyState.patrol;
    }

    IEnumerator changeAnimation(string animationName)
    {
        anim.SetBool(animationName, true);
        yield return new WaitForEndOfFrame();
        anim.SetBool(animationName, false);
    }

    //Dirty! TODO: Move to its own script?
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.current.OnPlayerAttacked(gameObject.GetInstanceID());
        }
    }
}
