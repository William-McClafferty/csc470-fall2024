using UnityEngine;

public class CombinedPlayerController : MonoBehaviour
{
    public CharacterController cc; // Character Controller for movement
    public Transform cameraTransform; // Reference to the camera's transform

    public float moveSpeed = 5f; // Normal move speed
    public float dashSpeed = 15f; // Speed when dashing
    public float jumpVelocity = 20f; // Jump force
    public float gravity = -75f; // Gravity strength
    public float rotationSpeed = 10f; // Speed for smooth rotation

    private float yVelocity = 0f; // Vertical velocity
    private float fallingTime = 0f; // Tracks time falling

    void Update()
    {
        // Get input for movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Determine movement speed (normal or dash)
        float currentSpeed = Input.GetKey(KeyCode.Z) ? dashSpeed : moveSpeed;

        // Calculate direction relative to the camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Eliminate vertical influence (y-axis)
        forward.y = 0f;
        right.y = 0f;

        // Normalize to avoid diagonal speed boost
        forward.Normalize();
        right.Normalize();

        // Calculate movement direction
        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        // Handle gravity and jumping
        if (!cc.isGrounded)
        {
            fallingTime += Time.deltaTime;

            if (fallingTime < 0.5f && Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpVelocity;
            }

            if (yVelocity > 0 && Input.GetKeyUp(KeyCode.Space))
            {
                yVelocity = 0;
            }

            yVelocity += gravity * Time.deltaTime;
        }
        else
        {
            yVelocity = -20f;
            fallingTime = 0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpVelocity;
            }
        }

        // Stop movement if no input is detected
        if (horizontal == 0 && vertical == 0)
        {
            moveDirection = Vector3.zero;
        }

        // Apply vertical movement
        Vector3 move = moveDirection * currentSpeed * Time.deltaTime;
        move.y += yVelocity * Time.deltaTime;

        // Move the player
        cc.Move(move);

        // Rotate the player to face the direction of movement
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
