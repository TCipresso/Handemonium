using UnityEngine;
using UnityEngine.AI;

// Ensure this class inherits from Enemy
public class Bomber : Enemy
{
    private NavMeshAgent agent;
    public Transform target; // Target usually will be the player
    public GameObject explosionPrefab; // Assign an explosion prefab here
    public float damageAmount = 20f;  // Damage dealt to the playerS


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Automatically find and assign the player as the target
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogError("Player object not found. Please ensure the player is tagged correctly.");
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Continuously update the destination to the player's position
            agent.SetDestination(target.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check for collision with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            if (explosionPrefab != null)
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Debug.Log("Explosion instantiated and damage dealt");
            if (PlayerStats.Instance != null)
            {
                PlayerStats.Instance.TakeDamage(damageAmount);
            }
            Destroy(gameObject);
        }
    }
}
