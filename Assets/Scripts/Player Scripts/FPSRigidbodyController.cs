using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSRigidbodyController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float crouchedWalkSpeed = 3f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float tiltAngle = 5f;  // Max tilt angle for camera
    public float tiltSmooth = 0.1f;  // Smoothing factor for tilting
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isCrouching = false;
    private float rotationX = 0;
    private float targetTilt = 0f;
    private float currentTilt = 0f;
    private bool jumpRequested = false;  // To capture jump requests

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle Mouse Rotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, currentTilt);

        // Camera Tilt Logic
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        targetTilt = -horizontalInput * tiltAngle;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSmooth);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, currentTilt);

        // Player Horizontal Rotation
        transform.Rotate(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // Handle Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
        }

        // Capture Jump Input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        // Handle Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentWalkSpeed = isCrouching ? crouchedWalkSpeed : walkSpeed;
        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        float currentSpeed = isRunning ? runSpeed : walkSpeed;
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);

        // Apply Jump if Requested
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;  // Reset jump request
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the ground
        if (collision.contacts[0].normal.y > 0.5)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
