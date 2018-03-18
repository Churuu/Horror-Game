using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


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
    private float originalWalkspeed = 3;
    private float originalRunspeed;
	private bool playerTired = false;


	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		Cursor.lockState = CursorLockMode.Locked;
		y = transform.eulerAngles.y;
	}
	
	void Update () 
	{
		if(!playerIsStopped)
			MouseController();

		Flashlight();
		//Stamina(); <--- Fixa så det går att springa Hampus och att spelaren kan bli trött
		Crouching();
	}

	void FixedUpdate()
	{
		if(!playerIsStopped)
			PlayerWalking();
	}

	// Allows the player to look around
	void MouseController()
	{
		y += Input.GetAxis("Mouse X") * sensitivity;
		x += Input.GetAxis("Mouse Y") * sensitivity;

		x = Mathf.Clamp(x, -90, 90);
		transform.localRotation = Quaternion.Euler(0, y, 0);
		head.localRotation = Quaternion.Euler(x, 0, 0);
	}
	// Allows the player to move forward, backwards, right and left
	void PlayerWalking()
	{
		float x_ = Input.GetAxis("Horizontal");
		float z_ = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(x_,  0f, z_) * speed * Time.deltaTime;

		transform.Translate(movement);
		if(movement.x > 0 || movement.z > 0 || movement.x < 0 || movement.z < 0)
			playerWalking = true;
		else
			playerWalking = false;
	}

	private void Stamina()
        {
            if (!playerWalking)
            {
                stamina = Mathf.Clamp(stamina - (staminaDecreasePerFrame * Time.deltaTime), 0.0f, maxStamina);
                staminaRegenTimer = 0.0f;
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
                speed = 5;
            else
                speed = originalWalkspeed;

        }

		private void Crouching()
        {
            if (Input.GetButtonDown("Crouching"))
                crouching = !crouching;

			var col = GetComponent<CapsuleCollider>();
            if (crouching)
            {
				speed = 0.5f;
				col.height = Mathf.Lerp(col.height, 1, 0.1f);
				head.localPosition = Vector3.Lerp(head.localPosition, headCrouchPos, 0.1f);
            }
            else if (!crouching)
            {
				speed = 2;
				col.height = Mathf.Lerp(col.height, 1.8f, 0.1f);
				head.localPosition = Vector3.Lerp(head.localPosition, headNormalPos, 0.1f);
            }
        }
        private void Flashlight()
        {
            if(Input.GetButtonDown("Flashlight"))
                flashlight = !flashlight;

            if(flashlight)
                cellphone.GetComponent<Light>().enabled = true;
            else
                cellphone.GetComponent<Light>().enabled = false;
        }

	void onCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Ai")
			FindObjectOfType<QuickSaveSystem>().Load();
	}

	public void StopPlayerForDeathScenario(Transform ai)
	{
		Camera.main.transform.LookAt(ai);
		playerIsStopped = true;
	}
}
