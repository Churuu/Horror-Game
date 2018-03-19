﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour 
{
	public float standingStepCycle;
	public float crouchingStepCycle;

	PlayerController playerController;
	AudioSource src;
	float stepCycle = 0.5f;
	float stepCycleCounter;

	public AudioClip[] clips;

	void Start()
	{
		playerController = GetComponent<PlayerController>();
		src = GetComponent<AudioSource>();
		stepCycleCounter = Time.time + stepCycle;
	}

	void Update()
	{
		StepWalkingSound();

		if(playerController.crouching)
		{
			src.volume = 0.05f;
			stepCycle = crouchingStepCycle;
		}
		else
		{
			src.volume = 0.1f;
			stepCycle = standingStepCycle;
		}
	}

	void StepWalkingSound()
	{
		if(playerController.playerWalking && Time.time > stepCycleCounter)
		{
			stepCycleCounter = Time.time + stepCycle;
			int n = Random.Range(1, clips.Length);
			src.clip = clips[n];
			src.PlayOneShot(src.clip);
			clips[n] = clips[0];
			clips[0] = src.clip;
		}
	}
}