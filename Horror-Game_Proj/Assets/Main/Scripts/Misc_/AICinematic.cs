using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICinematic : MonoBehaviour
{

    public GameObject ai;
    public Transform spawnPoint;
    public Transform targetPoint;
    GameObject _ai;
    NavMeshAgent agent;


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
            StartCinematic();

    }

    void StartCinematic()
    {
        _ai = Instantiate(ai, spawnPoint.position, Quaternion.identity);
        agent = _ai.GetComponent<NavMeshAgent>();
        agent.SetDestination(targetPoint.position);
        FindObjectOfType<Ai>().agent.isStopped = true;
        Destroy(gameObject);
    }
}
