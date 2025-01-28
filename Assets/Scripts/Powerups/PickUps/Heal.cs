using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pick up found");
            PowerUpManager.Instance.Heal(30);
            gameObject.SetActive(false);
        }
    }
}
