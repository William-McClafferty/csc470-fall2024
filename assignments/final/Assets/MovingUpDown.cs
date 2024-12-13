using UnityEngine;

public class MovingUpDown : MonoBehaviour
{
    public float speed = 2f; // Speed of the platform's movement
    public float distance = 3f; // Maximum distance the platform will move up or down

    private Vector3 startPosition; // Starting position of the platform
    private bool movingUp = true; // Direction of movement

    void Start()
    {
        // Record the starting position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate movement per frame
        float movement = speed * Time.deltaTime;

        if (movingUp)
        {
            // Move the platform up
            transform.position += new Vector3(0, movement, 0);

            // Check if the platform reached the maximum upward distance
            if (transform.position.y >= startPosition.y + distance)
            {
                movingUp = false;
            }
        }
        else
        {
            // Move the platform down
            transform.position -= new Vector3(0, movement, 0);

            // Check if the platform reached the maximum downward distance
            if (transform.position.y <= startPosition.y - distance)
            {
                movingUp = true;
            }
        }
    }
}
