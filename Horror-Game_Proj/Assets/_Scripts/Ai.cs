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
    [HideInInspector]
    public List<Transform> aiPointToPatrol;
    public AiState state;
    [HideInInspector]
    public bool playerIsVisible = false;
    [Range(100, 180)]
    public int fieldOfViewAngle;
    [HideInInspector]
    public GameObject currentFloorToPatrol;
    [Range(1, 10)]
    public float chasingSpeed;
    [Range(1, 10)]
    public float walkingSpeed;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Vector3 PlayerLastSighting;

    private GameObject player;
    private GameObject room;
    private Vector3 AiDestination;
    private int patrollingPoint = 0;
    private bool arrived = false;
    private bool patrollingRoom;


    public enum AiState { wait, patrolRoom, patrolKeypoint, chasePlayer };

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
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
        float maxViewDistance;
        Vector3 dirToTarget = (player.transform.position - transform.position).normalized;


        var pC = FindObjectOfType<PlayerController>();
        if(pC.flashlight || pC.crouching)
            maxViewDistance = 10;
        else
            maxViewDistance = 5;


        if(Vector3.Angle(transform.forward, dirToTarget) < (fieldOfViewAngle / 2))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, maxViewDistance))
            {
                if(hit.collider.tag == "Player") 
                {
                    playerIsVisible = true;
                    PlayerLastSighting = player.transform.position;
                }
            }
        }

        if(Vector3.Distance(transform.position, player.transform.position) <= 5)
            playerIsVisible = true;

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
            if (!Physics.CheckSphere(AiDestination, 0.5f) && agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(AiDestination);
                patrollingRoom = true;
            }else{
                Debug.LogWarning("The AI had trouble reaching its destination, make sure it has a clear path to the target");
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