﻿using System.Collections;
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
    public AudioClip[] laugh;
    public GameObject fade;
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
    private Animator _anim;
    private GameObject[] roomHolder;



    public enum AiState { wait, patrolRoom, patrolKeypoint, chasePlayer };

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        roomHolder = GameObject.FindGameObjectsWithTag("Room");
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckIfPlayerVisible();
        AnimationHandler();
        StateHandler();
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
    }

    void AnimationHandler()
    {

        _anim.SetBool("Walking", state == AiState.patrolRoom && patrollingRoom && agent.velocity.magnitude > 0.1 || state == AiState.patrolKeypoint && !patrollingKeyRoom && agent.velocity.magnitude > 0.1 ? true : false);
        _anim.SetBool("Running", playerIsVisible && agent.velocity.magnitude > 0.1 || state == AiState.chasePlayer && agent.velocity.magnitude > 0.1 ? true : false);
        _anim.SetBool("Idle", agent.velocity.magnitude == 0 ? true : false);

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
                Invoke("ReturnToRoomPatrol", 1);




    }

    public void PatrolRoom()
    {
        if (!patrollingRoom)
        {
            room = roomHolder[UnityEngine.Random.Range(0, roomHolder.Length)];
            AiDestination = GetRandomPosInsideBox(room.transform.position, room.GetComponent<Collider>().bounds.size);
            if (!Physics.CheckSphere(AiDestination, 0.3f) && room.GetComponent<Room>().roomEnabled)
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
        float alpha = ((distance / 2) *-1);
        alpha = Mathf.Clamp01(alpha);
        fade.GetComponent<image
        print(alpha);
    }


}