using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCollector : MonoBehaviour
{
    public GameObject sphere7;  // The golden sphere (7th sphere)
    public TMP_Text scoreText;  // TextMeshPro for artifacts collected display
    public TMP_Text winMessage;  // TextMeshPro for win message
    public TMP_Text directionsMessage;  // TextMeshPro for directions message
    public TMP_Text controlText;  // TMP_Text for control instructions
    public TMP_Text displayControlsText;  // TMP_Text for detailed control display
    public TMP_Text loseMessage;  // TMP_Text for the lose message

    private int collectedCount = 0;  // Tracks collected artifacts
    private int totalSpheres = 6;  // Total artifacts to collect before the 7th sphere appears
    private bool gameWon = false;  // Flag to track if the game is won
    private bool gameStarted = false;  // Flag to track if the game has started
    private bool showingControls = false;  // Flag to track which text is active
    private bool gameLost = false;  // Flag to track if the player has lost

    private Vector3 spawnPoint;  // Store the player's starting position
    private CharacterController characterController;  // Reference to the CharacterController

    void Start()
    {
        // Save the player's starting position
        spawnPoint = transform.position;

        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();

        // Hide the 7th sphere, win message, and lose message initially
        if (sphere7 != null) sphere7.SetActive(false);
        if (winMessage != null) winMessage.gameObject.SetActive(false);
        if (loseMessage != null) loseMessage.gameObject.SetActive(false);

        // Display the directions message at the start and freeze the game
        if (directionsMessage != null) directionsMessage.gameObject.SetActive(true);
        if (controlText != null) controlText.gameObject.SetActive(false);
        if (displayControlsText != null) displayControlsText.gameObject.SetActive(false);

        Time.timeScale = 0;  // Freeze the game until the spacebar is pressed
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameStarted && !gameLost)
            {
                gameStarted = true;
                if (directionsMessage != null) directionsMessage.gameObject.SetActive(false);
                if (controlText != null) controlText.gameObject.SetActive(true);
                Time.timeScale = 1;  // Resume the game
            }
            else if (gameLost)
            {
                RestartGame();  // Restart the game if the player lost
            }
        }

        if (gameStarted && Input.GetKeyDown(KeyCode.J))
        {
            showingControls = !showingControls;

            if (showingControls)
            {
                if (displayControlsText != null) displayControlsText.gameObject.SetActive(true);
                if (controlText != null) controlText.gameObject.SetActive(false);
            }
            else
            {
                if (displayControlsText != null) displayControlsText.gameObject.SetActive(false);
                if (controlText != null) controlText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameWon || gameLost) return;  // Stop if the game is over

        if (other.CompareTag("PlayerObj"))
        {
            Destroy(other.gameObject);  // Destroy the collected artifact
            collectedCount++;  // Increment the counter
            UpdateScoreText();  // Update the display

            if (collectedCount == totalSpheres && sphere7 != null)
            {
                sphere7.SetActive(true);  // Reveal the 7th sphere
                Debug.Log("All artifacts collected! The golden sphere has appeared.");
            }

            if (collectedCount == totalSpheres + 1)
            {
                GameOver();  // Trigger game-over logic
            }
        }
        else if (other.CompareTag("TeleportObject"))
        {
            TeleportToSpawn();  // Teleport the player to the spawn point
        }
        else if (other.CompareTag("Lava"))
        {
            LoseGame();  // Trigger lose game logic
        }
    }

    private void TeleportToSpawn()
    {
        // Disable the CharacterController temporarily
        characterController.enabled = false;

        // Set the player's position to the spawn point
        transform.position = spawnPoint;
        Debug.Log("Teleported to the starting point.");

        // Re-enable the CharacterController
        characterController.enabled = true;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Artifacts Found: {collectedCount}/7";
        }
    }

    private void GameOver()
    {
        gameWon = true;  // Mark the game as won

        if (winMessage != null)
        {
            winMessage.gameObject.SetActive(true);  // Display the win message
        }

        Time.timeScale = 0;  // Stop the game
    }

    private void LoseGame()
    {
        gameLost = true;  // Mark the game as lost

        if (loseMessage != null)
        {
            loseMessage.gameObject.SetActive(true);  // Display the lose message
        }

        Time.timeScale = 0;  // Stop the game
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }
}
