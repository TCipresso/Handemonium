using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootingPoint;
    public float fireRate = 0.2f;

    private float timeUntilFire;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeUntilFire)
        {
            timeUntilFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
    }
}
