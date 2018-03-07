using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBob : MonoBehaviour
{

	public float swayAmount;
    public float swayDamp;

	PlayerController playerController;
    Animator anim;
	Vector3 initialPosition;

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
        if (playerController.playerWalking && !CellphoneAnimation.isPlaying)
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
		float x = -Input.GetAxis("Mouse X") * swayAmount;
		float y = Input.GetAxis("Mouse Y") * swayAmount;

        anim.SetFloat("Blend", x);
        anim.SetFloat("Up", y);

		/*Vector3 offset = new Vector3(x,y, 0);
		transform.localPosition = Vector3.Lerp(transform.localPosition, offset + initialPosition, Time.deltaTime * swayDamp);*/
    }
}
