using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [HideInInspector] public List<GameObject> inventory = new List<GameObject>();


    public void Add(GameObject obj)
    {
        inventory.Add(obj);
    }

    public bool Contains(GameObject obj)
    {
        if (inventory.Contains(obj))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
