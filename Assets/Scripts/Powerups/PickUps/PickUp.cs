using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float spinSpeed = 360f;

    private void Update()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }

}
