using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject key;

    public void UnlockDoor()
    {
        GetComponent<Animator>().SetTrigger("OpenDoor");
        print("Door unlocked");
    }
}
