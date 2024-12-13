using UnityEngine;
using TMPro;

public class EndGameTrigger : MonoBehaviour
{
    [Header("End Game UI Elements")]
    public Canvas endGameCanvas; // The canvas that will show the end message
    public TextMeshProUGUI endGameMessage; // The end game TextMeshPro component

    [Header("End Game Message")]
    [TextArea]
    public string message = "Game Over! You are in Jail!"; // Customizable end game message

    private void Start()
    {
        // Ensure the canvas and message are hidden at the start of the game
        if (endGameCanvas != null)
        {
            endGameCanvas.gameObject.SetActive(false);
        }
        if (endGameMessage != null)
        {
            endGameMessage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player touches an object tagged 'Jail'
        if (other.CompareTag("Jail"))
        {
            // Activate the end game canvas
            if (endGameCanvas != null)
            {
                endGameCanvas.gameObject.SetActive(true);
            }

            // Display the end game message
            if (endGameMessage != null)
            {
                endGameMessage.gameObject.SetActive(true);
                endGameMessage.text = message; // Set the custom message
            }

            // Freeze the game
            Time.timeScale = 0f; // Stop all game activity
            Debug.Log("Game Over: Player touched Jail. Game Frozen.");
        }
    }
}
