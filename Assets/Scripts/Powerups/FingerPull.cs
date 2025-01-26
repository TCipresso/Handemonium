using UnityEngine;

public class FingerPull : MonoBehaviour
{
    public Animator animator;
    public GameObject explosionEffect;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void TriggerExplosion()
    {
        if (explosionEffect != null)
        {
            explosionEffect.SetActive(true);
            StartCoroutine(DeactivateAfterDelay(explosionEffect, 1));
        }
    }

    System.Collections.IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
