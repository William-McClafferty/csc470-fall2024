using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    private Vector3 spawnPoint;

    void Start()
    {
        spawnPoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            transform.position = spawnPoint;
        }
    }
}