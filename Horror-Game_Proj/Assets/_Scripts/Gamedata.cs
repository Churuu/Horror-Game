using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedata : MonoBehaviour {

	public Vector3 playerPos;
	public Vector3 aiPos;
	public Ai.AiState aiState;
	public List<GameObject> _playerInv = new List<GameObject>();

	public Gamedata(Vector3 playerPos, Vector3 aiPos, Ai.AiState aiState, List<GameObject> _playerInv)
	{
		this.playerPos = playerPos;
		this.aiPos = aiPos;
		this.aiState = aiState;
		this._playerInv = _playerInv;

	}
}
