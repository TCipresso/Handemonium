using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour
{
    public float chargeRange = 10f;   // Distance to check for enemies
    public float chargeSpeed = 25f;   // Speed of the charge movement
    public float stopDistance = 1.5f; // Distance to stop before colliding
    public float chargeCooldown = 2f; // Cooldown before another charge can occur
    public float damage = 50f;        // Damage dealt when charging
    public LayerMask enemyLayer;      // Layer for enemies

    private Rigidbody rb;
    private bool isCharging = false;
    private Transform targetEnemy;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isCharging)
        {
            TryCharge();
        }
    }

    void TryCharge()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, chargeRange);
        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
            StartCoroutine(ChargeToEnemy());
        }
        else
        {
            Debug.Log("No enemy detected within range.");
        }
    }

    IEnumerator ChargeToEnemy()
    {
        isCharging = true;
        Debug.Log("Charging towards: " + targetEnemy.name);

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = targetEnemy.position;

        while (Vector3.Distance(transform.position, targetPosition) > stopDistance)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            rb.MovePosition(transform.position + direction * chargeSpeed * Time.deltaTime);
            yield return null;
        }

        DealChargeDamage();
        yield return new WaitForSeconds(chargeCooldown);
        isCharging = false;
    }

    void DealChargeDamage()
    {
        if (targetEnemy != null)
        {
            Enemy enemyScript = targetEnemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage, targetEnemy.position);
                Debug.Log("Charged and damaged enemy for " + damage);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the charge detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chargeRange);
    }
}
