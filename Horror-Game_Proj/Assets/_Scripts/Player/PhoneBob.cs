using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBob : MonoBehaviour
{

	public float swayAmount;

	PlayerController playerController;
    Animator anim;
	Vector3 initialPosition;
    float x;
    float y;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
		initialPosition = transform.localPosition;


    }

    void Update()
    {
        bobbing();
        Sway();
    }


    void bobbing()
    {
        if (playerController.playerWalking)
        {
            anim.Play("CellphoneBob");
        }

        if (playerController.crouching)
            anim.speed = 0.5f;
        else
        {
            anim.speed = 1f;
        }
    }

    void Sway()
    {
		x = -Input.GetAxis("Mouse X") * swayAmount;
		y = Input.GetAxis("Mouse Y") * swayAmount;
        
        anim.SetFloat("Blend", x);
        anim.SetFloat("Up", y);
    }
}
