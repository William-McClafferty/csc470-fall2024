using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Required for restarting the game
using TMPro;  // Import TextMeshPro namespace

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
    private int totalSpheres = 6;  // Total artifacts to collect before 7th appears
    private bool gameWon = false;  // Flag to stop further interactions after winning
    private bool gameStarted = false;  // Flag to track if the game has started
    private bool showingControls = false;  // Flag to track which text is active
    private bool gameLost = false;  // Flag to track if the player has lost

    void Start()
    {
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
        // Start or restart the game when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameStarted && !gameLost)
            {
                // Start the game for the first time
                gameStarted = true;
                if (directionsMessage != null) directionsMessage.gameObject.SetActive(false);
                if (controlText != null) controlText.gameObject.SetActive(true);
                Time.timeScale = 1;  // Resume the game
            }
            else if (gameLost)
            {
                // Restart the game if the player lost
                RestartGame();
            }
        }

        // Toggle between controlText and displayControlsText when J is pressed
        if (gameStarted && Input.GetKeyDown(KeyCode.J))
        {
            showingControls = !showingControls;  // Toggle the flag

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
        if (gameWon || gameLost) return;  // Stop if the game is won or lost

        // Check if the player touches an object tagged "PlayerObj"
        if (other.CompareTag("PlayerObj"))
        {
            Destroy(other.gameObject);  // Destroy the collected artifact
            collectedCount++;  // Increment the counter

            UpdateScoreText();  // Update the artifacts collected display

            // Activate the 7th sphere if all 6 artifacts are collected
            if (collectedCount == totalSpheres && sphere7 != null)
            {
                sphere7.SetActive(true);  // Reveal the 7th sphere
                Debug.Log("All artifacts collected! The golden sphere has appeared.");
            }

            // If the 7th sphere is collected, end the game
            if (collectedCount == totalSpheres + 1)
            {
                GameOver();  // Trigger game-over logic
            }
        }
        // Check if the player touches an object tagged "Lava"
        else if (other.CompareTag("Lava"))
        {
            LoseGame();  // Trigger the lose game logic
        }
    }

    // Update the artifacts collected display
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Artifacts Found: {collectedCount}/7";
        }
    }

    // Handle game-over logic
    private void GameOver()
    {
        gameWon = true;  // Mark the game as won

        if (winMessage != null)
        {
            winMessage.gameObject.SetActive(true);  // Display the win message
        }

        Time.timeScale = 0;  // Stop the game
    }

    // Handle lose game logic
    private void LoseGame()
    {
        gameLost = true;  // Mark the game as lost

        if (loseMessage != null)
        {
            loseMessage.gameObject.SetActive(true);  // Display the lose message
        }

        Time.timeScale = 0;  // Stop the game
    }

    // Restart the game
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }
}