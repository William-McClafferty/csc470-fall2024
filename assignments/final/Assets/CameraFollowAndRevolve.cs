using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowAndRevolve : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float rotationSpeed = 50f; // Speed of rotation around the player

    private Vector3 initialOffset; // Initial offset set in Unity

    void Start()
    {
        // Use the current position as the initial offset relative to the player
        initialOffset = transform.position - player.position;
        UpdateCameraPosition();
    }

    void LateUpdate()
    {
        // Always keep the camera at the relative position to the player
        UpdateCameraPosition();

        // Handle camera rotation around the player
        if (Input.GetKey(KeyCode.O))
        {
            RotateAroundPlayer(-rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.P))
        {
            RotateAroundPlayer(rotationSpeed * Time.deltaTime);
        }
    }

    void RotateAroundPlayer(float angle)
    {
        // Rotate the offset vector around the player
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        initialOffset = rotation * initialOffset;
    }

    void UpdateCameraPosition()
    {
        // Keep the camera in the same position relative to the player
        transform.position = player.position + initialOffset;
        transform.LookAt(player);
    }
}
