using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInventory))]
public class Interact : MonoBehaviour
{

    public GameObject interactionImage;
    public Text interactionText;
    public LayerMask interactable;
    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        ShowInformationOnScreen();
        InteractWithObj();
    }

    private void ShowInformationOnScreen()
    {
        interactionImage.SetActive(false);
        interactionText.text = "";

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f, interactable))
        {
			var name = hit.collider.name;
            interactionImage.SetActive(true);
            switch (hit.collider.tag)
            {
                case "ObjectiveItem":
                    interactionText.text = "Pickup " + name;
                    break;
                case "Door":
                    interactionText.text = "Open";
                    break;
                case "Generator":
                    interactionText.text = "Turn on";
                    break;
                case "ExitDoor":
                    interactionText.text = "Exit";
                    break;
            }
        }
    }

    private void InteractWithObj()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Interaction"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
            {
                switch (hit.collider.tag)
                {
                    case "ObjectiveItem":
                        playerInventory.Add(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);
                        break;
                    case "Door":
                        var _door = hit.collider.transform.parent.GetComponent<Door>();
                        if (playerInventory.Contains(_door.Key))
                            _door.UnlockDoor();
                        break;
                    case "Generator":
                        FindObjectOfType<Generator>().EnableGenerator();
                        break;
                    case "ExitDoor":
                        if (FindObjectOfType<Generator>().generatorIsOn)
                            GameManager.instance.EndGame();
                        break;
                }
            }
        }
    }

}
