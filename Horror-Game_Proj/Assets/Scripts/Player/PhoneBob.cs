using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBob : MonoBehaviour
{

    public float swayAmount;
    public float maxStoreValue;

    Queue<float> test;
    List<Vector2> storedVectors = new List<Vector2>();
    PlayerController playerController;
    Animator anim;
    float x;
    float y;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!FindObjectOfType<NightVision>().visionEnabled)
        {
            Sway();
            bobbing();
        }
    }


    void bobbing()
    {
        anim.SetBool("IsMoving", playerController.playerWalking);

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
        Vector2 mouse = new Vector2(x, y);
        storedVectors.Insert(0, mouse);
        if (storedVectors.Count > maxStoreValue)
            storedVectors.RemoveAt(storedVectors.Count - 1);

        Vector2 combinedVectors = new Vector2();
        foreach (var vector in storedVectors)
        {
            combinedVectors += vector;
        }
        combinedVectors = combinedVectors / maxStoreValue;

        anim.SetFloat("Blend", combinedVectors.x);
        anim.SetFloat("Up", combinedVectors.y);
    }
}
