using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    [HideInInspector] public List<GameObject> playerInventory = new List<GameObject>();
    public GameObject interaction;
    public Text interactionText;

    void Update()
    {
        PickObject();
        OpenDoor();
        ShowInformationOnScreenIneractions();
        Generator();
        WinDoor();
    }


    private void PickObject()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Interaction"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
            {
                if (hit.collider.tag == "ObjectiveItem")
                {
                    playerInventory.Add(hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
    private void OpenDoor()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Interaction"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
            {
                if (hit.collider.tag == "Door")
                {
                    var _door = hit.collider;
                    if (playerInventory.Contains(_door.transform.parent.GetComponent<Door>().keyToOpen))
                    {
                        _door.transform.parent.GetComponent<Door>().UnlockRooms();
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
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2f))
        {
            if (hit.collider.tag == "Door")
            {
                interaction.SetActive(true);
                interactionText.text = "Door";
            }
            if (hit.collider.tag == "ObjectiveItem")
            {
                interaction.SetActive(true);
                interactionText.text = hit.collider.name;
            }
            if (hit.collider.tag == "Generator")
            {
                interaction.SetActive(true);
                interactionText.text = "Generator";
            }
            if (hit.collider.tag == "WinDoor")
            {
                interaction.SetActive(true);
                interactionText.text = "Exit";
            }
        }
    }

    private void Generator()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Interaction"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
            {
                if (hit.collider.tag == "Generator")
                {
                    FindObjectOfType<Generator>().generatorIsOn = true;
                    print("generator is online");
                }
            }
        }
    }
    private void WinDoor()
    {
        RaycastHit hit;
        if (Input.GetButtonDown("Interaction"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
            {
                if (hit.collider.tag == "WinDoor")
                {
                    if (FindObjectOfType<Generator>().generatorIsOn)
                    {
                        FindObjectOfType<WinHandler>().WonGame();
                    }
                }
            }
        }
    }
}
