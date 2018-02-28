using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUnderTable : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player" && FindObjectOfType<Ai>().state != Ai.AiState.chasePlayer)
		{
			FindObjectOfType<Ai>().playerHiding = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			FindObjectOfType<Ai>().playerHiding = false;
		}
	}
}
