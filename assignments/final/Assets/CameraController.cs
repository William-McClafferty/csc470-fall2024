using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float moveSpeed = 10f;

    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.P))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.I))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.K))
        {
            transform.position -= transform.right * moveSpeed * Time.deltaTime;
        }
    }
}