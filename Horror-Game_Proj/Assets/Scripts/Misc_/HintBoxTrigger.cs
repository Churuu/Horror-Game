using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintBoxTrigger : MonoBehaviour {

	public string message;


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			FindObjectOfType<HintBox>().Hint(message);
			Destroy(this);
		}
	}
}
