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

    void Start()
    {
        agent = _ai.GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
            StartCinematic();

    }

    void StartCinematic()
    {
        _ai = Instantiate(ai, spawnPoint, Quaternion.identity);
		agent.SetDestination(targetPoint);
        InvokeRepeating("CheckArrival", 0, Time.deltaTime);
    }
    
    void CheckArrival()
    {
        if (!agent.pathPending)
            if (agent.remainingDistance <= 0.1f)
            {
	        	Destroy(_ai, 1);
				Destroy(this);
            }
    }
}
