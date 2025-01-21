using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon Instance { get; private set; } // Singleton instance

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
            animator.SetBool("IsShoot", false);
        }
    }

    void FireHitscan()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, range))
        {
            PlayShootingSound();

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
            hitEffectPrefab.transform.position = hit.point;
            hitEffectPrefab.transform.rotation = Quaternion.LookRotation(hit.normal);
            hitEffectPrefab.SetActive(false);
            hitEffectPrefab.SetActive(true);
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
