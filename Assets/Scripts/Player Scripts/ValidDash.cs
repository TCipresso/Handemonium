using UnityEngine;

public class ValidDash : MonoBehaviour
{
    public Transform dashIndicator;  // Assign the indicator GameObject in the Inspector
    public float dashRange = 10f;    // Max range of the dash raycast
    public LayerMask dashableLayers; // Layers that can be dashed to

    void Update()
    {
        UpdateDashIndicator();
    }

    void UpdateDashIndicator()
    {
        RaycastHit hit;

        // Cast a ray forward from the player
        if (Physics.Raycast(transform.position, transform.forward, out hit, dashRange, dashableLayers))
        {
            // Position the indicator at the hit point
            dashIndicator.position = hit.point;
            dashIndicator.rotation = Quaternion.LookRotation(hit.normal); // Align with surface
        }
        else
        {
            // If nothing is hit, position the indicator at max range
            dashIndicator.position = transform.position + transform.forward * dashRange;
            dashIndicator.rotation = Quaternion.identity; // Reset rotation if no hit
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the ray in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * dashRange);
    }
}
