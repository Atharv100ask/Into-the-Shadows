// shoot
// using __ imports namespace
// Namespaces are collection of classes, data types
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehavior is the base class from which every Unity Script Derives
public class PlayerMovement : MonoBehaviour
{
    //movement variables
    public float speed = 4.0f;
    public float rotationSpeed = 70;
    public float force = 200f;
    public float jumpForce = 200f;
    private bool isGrounded = true;
    //camera movement variables
    public float sensitivity = 5f;
    float xRotation = 0f;
    float yRotation = 0f;
    //[SerializeField]
    //private GameObject spawnedPrefab;
    // save the instantiated prefab
    //private GameObject instantiatedPrefab;

    // reference to the camera audio listener
    [SerializeField] private AudioListener audioListener;
    // reference to the camera
    [SerializeField] private Camera playerCamera;

    Rigidbody rb;
    Transform t;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
{
    Vector3 moveDirection = Vector3.zero;

    // if (isGrounded)
    // {
    //     if (Input.GetKey(KeyCode.W))
    //         moveDirection += transform.forward;
    //     if (Input.GetKey(KeyCode.S))
    //         moveDirection -= transform.forward;
    //     if (Input.GetKey(KeyCode.D))
    //         moveDirection += transform.right;
    //     if (Input.GetKey(KeyCode.A))
    //         moveDirection -= transform.right;

    //     // Normalize so diagonal movement isn't faster
    //     moveDirection = moveDirection.normalized * speed;

    //     // Keep vertical (y) velocity intact (for jumping/falling)
    //     rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
    // }
    if (isGrounded)
{
    float forwardInput = 0f;
    float strafeInput = 0f;

    if (Input.GetKey(KeyCode.W)) forwardInput += 1f;
    if (Input.GetKey(KeyCode.S)) forwardInput -= 1f;
    if (Input.GetKey(KeyCode.D)) strafeInput += 1f;
    if (Input.GetKey(KeyCode.A)) strafeInput -= 1f;

    // Multiply by custom speed per direction
    Vector3 forwardMove = transform.forward * forwardInput * speed;
    Vector3 strafeMove = transform.right * strafeInput * (speed * 0.5f); // Half speed for strafing

    Vector3 totalMove = forwardMove + strafeMove;

    rb.linearVelocity = new Vector3(totalMove.x, rb.linearVelocity.y, totalMove.z);
}


    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    {
        rb.AddForce(t.up * jumpForce);
        isGrounded = false;
    }

    // Camera movement
    float mouseX = Input.GetAxis("Mouse X") * sensitivity;
    float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

    yRotation += mouseX;
    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -50f, 8f);

    transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
}


    //check if touching ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}