using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public GameObject particleSmoke;
    public bool generatorIsOn = false;
    public AudioClip pourSound;

    void TurnOn()
    {
        particleSmoke.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void EnableGenerator()
    {
        if (!generatorIsOn)
        {
            GetComponent<AudioSource>().PlayOneShot(pourSound);
            generatorIsOn = true;
            Invoke("TurnOn", pourSound.length + .5f);
        }
    }
}


