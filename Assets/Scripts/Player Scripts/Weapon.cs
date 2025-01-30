using UnityEngine;
using System.Collections;
using TMPro;

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
    public AudioClip reloadSound; // Optional: Sound for reloading
    public AudioSource audioSource; // AudioSource for playing shooting sounds
    public GameObject hitEffectPrefab;

    private float nextTimeToFire = 0f;
    private int magazineSize = 10; // Size of the magazine
    private int currentAmmo; // Current ammunition count
    private bool isReloading = false; // Track reloading state
    public TextMeshProUGUI ammoText;

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
        currentAmmo = magazineSize; // Initialize ammo count
    }

    void Update()
    {
        if (isReloading)
            return; // Skip update if reloading

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            animator.SetBool("IsShoot", true); // Set IsShoot to true when firing
            UpdateAmmoText();
        }
        else
        {
            animator.SetBool("IsShoot", false);
        }

        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
            UpdateAmmoText();// Trigger reload when ammo is out and not already reloading
        }
    }

    public void FireWeapon()
    {
        if (currentAmmo > 0 && !isReloading) // Check if there is ammo and not reloading
        {
            PlayShootingSound(); // Ensure the sound plays every time the weapon is fired
            FireHitscan();
            currentAmmo--; // Decrement ammo count
        }
    }

    void FireHitscan()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, range))
        {
            //PlayShootingSound();

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

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetTrigger("Reload"); // Trigger reload animation
        if (reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound); // Play reload sound
        }
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for animation to finish
        currentAmmo = magazineSize;
        isReloading = false;
    }

    void Reloading()
    {
        currentAmmo = magazineSize;
        isReloading = false;
    }

    void UpdateAmmoText()
    {
        if (ammoText != null)
            ammoText.text = $"{currentAmmo}";
    }
}
