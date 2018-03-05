using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBob : MonoBehaviour
{

	public float swayAmount;
    public float swayDamp;

	PlayerController playerController;
    Animation anim;
	Vector3 initialPosition;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animation>();
		initialPosition = transform.localPosition;
    }

    void Update()
    {
        bobbing();
		Sway();
    }

    void bobbing()
    {
        if (playerController.playerWalking && !anim.isPlaying)
        {
            anim.Play();
        }

        if (playerController.crouching)
            anim["CellphoneBob"].speed = 0.5f;
        else
        {
            anim["CellphoneBob"].speed = 1f;
        }
    }

    void Sway()
    {
		float y = Input.GetAxis("Mouse X") * swayAmount;
		float x = Input.GetAxis("Mouse Y") * swayAmount;

		Vector3 offset = new Vector3(x,y, transform.localPosition.z);
		transform.localPosition = Vector3.Lerp(transform.localPosition, offset + initialPosition, Time.deltaTime * swayDamp);
    }
}
