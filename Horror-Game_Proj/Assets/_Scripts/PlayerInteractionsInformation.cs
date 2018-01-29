using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionsInformation : MonoBehaviour {
	
	public GameObject interaction;
	public Text interactionText;

	// Update is called once per frame
	void Update () 
	{
		ShowInformationOnScreenIneractions();
	}

	private void ShowInformationOnScreenIneractions()
    {
		interaction.SetActive(false);

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 2))
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
