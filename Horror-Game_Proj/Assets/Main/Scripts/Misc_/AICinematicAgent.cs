using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICinematicAgent : MonoBehaviour
{
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
    }
}
