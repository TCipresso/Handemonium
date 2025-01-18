using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float crouchedWalkSpeed = 3f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float tiltAngle = 5f;  // Max tilt angle for camera
    public float tiltSmooth = 0.1f;  // Smoothing factor for tilting

    private bool isCrouching = false;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float targetTilt = 0f;  // Target tilt based on input
    private float currentTilt = 0f;  // Current tilt of the camera
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentWalkSpeed = isCrouching ? crouchedWalkSpeed : walkSpeed;
        moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
        if (moveDirection.sqrMagnitude > 1)
        {
            moveDirection.Normalize();
        }
        float currentSpeed = isRunning ? runSpeed : currentWalkSpeed;
        moveDirection *= currentSpeed;
        characterController.Move(moveDirection * Time.deltaTime);

        // Handle Rotation
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        // Camera Tilt Logic
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        targetTilt = -horizontalInput * tiltAngle;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSmooth);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, currentTilt);

        // Handle Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
        }
    }
}
