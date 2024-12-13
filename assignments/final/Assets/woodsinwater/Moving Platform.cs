using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f; // Speed of platform movement
    public float distance = 3f; // Maximum movement distance
    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the platform back and forth
        float movement = speed * Time.deltaTime;
        if (movingRight)
        {
            transform.position += new Vector3(movement, 0, 0);
            if (transform.position.x >= startPosition.x + distance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position -= new Vector3(movement, 0, 0);
            if (transform.position.x <= startPosition.x - distance)
            {
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Parent the player to the platform
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Detach the player from the platform
            collision.transform.SetParent(null);
        }
    }
}
