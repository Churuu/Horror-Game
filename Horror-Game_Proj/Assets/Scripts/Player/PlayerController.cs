﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float sensitivity;
    public float stamina = 10.0f;
    public float walkSpeed;
    public float crouchSpeed;
    public float leaningAngle;
    public float jumpForce;
    public float playerHeight;
    public float crouchHeight;
    public bool flashlight = false;
    public Transform head;
    public GameObject cellphone;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public LayerMask ground;
    [HideInInspector] public float speed;
    [HideInInspector] public bool playerWalking = false;
    [HideInInspector] public bool crouching;
    [HideInInspector] public bool playerIsStopped;
    [HideInInspector] public bool playerIsRunning;



    private Rigidbody rb;
    private float y;
    private float x = 0;
    private float maxStamina = 10.0f;
    private float staminaRegenTimer = 0.0f;
    private const float staminaDecreasePerFrame = 1.0f;
    private const float staminaIncreasePerFrame = 3.0f;
    private const float staminaTimeToRegen = 3.0f;
    private float runspeed = 450;
    private bool playerTired = false;
    private Transform newAngleRight;
    private Transform newAngleLeft;
    private Transform normalAngel;
    private bool leaningLeft;
    private bool leaningRight;
    private bool notLeaning;
    private bool cursorLocked = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        y = transform.eulerAngles.y;
    }

    void Update()
    {
        if (FindObjectOfType<Pause>() != null)
        {
            if (!FindObjectOfType<Pause>().isPaused)
            {
                MouseController();
                PlayerWalking();
                Flashlight();
                Stamina();
                Crouching();
                Lean();
                Jump();
            }
        }
        else
        {
            MouseController();
            PlayerWalking();
            Flashlight();
            Stamina();
            Crouching();
            Lean();
            Jump();
        }
    }



    void FixedUpdate()
    {
    }


    // Allows the player to look around
    void MouseController()
    {
        y += Input.GetAxis("Mouse X") * sensitivity;
        x += Input.GetAxis("Mouse Y") * sensitivity;

        x = Mathf.Clamp(x, -90, 90);
        transform.localRotation = Quaternion.Euler(0, y, transform.rotation.eulerAngles.z);
        head.localRotation = Quaternion.Euler(x, 0, transform.rotation.eulerAngles.z);
    }
    // Allows the player to move forward, backwards, right and left
    void PlayerWalking()
    {
        float x_ = Input.GetAxis("Horizontal");
        float z_ = Input.GetAxis("Vertical");
        float yMove = rb.velocity.y;
        Vector3 movement = new Vector3(x_, 0, z_);
        movement.Normalize();
        movement = transform.TransformDirection(movement);
        movement.x *= speed * Time.deltaTime;
        movement.z *= speed * Time.deltaTime;
        movement.y = yMove;
        rb.velocity = movement;
        if (movement.magnitude > 0.1f * Time.deltaTime)
            playerWalking = true;
        else
            playerWalking = false;
    }

    private void Stamina()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerIsRunning = true;
        }
        else
        {
            playerIsRunning = false;
        }


        if (playerIsRunning && !playerTired)
        {
            stamina = Mathf.Clamp(stamina - (staminaDecreasePerFrame * Time.deltaTime), 0.0f, maxStamina);
            staminaRegenTimer = 0.0f;
            speed = runspeed;
        }
        else if (stamina < maxStamina)
        {
            if (staminaRegenTimer >= staminaTimeToRegen)
                stamina = Mathf.Clamp(stamina + (staminaIncreasePerFrame * Time.deltaTime), 0.0f, maxStamina);
            else
                staminaRegenTimer += Time.deltaTime;
        }
        if (stamina <= 0)
            playerTired = true;
        else if (stamina > 0)
            playerTired = false;

        if (playerTired)
            speed = walkSpeed;


    }
    public void Lean()
    {


        Quaternion newAngleRight = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, leaningAngle);
        Quaternion newAngleLeft = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -leaningAngle);

        Quaternion normalAngel = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);


        if (Input.GetKeyDown(KeyCode.Q))
        {
            leaningLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            leaningLeft = false;
            transform.rotation = Quaternion.Lerp(transform.rotation, normalAngel, Time.deltaTime * 3);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            leaningRight = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            leaningRight = false;
        }

        if (transform.rotation.z == 0)
        {
            notLeaning = false;
        }
        else
        {
            notLeaning = true;
        }

        if (leaningRight)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newAngleLeft, Time.deltaTime * 3);

        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, normalAngel, Time.deltaTime * 3);

        }
        if (leaningLeft)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, newAngleRight, Time.deltaTime * 3);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, normalAngel, Time.deltaTime * 3);
        }


    }

    private void Crouching()
    {
        if (Input.GetButtonDown("Crouching"))
            crouching = !crouching;

        var col = GetComponent<CapsuleCollider>();
        if (crouching)
        {
            speed = crouchSpeed;
            col.height = Mathf.Lerp(col.height, crouchHeight, 0.1f);
            head.localPosition = Vector3.Lerp(head.localPosition, new Vector3(head.localPosition.x, crouchHeight - 0.5f, head.localPosition.z), 0.1f);
        }
        else if (!crouching && !playerIsRunning)
        {
            speed = walkSpeed;
            col.height = Mathf.Lerp(col.height, playerHeight, 0.1f);
            head.localPosition = Vector3.Lerp(head.localPosition, new Vector3(head.localPosition.x, playerHeight - 1, head.localPosition.z), 0.1f);
        }
        else if (!crouching && playerIsRunning && !playerTired)
        {
            speed = runspeed;
            col.height = Mathf.Lerp(col.height, playerHeight, 0.1f);
            head.localPosition = Vector3.Lerp(head.localPosition, new Vector3(head.localPosition.x, playerHeight - 1, head.localPosition.z), 0.1f);
        }
    }
    private void Flashlight()
    {
        if (Input.GetButtonDown("Flashlight"))
            flashlight = !flashlight;

        if (flashlight)
            cellphone.GetComponent<Light>().enabled = true;
        else
            cellphone.GetComponent<Light>().enabled = false;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded())
        {
            rb.AddForce(Vector3.up * jumpForce);
            GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }
    }

    public bool grounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, playerHeight / 2, ground))
            return true;
        else
            return false;
    }

    void onCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ai")
            FindObjectOfType<QuickSaveSystem>().Load();
        if (other.gameObject != null && Physics.Raycast(transform.position, Vector3.down))
            GetComponent<AudioSource>().PlayOneShot(landSound);
    }
}
