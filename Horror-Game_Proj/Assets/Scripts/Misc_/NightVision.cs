using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVision : MonoBehaviour
{

    public bool visionEnabled = false;
    public DeferredNightVisionEffect deferredNightVisionEffect;
    public GameObject screen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("NightVision"))
        {
            visionEnabled = !visionEnabled;
			Invoke("SwitchVision", 0.2f);
        }
			GetComponent<Animator>().SetBool("NightVisionEnabled", visionEnabled);
    }

    void SwitchVision()
    {
        if (visionEnabled)
        {
            deferredNightVisionEffect.enabled = true;
			screen.SetActive(true);
        }
        else
        {
            deferredNightVisionEffect.enabled = false;
			screen.SetActive(false);
        }

    }
}
