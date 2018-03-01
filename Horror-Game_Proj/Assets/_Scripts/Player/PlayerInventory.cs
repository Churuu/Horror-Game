using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

	[HideInInspector] public List<GameObject> playerInventory = new List<GameObject>();
	public GameObject interaction;
	public Text interactionText;

	void Update()
	{
        PickObject();
        OpenDoor();
        ShowInformationOnScreenIneractions();
        Cursor.lockState = CursorLockMode.Locked;
	}


    private void PickObject() 
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
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
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
        {
            if(hit.collider.tag == "Door")
            {
                if(Input.GetButtonDown("Interaction"))
                {
                    var _door = hit.collider;
					if(playerInventory.Contains(_door.GetComponent<Door>().keyToOpen))
                    {
                        if(_door.GetComponent<Door>().useAnimation)
                            _door.gameObject.GetComponent<Animation>().Play();
                        else
                            _door.gameObject.SetActive(false);

                        _door.GetComponent<Door>().UnlockRooms();
                    }      
                }
            }
        }
    }


    	private void ShowInformationOnScreenIneractions()
    {
		interaction.SetActive(false);
		interactionText.text = "";

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2f))
        {
            if(hit.collider.tag == "Door")
            {
				interaction.SetActive(true);
				interactionText.text = "Door";
            }
			if(hit.collider.tag == "ObjectiveItem")
			{
				interaction.SetActive(true);
				interactionText.text = hit.collider.name;
			}
        }
    }
}
