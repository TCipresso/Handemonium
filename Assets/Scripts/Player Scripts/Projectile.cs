using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float shootingForce = 500f;
    public float lifetime = 5f;

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

        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
            //Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
