using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneLightScr : MonoBehaviour {

    public Light flashlightLight;
    public float storedEnergy = 15500, energyCap = 30000, energyLoss = 10;  //'storedEnergy' = current amount of energy, 'energyCap' = maximum amount of energy, 'energyLoss' = how much energy is lost each second when the light is turned on.
    public bool lightIsOn = false;

    private void Update() {
        if (Input.GetButtonDown("Mobile Light")) {
            if (lightIsOn)
                StopLight();
            else
                StartLight();
        }
    }

    public void StartLight() {
        if (storedEnergy > 0) {
            //There were energy left to use. The flashlight will be turned on.
            lightIsOn = true;
            InvokeRepeating("UseEnergy", 1, 1);  //Start the energy consumption by executing 'UseEnergy' every second.
            flashlightLight.enabled = true;  //Activate the light component.
        }
    }

    public void StopLight() {
        CancelInvoke("UseEnergy");  //No more energy consumption since the light will be turned off.
        lightIsOn = false;
        flashlightLight.enabled = false;  //Deactivate the light component.
    }

    private void UseEnergy() {
        //This method is used to consume energy. Supposed to be invoked once every second while the light is turned on.
        storedEnergy -= energyLoss;  //Subtract 'energyLoss' from 'storedEnergy'. Now the energy will be less.
        if (storedEnergy <= 0)  //Check if there is no more energy, then stop the light by invoking 'StopLight'.
            StopLight();
    }

}
