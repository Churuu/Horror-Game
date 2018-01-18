﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{

    // Script that controls the AI
    [HideInInspector]
    public Transform[] aiPointToPatrol;
    public GameObject player;
    public AiState state;
    [Range(100, 180)]
    public int fieldOfViewAngle;
    public bool playerIsVisible = false;
    [HideInInspector]
    public GameObject currentFloorToPatrol;

    private NavMeshAgent agent;
    private GameObject[] rooms;
    private GameObject room;
    private Vector3 PlayerLastSighting;
    private Vector3 AiDestination;
    private int patrollingPoint = 0;
    private bool arrived = false;
    private bool patrollingRoom;
    private GameObject test;

    public enum AiState { wait, patrolRoom, patrolKeypoint, chasePlayer };

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rooms = GameObject.FindGameObjectsWithTag("Room");
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
                state = AiState.patrolRoom;
            }
                
        }
    }

    void CheckIfPlayerVisible()
    {
        playerIsVisible = false;
        Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
        if(Vector3.Angle(transform.forward, dirToTarget) < (fieldOfViewAngle / 2))
        {
            RaycastHit hit; 
            if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit))
            {
                if(hit.collider.tag == "Player") 
                {
                    playerIsVisible = true;
                    PlayerLastSighting = player.transform.position;
                } 
            }
        }
    }

    void ChasePlayer()
    {
        CancelInvoke();
        agent.SetDestination(PlayerLastSighting);
    }

    public void PatrolRoom()
    {
        if (!patrollingRoom)
        {
            room = rooms[UnityEngine.Random.Range(0, rooms.Length)];
            if(currentFloorToPatrol.GetComponent<Collider>().bounds.Contains(room.transform.position))
            {
                AiDestination = GetRandomPosInsideBox(room.transform.position, room.GetComponent<Collider>().bounds.size);
                if (!Physics.CheckSphere(AiDestination, 0.5f))
                {
                    agent.SetDestination(AiDestination);
                    patrollingRoom = true;
                }
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


    public void PatrolKeyPoint(Transform[] points)
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
        if (patrollingPoint == aiPointToPatrol.Length - 1)
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


    public void switchAiPatrolPoint(Transform[] aiPointToPatrol)
    {
        if (state != AiState.chasePlayer)
        {
            System.Random rnd = new System.Random();
            Transform[] ScrambledPatrolPoints = aiPointToPatrol.OrderBy(x => rnd.Next()).ToArray();

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

    void AISound() 
    {
        if(Input.GetKey(KeyCode.LeftShift))
            PlayerLastSighting = player.transform.position;
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(player.GetComponent<Rigidbody>().velocity.x > 0 && player.GetComponent<Rigidbody>().velocity.z > 0)
                PlayerLastSighting = player.transform.position;
        }
    }
}