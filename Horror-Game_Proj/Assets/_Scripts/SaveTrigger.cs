using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour {


	bool trigerred = false;
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(!trigerred)
			{
				BinarySaveSystem.Save();
				Debug.Log("System saved");
				trigerred = true;
			}
		}
	}
}
