using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovementLevel1 : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    float height;
    public float crouch;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        height = characterController.height;

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            // Unlock cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }



        // reset player position when falling off the map
        if (this.gameObject.transform.position.y < -15.0f)
        {
            this.gameObject.transform.position = new Vector3(3.1f, 16f, -0.7f);
        }


        if (Input.GetKeyDown(KeyCode.LeftControl))
            Crouch();
        else if (Input.GetKeyUp(KeyCode.LeftControl))
            Stand();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleporter Start"))
        {
            transform.position = new Vector3(139, 5, -42);
            Debug.Log(transform.position);
        }

        if (other.CompareTag("NextLevelDoor"))
        {
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                SceneManager.LoadScene("Level 1");
            }
            else if (SceneManager.GetActiveScene().name == "Level 1")
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
        }
    }


    void Crouch()
    {
        characterController.height = crouch;
    }

    void Stand()
    {
        characterController.height = height;
    }
}