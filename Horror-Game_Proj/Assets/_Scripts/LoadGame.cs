using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour {

	bool trigerred = false;
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			if(!trigerred)
			{
				BinarySaveSystem.Load();
				var player = GameObject.FindGameObjectWithTag("Player");
				var ai = GameObject.FindGameObjectWithTag("Ai");
				Debug.Log("System loaded");
				trigerred = true;
			}
		}
	}
}
