
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    GameObject mainCamera;
    GameObject carriedObject;
    bool carrying = false;
    public float distance;
    public float rayDistance;
    public Transform playerCam;
    float throwForce = 400;


    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");

    }

    void Update()
    {
        transform.parent = playerCam;

        if (carrying == true)
        {



        if (Input.GetKeyDown(KeyCode.R))  //DROP
        {
            carriedObject.gameObject.GetComponent<Rigidbody>().drag = 0;
            carrying = false;
            Destroy(playerCam.GetComponent<FixedJoint>());
            Debug.Log("Bam");
        }


            if (Input.GetMouseButtonDown(0))  //Kasta
            {
                carriedObject.gameObject.GetComponent<Rigidbody>().drag = 0;
                carrying = false;
                Destroy(playerCam.GetComponent<FixedJoint>());
                carriedObject.GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);

            }
        }
        else
        {
            Pickup();
        }
        

    }


    void Pickup()
    {
        if (Input.GetKeyDown(KeyCode.R) && carrying == false)
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                Pickupable p = hit.collider.GetComponent<Pickupable>();
                if (p != null)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        playerCam.gameObject.AddComponent<FixedJoint>();
                        playerCam.gameObject.GetComponent<FixedJoint>().enablePreprocessing = false;
                        playerCam.gameObject.GetComponent<FixedJoint>().breakTorque = 10000;
                        playerCam.gameObject.GetComponent<FixedJoint>().breakForce = 10000;
                        playerCam.gameObject.GetComponent<FixedJoint>().connectedBody = p.gameObject.GetComponent<Rigidbody>();
                        playerCam.gameObject.GetComponent<FixedJoint>().enableCollision = true;

                        p.gameObject.GetComponent<Rigidbody>().drag = 20;

                        carriedObject = p.gameObject;

                        if (playerCam.gameObject.GetComponent<FixedJoint>().connectedBody == p.gameObject.GetComponent<Rigidbody>())
                        {
                            
                        carrying = true;
                        }


                    }
                }
            }
        }
    }
}


 

 