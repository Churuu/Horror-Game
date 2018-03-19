using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float sensitivity;
    public float speed;
    public float stamina = 10.0f;
    public Transform head;
    [HideInInspector] public bool flashlight = true;
    [HideInInspector] public bool crouching;
    public GameObject cellphone;
    public Vector3 headNormalPos;
    public Vector3 headCrouchPos;
    public bool playerWalking = false;
    [HideInInspector] public bool playerIsStopped;


    private Rigidbody rb;
    private float y = 0;
    private float x = 0;
    private float maxStamina = 10.0f;
    private float staminaRegenTimer = 0.0f;
    private const float staminaDecreasePerFrame = 1.0f;
    private const float staminaIncreasePerFrame = 3.0f;
    private const float staminaTimeToRegen = 3.0f;
    private float originalWalkspeed;
    private float runspeed = 8;
    public bool playerTired = false;
    private bool playerIsRunning;
    private Transform newAngleRight;
    private Transform newAngleLeft;
    private Transform normalAngel;
    private bool leaningLeft;
    private bool leaningRight;
    private bool notLeaning;
    public float crouchSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        originalWalkspeed = speed;
    }

    void Update()
    {
        if (!playerIsStopped)
            MouseController();
        Flashlight();
        Stamina();
        Crouching();
        Lean();

    }

    void FixedUpdate()
    {
        if (!playerIsStopped)
            PlayerWalking();
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
        Vector3 movement = new Vector3(x_, 0f, z_) * speed * Time.deltaTime;

        transform.Translate(movement);
        if (movement.x > 0 || movement.z > 0 || movement.x < 0 || movement.z < 0)
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
            speed = originalWalkspeed;


    }
    public void Lean()
    {


        Quaternion newAngleRight = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 30);
        Quaternion newAngleLeft = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -30);

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
            col.height = Mathf.Lerp(col.height, 1, 0.1f);
            head.localPosition = Vector3.Lerp(head.localPosition, headCrouchPos, 0.1f);
        }
        else if (!crouching && !playerIsRunning)
        {
            speed = originalWalkspeed;
            col.height = Mathf.Lerp(col.height, 1.8f, 0.1f);
            head.localPosition = Vector3.Lerp(head.localPosition, headNormalPos, 0.1f);
        }
        else if (!crouching && playerIsRunning && !playerTired)
        {
            speed = runspeed;
            col.height = Mathf.Lerp(col.height, 1.8f, 0.1f);
            head.localPosition = Vector3.Lerp(head.localPosition, headNormalPos, 0.1f);
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

    void onCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ai")
            FindObjectOfType<QuickSaveSystem>().Load();
    }

    public void StopPlayerForDeathScenario(Transform t)
    {
        transform.LookAt(t);
        playerIsStopped = true;
    }
}
