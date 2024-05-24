using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovementTest : MonoBehaviour
{
    float playerHeight = 2f;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded { get; private set; }

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    private int enemiesCounter;
    private bool transitionToNextLevel = false;
    public Text enemiesCounterText;

    public Text grenadesCount;


    private Vector3 doorInitialPosition;
    private Quaternion doorInitialRotation;

    private Vector3 lootInitialPosition;
    private Quaternion lootInitialRotation;

    public GameObject ammoBox;


    public int ammoBoxAmountPrimary = 30;
    public int ammoBoxAmountSecondary = 15;


     bool isWalking = false;
     AudioSource walkSound;


    private bool wasKeyCollected = false;
    public GameObject key;


    private bool isDoorOpening = false;
    private bool isLootPicked = false;


    public GameObject collectKeyMessage;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        walkSound = GetComponent<AudioSource>();

        collectKeyMessage.SetActive(false);
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();



        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        enemiesCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesCounterText.text = "Enemies: " + enemiesCounter;
        if (enemiesCounter == 0)
        {
            if (!wasKeyCollected)
            {
                collectKeyMessage.SetActive(true);

                if (SceneManager.GetActiveScene().name == "Level 1")
                    Instantiate(key, new Vector3(-32f, 5f, 33f), Quaternion.identity);
                else if (SceneManager.GetActiveScene().name == "Level 2")
                    Instantiate(key, new Vector3(13f, 1.7f, 53f), Quaternion.identity);
                else if (SceneManager.GetActiveScene().name == "Level 3")
                    Instantiate(key, new Vector3(16f, 9f, 54f), Quaternion.identity);
                else if (SceneManager.GetActiveScene().name == "Final Boss")
                    Instantiate(key, new Vector3(9f, 2f, -5f), Quaternion.identity);

                wasKeyCollected = true;
            }
        }



        if (rb.velocity.x != 0 || rb.velocity.y != 0)
            isWalking = true;
        else
            isWalking = false;

        if (isWalking)
        {
            if (!walkSound.isPlaying)
                walkSound.Play();
        }
        else
            walkSound.Stop();


        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (transform.position.y < 0f)
                transform.position = new Vector3(2.5f, 10f, 1.4f);
        }
        else if (SceneManager.GetActiveScene().name == "Level 1")
        {
            if (transform.position.y < -5f)
                transform.position = new Vector3(3.2f, 5f, -1.3f);
        }
        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            if (transform.position.y < -5f)
                transform.position = new Vector3(27.8f, 5f, 10f);
        }
        else if (SceneManager.GetActiveScene().name == "Level 3")
        {
            if (transform.position.y < -5f)
                transform.position = new Vector3(28f, 5f, 10f);
        }
        else if (SceneManager.GetActiveScene().name == "Final Boss")
        {
            if (transform.position.y < -5f)
                transform.position = new Vector3(9f, 5f, -24f);
        }
        
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }


	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Door To Open")
		{
            if (!isDoorOpening)
            {
                isDoorOpening = true;
                doorInitialPosition = collision.gameObject.transform.localPosition;
                doorInitialRotation = collision.gameObject.transform.rotation;

                collision.gameObject.transform.localPosition = new Vector3(-50, collision.gameObject.transform.localPosition.y, -50);
                StartCoroutine(CloseDoor(collision.gameObject, 2f));
            }
        }

        if (collision.gameObject.tag == "NextLevelDoor")
        {
            if (transitionToNextLevel)
            {
                if (SceneManager.GetActiveScene().name == "Level 1")
                {
                    SceneManager.LoadScene("Level 2");
                }
                else if (SceneManager.GetActiveScene().name == "Level 2")
                {
                    SceneManager.LoadScene("Level 3");
                }
                else if (SceneManager.GetActiveScene().name == "Level 3")
                {
                    SceneManager.LoadScene("Final Boss");
                }
                else if (SceneManager.GetActiveScene().name == "Final Boss")
                {
                    SceneManager.LoadScene("Ending");
                }
            }
        }
	}

    private IEnumerator CloseDoor(GameObject door, float sec)
    {
        yield return new WaitForSeconds(sec);
        //Debug.Log("New positionn at: " + doorInitialPosition);
        door.transform.localPosition = doorInitialPosition;
        door.transform.rotation = doorInitialRotation;

        isDoorOpening = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleporter Start"))
        {
            transform.position = new Vector3(124, 5, -42);
            //Debug.Log(transform.position);
        }

        if (other.CompareTag("TeleporterToLevel1"))
        {
            SceneManager.LoadScene("Level 1");
        }

        if (other.CompareTag("Ammo Box")){

            if (SceneManager.GetActiveScene().name == "Final Boss")
            {
                if (!isLootPicked)
                {
                    isLootPicked = true;
                    lootInitialPosition = other.gameObject.transform.position;
                    lootInitialRotation = other.gameObject.transform.rotation;

                    FindObjectOfType<AudioManager>().Play("Ammo_Pickup");

                    AR_Weapon.ammoInMagazine += ammoBoxAmountPrimary;
                    SecondaryGun.ammoInMagazine += ammoBoxAmountSecondary;

                    if (GrenadeThrow.grenadeAmount < 3)
                    {
                        GrenadeThrow.grenadeAmount++;
                        grenadesCount.text = "x " + GrenadeThrow.grenadeAmount;
                    }

                    Destroy(other.gameObject);

                    StartCoroutine(RespawnLoot(ammoBox, 10f));
                }
			}
			else
			{
                FindObjectOfType<AudioManager>().Play("Ammo_Pickup");

                AR_Weapon.ammoInMagazine += ammoBoxAmountPrimary;
                SecondaryGun.ammoInMagazine += ammoBoxAmountSecondary;

                if (GrenadeThrow.grenadeAmount < 3)
                {
                    GrenadeThrow.grenadeAmount++;
                    grenadesCount.text = "x " + GrenadeThrow.grenadeAmount;
                }

                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Access Key"))
		{
            FindObjectOfType<AudioManager>().Play("Key");

            transitionToNextLevel = true;

            collectKeyMessage.SetActive(false);

            Destroy(other.gameObject);
        }
    }


    private IEnumerator RespawnLoot(GameObject loot, float sec)
    {
        yield return new WaitForSeconds(sec);

        Instantiate(loot, lootInitialPosition, lootInitialRotation);

        isLootPicked = false;
    }
}