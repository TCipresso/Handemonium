using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour
{
    public static Charge Instance { get; private set; }  // Singleton instance

    public Rigidbody rb;                  // Reference to the player's Rigidbody
    public Transform orientation;         // Reference to the player's orientation
    public float dashSpeed = 10f;         // How fast the dash moves
    public float dashDistance = 10f;      // How far the dash goes
    public float dashCooldown = 1.5f;     // Cooldown duration between dashes
    public LayerMask obstacleLayers;      // Layers that represent obstacles

    private bool isDashing;               // To check if currently dashing
    private bool canDash = true;          // To check if dash is available
    private Vector3 dashDirection;        // Direction of the dash
    private float dashTime;               // Time it takes to complete the dash

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optionally make the object persistent across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // Ensure that there is only one instance
        }

        dashCooldown = 1.5f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isDashing)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        canDash = false;
        isDashing = true;
        Vector3 startPosition = transform.position;
        dashDirection = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
        if (dashDirection != Vector3.zero)
        {
            dashDirection.Normalize(); // Normalize to ensure consistent dash direction
        }
        else
        {
            dashDirection = orientation.forward; // Default to forward if no input
        }

        dashTime = dashDistance / dashSpeed; // Calculate how long the dash should take based on distance and speed
        float actualDashDistance = CalculateDashDistance(startPosition, dashDirection, dashDistance);
        StartCoroutine(PerformDash(startPosition, dashDirection, actualDashDistance));
    }

    private float CalculateDashDistance(Vector3 startPosition, Vector3 direction, float maxDistance)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPosition, direction, out hit, maxDistance, obstacleLayers))
        {
            return hit.distance - 0.01f; // Stop slightly before hitting the object
        }
        return maxDistance; // No obstacle, return the full dash distance
    }

    private IEnumerator PerformDash(Vector3 startPosition, Vector3 direction, float distance)
    {
        float elapsed = 0;
        Vector3 lastSafePosition = startPosition;

        while (elapsed < dashTime)
        {
            Vector3 nextPosition = startPosition + direction * dashSpeed * elapsed;
            if (!IsPositionSafe(nextPosition, direction))
            {
                rb.MovePosition(lastSafePosition);
                break;
            }
            lastSafePosition = nextPosition;
            rb.MovePosition(nextPosition);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(startPosition + direction * distance);
        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    private bool IsPositionSafe(Vector3 position, Vector3 direction)
    {
        float checkDistance = 0.1f;
        RaycastHit hit;
        if (Physics.Raycast(position, direction, out hit, checkDistance, obstacleLayers))
        {
            return false;
        }
        return true;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
