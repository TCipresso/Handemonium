using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform player;
    public float sensitivity = 0.5f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, sensitivity * Time.deltaTime);
    }
}