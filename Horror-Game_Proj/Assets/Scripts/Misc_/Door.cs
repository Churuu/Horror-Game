using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject key;
    public AudioClip[] sounds;

    AudioSource src;
    bool opened = false;

    void Start()
    {
        src = GetComponent<AudioSource>();
    }

    public void UnlockDoor()
    {
        if (!opened)
        {
            opened = true;
            GetComponent<Animator>().SetTrigger("OpenDoor");
            src.PlayOneShot(sounds[0]);
        }

    }

    public void DoorLocked()
    {
        src.PlayOneShot(sounds[1]);
    }
}
