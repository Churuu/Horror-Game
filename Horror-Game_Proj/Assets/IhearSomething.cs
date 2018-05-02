using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IhearSomething : MonoBehaviour {

    public AudioClip sound;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
}
