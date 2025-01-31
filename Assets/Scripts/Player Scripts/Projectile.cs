using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float shootingForce = 500f;
    public float lifetime = 5f;
    public float damage = 20f; // Damage the projectile deals to the player

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shootingForce, ForceMode.Impulse);
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Access the PlayerStats component and call the TakeDamage method
            PlayerStats playerStats = other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the projectile after hitting the player
        }
        else if (other.CompareTag("Enemy"))
        {
             
        }
        else
        {
            Destroy(gameObject); // Destroy the projectile if it hits something other than player or enemy
        }
    }
}
