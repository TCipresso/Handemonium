using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float attackPower;
    public float speed;
    public GameObject hitEffectPrefab;
    public GameObject hands; // Reference to the Hands GameObject
    public GameObject[] powerUps; // Array to hold different power-up prefabs
    public float dropChance = 0.25f; // Chance to drop a power-up (25%)

    public virtual void TakeDamage(float amount)
    {
        TakeDamage(amount, transform.position); // Use enemy position as default hit point
    }

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
        DropPowerUp(); // Try to drop a power-up
        gameObject.SetActive(false); // Deactivate the enemy instead of destroying it
    }

    private void DropPowerUp()
    {
        if (powerUps.Length > 0 && Random.value < dropChance)
        {
            int index = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[index], transform.position, Quaternion.identity);
        }
    }
}
