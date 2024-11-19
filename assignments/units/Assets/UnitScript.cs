using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float rotationSpeed = 10f; // Speed of rotation

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position; // Initialize to current position
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0; // Ignore vertical differences
            float distance = direction.magnitude;

            if (distance > 0.1f) // Keep moving if not close enough
            {
                direction.Normalize();

                // Move the character
                transform.position += direction * moveSpeed * Time.deltaTime;

                // Rotate the character smoothly towards the direction of movement
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                isMoving = false; // Stop moving when close enough
            }
        }
    }

    public void MoveToPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;
    }
}
