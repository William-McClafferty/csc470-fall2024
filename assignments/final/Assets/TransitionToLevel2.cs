using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToLevel2 : MonoBehaviour
{
    // Name of the next scene
    private string nextSceneName = "Level2";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with this object
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
