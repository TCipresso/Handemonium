using UnityEngine;

public class GunSway : MonoBehaviour
{
    public float swayAmount = 1.0f;
    public float swaySpeed = 2.0f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        float swayPosition = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.localPosition = originalPosition + new Vector3(swayPosition, 0, 0);
    }
}
