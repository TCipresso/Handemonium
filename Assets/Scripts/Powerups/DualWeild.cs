using UnityEngine;

public class DualWield : MonoBehaviour
{
    public static DualWield Instance { get; private set; } // Singleton instance

    public Transform shootingPoint;
    public float range = 100f;
    public LineRenderer lineRenderer;
    public float fireRate = 10f;
    public float damage = 25f;
    public Animator animator;
    public AudioClip shootingSound;
    public AudioSource audioSource; // AudioSource for playing shooting sounds
    public GameObject hitEffectPrefab;

    private float nextTimeToFire = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally make the object persistent across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Ensure that there is only one instance
        }
    }

    // This method should be called via an Animation Event
    public void FireWeapon()
    {
        if (Time.time >= nextTimeToFire)
        {
            PlayShootingSound();
            nextTimeToFire = Time.time + 1f / fireRate;
            FireHitscan();
        }
    }

    void FireHitscan()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, range))
        {
           // PlayShootingSound();

            if (!hit.transform.CompareTag("Enemy"))
            {
                ShowHitEffect(hit);
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

    void ShowHitEffect(RaycastHit hit)
    {
        if (hitEffectPrefab != null)
        {
            GameObject tempEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(tempEffect, 2f); // Destroy the effect after 2 seconds
        }
    }

    void PlayShootingSound()
    {
        if (shootingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootingSound);
        }
    }
}
