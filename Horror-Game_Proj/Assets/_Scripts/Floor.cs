using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
			FindObjectOfType<Ai>().currentFloorToPatrol = this.gameObject;
	}
}
