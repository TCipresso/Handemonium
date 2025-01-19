using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shootingPoint;
    public float range = 100f;
    public LineRenderer lineRenderer; // Reference to the LineRenderer component
    public float fireRate = 10f; // Shots per second

    private float nextTimeToFire = 0f; // Time until the next shot is allowed

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            FireHitscan();
        }
    }

    void FireHitscan()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, range))
        {
            // Show the bullet trail
            StartCoroutine(ShowBulletTrail(hit.point));

            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Damage to enemy");
            }
        }
        else
        {
            // No object was hit, draw line to max range
            StartCoroutine(ShowBulletTrail(shootingPoint.position + shootingPoint.forward * range));
        }
    }

    System.Collections.IEnumerator ShowBulletTrail(Vector3 hitPoint)
    {
        lineRenderer.SetPosition(0, shootingPoint.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.1f); // Display the line for a short duration

        lineRenderer.enabled = false; // Hide the line again
    }
}
