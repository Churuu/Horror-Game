using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider c) 
	{
		if(c.gameObject.tag == "Player")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}
	}
}
