using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.AI;

/* This script controls the AI and how it will react in different situations */

 
public class Ai : MonoBehaviour
{

    // Script that controls the AI
    [HideInInspector] public List<Transform> aiPointToPatrol;
    public AiState state;
    [HideInInspector] public bool playerIsVisible = false;
    [Range(100, 180)] public int fieldOfViewAngle;
    [HideInInspector] public GameObject currentFloorToPatrol;
    [Range(1, 10)] public float chasingSpeed;
    [Range(1, 10)] public float walkingSpeed;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Vector3 PlayerLastSighting;
    [HideInInspector] public bool playerHiding = false;

    private GameObject player;
    private GameObject room;
    private Vector3 AiDestination;
    private int patrollingPoint = 0;
    private bool arrived = false;
    private bool patrollingRoom;
    private Animator _anim;
 


    public enum AiState { wait, patrolRoom, patrolKeypoint, chasePlayer };

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
    }

    void Update()
    {

        StartCoroutine(AI());
        CheckIfPlayerVisible();

        switch (state)
        {
            case AiState.wait:
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
    }

    IEnumerator AI()
    {
        if (playerIsVisible == true)
            state = AiState.chasePlayer;
        else
        {
            if(state == AiState.chasePlayer)
            {
                yield return new WaitForSeconds(10);
                agent.speed = walkingSpeed;
                state = AiState.patrolRoom;
            }
                
        }
    }

    void CheckIfPlayerVisible()
    {
        playerIsVisible = false;
        Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
        float maxViewDistance;
        float maxHearDistance;

        var playerController = FindObjectOfType<PlayerController>();
        if(playerController.flashlight || playerController.crouching)
            maxViewDistance = 10;
        else
            maxViewDistance = 5;


        if(Vector3.Angle(transform.forward, dirToTarget) < (fieldOfViewAngle / 2))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, maxViewDistance))
            {
                if(hit.collider.tag == "Player" && !playerHiding) 
                {
                    playerIsVisible = true;
                    PlayerLastSighting = player.transform.position;
                }
            }
        }

        if(playerController.crouching)
            maxHearDistance = 2f;
        else
            maxHearDistance = 5;

        if(Vector3.Distance(transform.position, player.transform.position) <= maxHearDistance && !playerHiding && playerController.playerWalking)
        {
            playerIsVisible = true;
            PlayerLastSighting = player.transform.position;
        }
    }

    void ChasePlayer()
    {
        CancelInvoke();
        agent.speed = chasingSpeed;
        agent.SetDestination(PlayerLastSighting);
    }

    public void PatrolRoom()
    {
        if (!patrollingRoom)
        {
            room = currentFloorToPatrol.transform.GetChild(UnityEngine.Random.Range(0, transform.childCount + 1)).gameObject;
            AiDestination = GetRandomPosInsideBox(room.transform.position, room.GetComponent<Collider>().bounds.size);
            if (!Physics.CheckSphere(AiDestination, 0.3f) && room.GetComponent<Room>().roomEnabled)
            {
                agent.SetDestination(AiDestination);
                patrollingRoom = true;
            }
        }

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= 0.5f)
                    Invoke("ResetRoomPatrol", 5);
            }
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
                arrived = true;
                if (arrived)
                {
                    Invoke("MoveToNextPoint", 5);
                    arrived = false;
                }
            }
        }
    }

    void MoveToNextPoint()
    {
        if (patrollingPoint == aiPointToPatrol.Count - 1)
        {
            state = AiState.wait;
            ResetPatrolPoints();
        }
        else
        {
            CancelInvoke("MoveToNextPoint");
            patrollingPoint++;
        }
    }

    void ResetPatrolPoints()
    {
        patrollingPoint = 0;
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
}