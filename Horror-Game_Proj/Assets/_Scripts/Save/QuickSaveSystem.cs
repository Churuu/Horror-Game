using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSaveSystem : MonoBehaviour 
{
	
	public GameObject player;
	public GameObject ai;
	PlayerInventory _inv;

	Vector3 playerPos, aiPos;
	Vector3 playerRot, aiRot;
	List <GameObject> inventory = new List<GameObject>();

	void Start() 
	{
		_inv = player.GetComponent<PlayerInventory>();
		DontDestroyOnLoad(this);
		Save();
	}

	public void Save()
	{
		playerPos = player.transform.position;
		playerRot = player.transform.eulerAngles;
		aiPos = ai.transform.position;
		aiRot = ai.transform.eulerAngles;
		inventory = _inv.playerInventory;
		print(playerRot);
	}

	public void Load()
	{
		player.transform.position = playerPos;
		player.transform.eulerAngles = new Vector3(0, playerRot.y, 0);
		ai.transform.position = aiPos;
		ai.transform.eulerAngles = new Vector3(aiRot.x, aiRot.y, aiRot.z);
		_inv.playerInventory = inventory;
		var _ai = FindObjectOfType<Ai>();
		_ai.PlayerLastSighting = aiPos;
		_ai.agent.ResetPath();
		_ai.state = Ai.AiState.patrolRoom;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F9))
			Load();
	}

}
