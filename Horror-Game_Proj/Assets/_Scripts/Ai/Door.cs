using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public GameObject keyToOpen;

	public void UnlockRooms()
	{
        GetComponent<Animator>().SetBool("DoorIsOpen", true);
	}
}
