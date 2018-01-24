using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public List<GameObject> inventory = new List<GameObject>();
	public PlayerInventory(GameObject _item) 
	{
		inventory.Add(_item);
	}

	public bool CheckInventory(GameObject key) 
	{
		if(inventory.Contains(key))
			return true;
		else
			return false;
	} 
}
