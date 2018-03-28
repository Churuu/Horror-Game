using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour 
{

	public GameObject particleSmoke;
	public bool generatorIsOn = false;

	public void EnableGenerator()
	{
		generatorIsOn = true;
		particleSmoke.SetActive(true);
	}
}


