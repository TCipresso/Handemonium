using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shootingPoint;
    public float range = 100f;
    public LineRenderer lineRenderer;
    public float fireRate = 10f;
    public float damage = 25f;

    private float nextTimeToFire = 0f;

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
            StartCoroutine(ShowBulletTrail(hit.point));

            if (hit.transform.CompareTag("Enemy"))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Debug.Log("Damage to enemy: " + damage);
                }
            }
        }
        else
        {
            StartCoroutine(ShowBulletTrail(shootingPoint.position + shootingPoint.forward * range));
        }
    }

    System.Collections.IEnumerator ShowBulletTrail(Vector3 hitPoint)
    {
        lineRenderer.SetPosition(0, shootingPoint.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.1f);

        lineRenderer.enabled = false;
    }
}
