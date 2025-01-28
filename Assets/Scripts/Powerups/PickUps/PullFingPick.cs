using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullFingPick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pull Ring Power Up Activated");
            PowerUpManager.Instance.ActivatePull(10);
            gameObject.SetActive(false);
        }
    }
}
