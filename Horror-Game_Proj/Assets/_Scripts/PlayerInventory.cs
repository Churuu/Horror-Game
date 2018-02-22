using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    [HideInInspector]
	public List<GameObject> playerInventory = new List<GameObject>();

	void Update()
	{
        PickObject();
        OpenDoor();
	}


    private void PickObject() 
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2))
        {
            if(hit.collider.tag == "ObjectiveItem")
            {
                if(Input.GetButtonDown("Interaction"))
                {
					playerInventory.Add (hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
    private void OpenDoor() 
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2))
        {
            if(hit.collider.tag == "Door")
            {
                if(Input.GetButtonDown("Interaction"))
                {
					if(playerInventory.Contains(hit.collider.GetComponent<Door>().keyToOpen))
                    {
                        hit.collider.gameObject.SetActive(false);
                    }      
                }
            }
        }
    }
}
