using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pick up found");
            PowerUpManager.Instance.Zoom(30, 10);
            gameObject.SetActive(false);
        }
    }
}
