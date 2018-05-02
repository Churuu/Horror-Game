using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
/* This script controls the AI and how it will react in different situations */


public class Ai : MonoBehaviour
{

    // Script that controls the AI
    public AiState state;
    [Range(1, 10)] public float chasingSpeed;
    [Range(1, 10)] public float walkingSpeed;
    [Range(100, 180)] public int fieldOfViewAngle;
    public Image fade;
    public AudioClip detectionSound;
    public AudioClip[] sounds; // 0 Walking, 1 Scary angry sound
    public GameObject[] rooms;
    [HideInInspector] public List<Transform> aiPointToPatrol;
    [HideInInspector] public bool playerIsVisible = false;
    [HideInInspector] public GameObject currentFloorToPatrol;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Vector3 PlayerLastSighting;
    [HideInInspector] public bool playerHiding = false;


    private GameObject player;
    private GameObject room;
    private Vector3 AiDestination;
    private int patrollingPoint = 0;
    private bool patrollingKeyRoom = false;
    private bool patrollingRoom;
    private bool firstDetection;
    private Animator _anim;
    private float stepCycle = .7f;
    private float stepCycleCounter;
    private float soundCycleCounter;
    private AudioSource src;
    private Vector3 playerStartingPos;



    public enum AiState { wait, patrolRoom, patrolKeypoint, chasePlayer };

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //rooms = GameObject.FindGameObjectsWithTag("Room");
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        stepCycleCounter = Time.time + stepCycle;
        soundCycleCounter = Time.time + UnityEngine.Random.Range(30, 300);
        src = GetComponent<AudioSource>();
        playerStartingPos = player.transform.position;

    }

    void Update()
    {
        CheckIfPlayerVisible();
        AnimationHandler();
        StateHandler();
        Kill();
        StepSound();
    }

    void StateHandler()
    {
        switch (state)
        {
            case AiState.wait:
                // Gör faktiskt inget men praktiskt ibland att ha.
                break;
            case AiState.patrolRoom:
                PatrolRoom();
                break;
            case AiState.patrolKeypoint:
                PatrolKeyPoint(aiPointToPatrol);
                break;
            case AiState.chasePlayer:
                ChasePlayer();
                break;
        }

        PlaySounds();
    }

    void AnimationHandler()
    {
        _anim.SetFloat("Blend", agent.desiredVelocity.magnitude);
    }


    void CheckIfPlayerVisible()
    {
        playerIsVisible = false;
        Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
        float maxViewDistance;
        float maxHearDistance;

        var playerController = FindObjectOfType<PlayerController>();
        if (playerController.flashlight || playerController.crouching)
            maxViewDistance = 10;
        else
            maxViewDistance = 5;


        if (Vector3.Angle(transform.forward, dirToTarget) < (fieldOfViewAngle / 2))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, maxViewDistance))
            {
                if (hit.collider.tag == "Player" && !playerHiding)
                {
                    playerIsVisible = true;
                    PlayerLastSighting = player.transform.position;
                    state = AiState.chasePlayer;

                    if(firstDetection)
                    {
                        firstDetection = false;
                        src.PlayOneShot(detectionSound);
                    }
                }
            }
        }

        if (playerController.crouching)
            maxHearDistance = 2f;
        else
            maxHearDistance = 5;

        if (Vector3.Distance(transform.position, player.transform.position) <= maxHearDistance && !playerHiding && playerController.playerWalking)
        {
            playerIsVisible = true;
            PlayerLastSighting = player.transform.position;
            state = AiState.chasePlayer;

        }
    }

    void ChasePlayer()
    {
        agent.speed = chasingSpeed;
        agent.SetDestination(PlayerLastSighting);

        if (!agent.pathPending)
            if (agent.remainingDistance <= 0.1f)
            {
                firstDetection = true;
                Invoke("ReturnToRoomPatrol", 1);
            }
    }

    public void PatrolRoom()
    {
        if (!patrollingRoom)
        {
            if (rooms.Length == 0)
            {
                Debug.LogError("AI'n har inga rum att gå till, skapa ett rum för monstret att gå till! Kom ihåg att du måste dra in de rum den ska gå till också på monstret");
                return;
            }
            room = rooms[UnityEngine.Random.Range(0, rooms.Length)];
            AiDestination = GetRandomPosInsideBox(room.transform.position, room.GetComponent<Collider>().bounds.size);
            if (!Physics.CheckSphere(AiDestination, 0.3f))
            {
                agent.SetDestination(AiDestination);
                agent.speed = walkingSpeed;
                patrollingRoom = true;
            }
        }

        if (!agent.pathPending)
            if (agent.remainingDistance <= 0.1f)
                Invoke("ResetRoomPatrol", 5);
    }

    void ResetRoomPatrol()
    {
        CancelInvoke("ResetRoomPatrol");
        patrollingRoom = false;
    }


    public void PatrolKeyPoint(List<Transform> points)
    {
        agent.SetDestination(points[patrollingPoint].position);
        if (!agent.pathPending)
        {
            if (agent.remainingDistance < 0.05f)
            {
                patrollingKeyRoom = false;
                if (!patrollingKeyRoom)
                {
                    Invoke("MoveToNextPoint", 5);
                    patrollingKeyRoom = true;
                }
            }
        }
    }

    void MoveToNextPoint()
    {
        if (patrollingPoint == aiPointToPatrol.Count - 1)
        {
            ReturnToRoomPatrol();
        }
        else
        {
            CancelInvoke("MoveToNextPoint");
            patrollingPoint++;
        }
    }

    void ReturnToRoomPatrol()
    {
        patrollingPoint = 0;
        agent.speed = walkingSpeed;
        state = AiState.patrolRoom;
    }


    public void switchAiPatrolPoint(List<Transform> aiPointToPatrol)
    {
        if (state != AiState.chasePlayer)
        {
            System.Random rnd = new System.Random();
            List<Transform> ScrambledPatrolPoints = aiPointToPatrol.OrderBy(x => rnd.Next()).ToList();
            this.aiPointToPatrol = ScrambledPatrolPoints;
            patrollingPoint = 0;
            state = AiState.patrolKeypoint;
        }
    }

    Vector3 GetRandomPosInsideBox(Vector3 center, Vector3 size)
    {
        Vector3 rndP = new Vector3(
            size.x * (UnityEngine.Random.value - .5f),
            0,
            size.z * (UnityEngine.Random.value - .5f));
        return center + rndP;

    }

    void Kill()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        float target = Mathf.Clamp01(distance);
        target = target / distance;
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, target);
        if (target >= 0.9f)
            player.transform.position = playerStartingPos;
    }


    void StepSound()
    {
        if (Time.time > stepCycleCounter && agent.velocity.magnitude > 0)
        {
            stepCycleCounter = Time.time + stepCycle;
            src.PlayOneShot(sounds[0], 1);
        }
    }

    void PlaySounds()
    {
        if (Time.time > soundCycleCounter)
        {
            soundCycleCounter = Time.time + UnityEngine.Random.Range(30, 300);
            src.PlayOneShot(sounds[UnityEngine.Random.Range(1, 2)], 1);
        }
    }
}