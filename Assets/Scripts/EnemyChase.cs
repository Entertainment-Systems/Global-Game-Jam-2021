using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] float chaseSpeed = 3f;
    float screechTimer = 0;
    Transform target;
    AudioPlayer _audioPlayer;
    NavMeshAgent agent;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _audioPlayer = GetComponent<AudioPlayer>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        GameEvents.current.PlayerKilled += OnPlayerKilled;
    }

    // Update is called once per frame
    void Update()
    {
        anim.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

        agent.SetDestination(target.position);
        agent.speed = chaseSpeed;

        if (screechTimer <= 0)
        {
            _audioPlayer.Play();
            screechTimer = 30f;
        }
        else
        {
            screechTimer -= Time.deltaTime;
        }

        anim.SetFloat("Speed", agent.desiredVelocity.magnitude, .2f, Time.deltaTime);
    }

    private void OnPlayerKilled(int id)
    {
        if (gameObject.GetInstanceID() == id)
        {
            anim.SetBool("Eat", true);
        }
    }
}
