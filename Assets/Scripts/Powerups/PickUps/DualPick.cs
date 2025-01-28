using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualPick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Dual Wield Power Up Activated");
            PowerUpManager.Instance.DualWield(10);
            gameObject.SetActive(false);
        }

        else
        {
            return;
        }
    }
}