using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICinematic : MonoBehaviour
{

    public GameObject ai;
    public Vector3 spawnPoint;
    public Vector3 targetPoint;
    GameObject _ai;
    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        _ai = Instantiate(ai, spawnPoint, Quaternion.identity);
        agent = _ai.GetComponent<NavMeshAgent>();
		agent.SetDestination(targetPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending)
            if (agent.remainingDistance <= 0.1f)
            {
	        	Destroy(_ai, 1);
				Destroy(this);
            }
    }
}
