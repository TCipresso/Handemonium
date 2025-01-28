using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Punch Power Up Activated");
            PowerUpManager.Instance.Power(15);
            //PowerUpManager.Instance.ChangeSpeeds(30, 15);
            gameObject.SetActive(false);
        }
    }
}
