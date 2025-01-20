using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform shootingPoint;
    public float range = 100f;
    public LineRenderer lineRenderer;
    public float fireRate = 10f;
    public float damage = 25f;
    public Animator animator;
    public AudioClip shootingSound;
    public AudioSource audioSource; // Publicly referenced AudioSource for playing shooting sounds
    public GameObject hitEffectPrefab;

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            animator.SetBool("IsShoot", true); // Set IsShoot to true when firing
            nextTimeToFire = Time.time + 1f / fireRate;
            FireHitscan();
            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            // Immediately reset the IsShoot to false when the button is released
            animator.SetBool("IsShoot", false);
        }

    }

    void FireHitscan()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, range))
        {
            PlayShootingSound();

            // Check if the hit object is NOT an enemy, then show hit effect
            if (!hit.transform.CompareTag("Enemy"))
            {
                hitEffectPrefab.transform.position = hit.point;
                hitEffectPrefab.transform.rotation = Quaternion.LookRotation(hit.normal);
                hitEffectPrefab.SetActive(false); // Reset any ongoing effect
                hitEffectPrefab.SetActive(true);  // Re-enable to play effect again
            }

            if (hit.transform.CompareTag("Enemy"))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage, hit.point);
                    Debug.Log("Damage to enemy: " + damage);
                }
            }
        }
    }


    void PlayShootingSound()
    {
        if (shootingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootingSound); // Play the shooting sound once
        }
    }

  
}
