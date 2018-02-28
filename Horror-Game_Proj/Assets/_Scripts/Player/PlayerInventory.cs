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
					if(playerInventory.Contains(hit.collider.GetComponent<Door>().keyToOpen))
                    {
                        if(hit.collider.GetComponent<Door>().useAnimation)
                            hit.collider.gameObject.GetComponent<Animation>().Play();
                        else
                            hit.collider.gameObject.SetActive(false);
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
