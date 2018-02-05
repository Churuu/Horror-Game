using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class BinarySaveSystem {

	public static float x,y,z;
	public static float rx,ry,rz;

	//ai info
	public static float _x,_y,_z;
	public static float _rx, _ry, _rz;

	public static void Save()
	{
		string destination = Application.persistentDataPath + "/save.dat";
		FileStream file;

		if(File.Exists(destination))
			file = File.OpenWrite(destination);
		else
			file = File.Create(destination);

		BinaryFormatter bf = new BinaryFormatter();
		Gamedata data = new Gamedata(x, y, z, rx, ry, rz, _x, _y, _z, _rx, _ry, _rz);
		bf.Serialize(file, data);
		file.Close();
	}


	public static void Load()
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
	}
}
