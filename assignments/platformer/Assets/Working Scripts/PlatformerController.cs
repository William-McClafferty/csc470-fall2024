using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    public CharacterController cc;
    public Camera playerCamera;
    float moveSpeed = 40f;
    float dashSpeed = 60f;
    float jumpVelocity = 65f;
    float fallingTime = 0f;
    float yVelocity = 0f;
    float gravity = -150f;
    float rotationSpeed = 10f;

    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");
        float currentSpeed = Input.GetKey(KeyCode.Z) ? dashSpeed : moveSpeed;

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

        Vector3 moveDirection = new Vector3(hAxis, 0, vAxis).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        Vector3 move = moveDirection * currentSpeed * Time.deltaTime;
        move.y += yVelocity * Time.deltaTime;

        cc.Move(move);

        if (playerCamera != null)
        {
            playerCamera.transform.position = new Vector3(transform.position.x, playerCamera.transform.position.y, transform.position.z);
        }
    }
}