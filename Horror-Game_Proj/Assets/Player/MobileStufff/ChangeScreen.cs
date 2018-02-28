using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour {

    public Texture Kamera;
    public Texture Bild1;
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.Keypad1))
        {
            gameObject.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", Kamera);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            gameObject.GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", Bild1);
        }







    }
}
