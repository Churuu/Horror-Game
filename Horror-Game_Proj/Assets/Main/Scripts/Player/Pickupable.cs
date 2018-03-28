using UnityEngine;
using System.Collections;

public class Pickupable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.GetComponent<Rigidbody>().useGravity == false)
        {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
