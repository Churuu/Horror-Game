using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CinematicAgent : MonoBehaviour {

	Animator _anim;
	NavMeshAgent agent;

	void Start()
	{
		_anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}

    // Update is called once per frame
    void Update()
    {
		_anim.SetBool("Walking", agent.velocity.magnitude > 0.1f ? true:false);

		if (!agent.pathPending)
		{
			if (agent.remainingDistance < 0.1f)
			{
				FindObjectOfType<Ai>().agent.isStopped = false;
				Destroy(gameObject);
			}
		}
    }
}
