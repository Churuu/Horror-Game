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
    public AudioClip[] pickupSound; // 0 Items, 1 Keys

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
            switch (hit.collider.tag)
            {
                case "ObjectiveItem":
                    interactionText.text = "Pickup " + name;
                    interactionImage.SetActive(true);
                    break;
                case "Door":
                    interactionText.text = "Open";
                    interactionImage.SetActive(true);
                    break;
                case "Generator":
                    interactionText.text = "Turn on";
                    interactionImage.SetActive(true);
                    break;
                case "ExitDoor":
                    interactionText.text = "Exit";
                    interactionImage.SetActive(true);
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
                        GetComponent<AudioSource>().PlayOneShot(pickupSound[Random.Range(0, 2)]);
                        Destroy(hit.collider.gameObject);
                        break;
                    case "Door":
                        var _door = hit.collider.transform.parent.GetComponent<Door>();
                        if (playerInventory.Contains(_door.key))
                            _door.UnlockDoor();
                        else
                        {
                            FindObjectOfType<HintBox>().Hint("The door is locked");
                            _door.DoorLocked();
                        }
                        break;
                    case "Generator":
                        FindObjectOfType<Generator>().EnableGenerator();
                        break;
                    case "ExitDoor":
                        if (FindObjectOfType<Generator>().generatorIsOn)
                            GameManager.instance.EndGame();
                        break;
                    case "Valve":
                        hit.collider.gameObject.GetComponent<Valve>().TurnValve();
                        break;

                }
            }
        }
    }

}
