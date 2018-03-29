using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject Key;

    public void UnlockDoor()
    {
        GetComponent<Animator>().SetTrigger("OpenDoor");
    }
}
