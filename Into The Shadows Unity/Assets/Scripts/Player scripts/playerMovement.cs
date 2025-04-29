using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehavior is the base class from which every Unity Script Derives
public class PlayerMovement : MonoBehaviour
{
    public bool canControl = true;

    //movement variables
    public float speed = 3.0f;
    //public float rotationSpeed = 70;
    public float force = 200f;
    public float jumpForce = 200f;
    public float sprintMultiplier = 1.3f;
    public float strafeMultiplier = 0.6f;
    public float backwardMultiplier = 0.7f;

    public static bool isGrounded = true;
    //camera movement variables
    public float sensitivity_vert = 4f;
    public float sensitivity_horiz = 4f;
    float xRotation = 0f;
    float yRotation = 0f;

    // reference to the camera audio listener
    [SerializeField] private AudioListener audioListener;
    // reference to the camera
    [SerializeField] private Camera playerCamera;

    Rigidbody rb;
    Transform t;
    public PlayerInfection PlayerInfection;
    //sprint fov
    [Header("FOV Settings")]
    public float normalFOV = 60f;
    public float sprintFOV = 75f;
    public float fovSmoothSpeed = 8f;
    public float CameraRangeUp = -50f;
    public float CameraRangeDown = 9f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked; //cursor lock to window
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 moveDirection = Vector3.zero;

        // Lock/Unlock cursor if Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (canControl)
        {
            if (isGrounded) //player can only move when touching ground
            {
                float forwardInput = 0f;
                float strafeInput = 0f;

                if (Input.GetKey(KeyCode.W)) forwardInput += 1f;
                if (Input.GetKey(KeyCode.S)) forwardInput -= 1f;
                if (Input.GetKey(KeyCode.D)) strafeInput += 1f;
                if (Input.GetKey(KeyCode.A)) strafeInput -= 1f;

                // Determine if sprinting
                float currentSpeed = speed;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    currentSpeed *= sprintMultiplier; //sprint speed increase
                }

                // Multiply by custom speed per direction
                float appliedForwardSpeed = currentSpeed;
                //back walk slow
                if (forwardInput < 0)
                {
                    appliedForwardSpeed *= backwardMultiplier;
                }

                Vector3 forwardMove = transform.forward * forwardInput * appliedForwardSpeed;

                Vector3 strafeMove = transform.right * strafeInput * (currentSpeed * strafeMultiplier); //strafe speed decrease

                Vector3 totalMove = forwardMove + strafeMove;
                rb.linearVelocity = new Vector3(totalMove.x, rb.linearVelocity.y, totalMove.z);


            }


            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(t.up * jumpForce);
                isGrounded = false;
            }
        }

        // Camera movement
        if (PlayerInfection.gameOverText.enabled == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity_horiz;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity_vert;

            yRotation += mouseX;
            xRotation -= mouseY; //up and down cursor movement, rotation of the cam along x axis
            xRotation = Mathf.Clamp(xRotation, CameraRangeUp, CameraRangeDown);

            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        // Sprint FOV effect
        float targetFOV = Input.GetKey(KeyCode.LeftShift) ? sprintFOV : normalFOV;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovSmoothSpeed);
    }


    //check if touching ground
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    
}