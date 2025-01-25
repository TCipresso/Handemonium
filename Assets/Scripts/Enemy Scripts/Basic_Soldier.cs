using UnityEngine;
using UnityEngine.AI;

public class Basic_Soldier : Enemy
{
    public Transform target; // The player's transform
    public float shootingRange = 10f;
    private NavMeshAgent agent;
    public GameObject projectilePrefab;
    public Transform shootingPoint;
    public float fireRate = 2f;
    private float nextTimeToShoot = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Automatically find and assign the player as the target
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
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
            // Always move towards the player
            agent.SetDestination(target.position);

            // Check distance to potentially shoot
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= shootingRange && Time.time >= nextTimeToShoot)
            {
                ShootAtPlayer();
            }
        }
    }

    void ShootAtPlayer()
    {
        nextTimeToShoot = Time.time + 1f / fireRate;
        Instantiate(projectilePrefab, shootingPoint.position, Quaternion.LookRotation(target.position - shootingPoint.position));
    }
}
