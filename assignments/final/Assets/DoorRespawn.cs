using UnityEngine;

public class DoorRespawn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered a "WrongDoor" trigger
        if (other.CompareTag("WrongDoor"))
        {
            // Find the respawn point tagged "DoorRespawn"
            GameObject respawnPoint = GameObject.FindGameObjectWithTag("DoorRespawn");

            // Ensure the respawn point exists
            if (respawnPoint != null)
            {
                // Teleport the player to the respawn point's position
                transform.position = respawnPoint.transform.position;
                Debug.Log("Player respawned to: " + respawnPoint.transform.position);
            }
            else
            {
                Debug.LogError("No object with tag 'DoorRespawn' found in the scene!");
            }
        }
    }
}
