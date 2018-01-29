using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BinarySaveSystem : MonoBehaviour {

	Vector3 playerPos;
	Vector3 aiPos;
	Ai.AiState aiState;
	List<GameObject> _playerInv = new List<GameObject>();

	
	public void Save()
	{
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if(File.Exists(destination))
			file = File.OpenWrite(destination);
		else
			file = File.Create(destination);

		Gamedata data = new Gamedata(playerPos, aiPos, aiState, _playerInv);
		BinaryFormatter bf = new BinaryFormatter();
		bf.Serialize(file, data);
		file.Close();
	}


	public void Load()
	{
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if(File.Exists(destination))
			file = File.OpenRead(destination);
		else
			return;

		BinaryFormatter bf =  new BinaryFormatter();
		Gamedata data = (Gamedata) bf.Deserialize(file);
		file.Close();

		playerPos = data.playerPos;
		aiPos = data.playerPos;
		aiState = data.aiState;
		_playerInv = data._playerInv;
	}
}
