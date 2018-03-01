using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public GameObject keyToOpen;
	public bool useAnimation;
	public GameObject[] rooms;

	public void UnlockRooms()
	{
		if(rooms.Length > 0)
		{
			foreach(var room in rooms)
			{
				room.GetComponent<Room>().roomEnabled = true;
			}
		}
	}
}
