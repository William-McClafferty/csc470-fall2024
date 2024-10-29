using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";  // Tag for player
    [SerializeField] Transform platform;  // Reference to the moving platform

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            // Parent the player's root transform to the platform
            other.gameObject.transform.SetParent(platform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            // Detach the player from the platform
            other.gameObject.transform.SetParent(null);
        }
    }
}
