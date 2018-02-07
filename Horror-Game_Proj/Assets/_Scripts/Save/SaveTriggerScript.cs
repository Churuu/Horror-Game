using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTriggerScript : MonoBehaviour {

	bool triggered = false;
	void OnTriggerEnter(Collider other)
	{
		if(!triggered)
		{
			if(other.gameObject.CompareTag("Player"))
			{
				FindObjectOfType<QuickSaveSystem>().Save();
				triggered = true;
			}
		}
	}
}
