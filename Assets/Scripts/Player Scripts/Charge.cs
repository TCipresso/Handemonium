using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour
{
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
        Vector3 lastSafePosition = startPosition; // Track the last known safe position

        while (elapsed < dashTime)
        {
            Vector3 nextPosition = startPosition + direction * dashSpeed * elapsed;
            if (!IsPositionSafe(nextPosition, direction))
            {
                rb.MovePosition(lastSafePosition); // Move to the last safe position instead of the next position
                break; // Exit the loop as we hit an unsafe position
            }
            lastSafePosition = nextPosition;
            rb.MovePosition(nextPosition);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(startPosition + direction * distance); // Ensure character ends exactly at the adjusted dash distance
        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    private bool IsPositionSafe(Vector3 position, Vector3 direction)
    {
        float checkDistance = 0.1f; // Small forward check to prevent clipping
        RaycastHit hit;
        if (Physics.Raycast(position, direction, out hit, checkDistance, obstacleLayers))
        {
            return false; // Not safe if we hit something this close
        }
        return true;
    }


    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
