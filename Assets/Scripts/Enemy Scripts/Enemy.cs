using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float attackPower;
    public float speed;
    public GameObject hitEffectPrefab;
    public GameObject hands; // Reference to the Hands GameObject


    public virtual void TakeDamage(float amount)
    {
        TakeDamage(amount, transform.position); // Use enemy position as default hit point
    }

    // Overloaded TakeDamage with hit point
    public virtual void TakeDamage(float amount, Vector3 hitPoint)
    {
        health -= amount;

        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
            effect.SetActive(true);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}
