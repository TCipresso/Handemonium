using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesDekay : MonoBehaviour
{
    public float delay = 3f; // Time in seconds before the GameObject is destroyed

    void Start()
    {
        Destroy(gameObject, delay); // Destroy this GameObject after a delay
    }
}
