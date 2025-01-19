using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float attackPower;
    public float speed;

    public virtual void TakeDamage(float amount)
    {
        health -= amount;
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
