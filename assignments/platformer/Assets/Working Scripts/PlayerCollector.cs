using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCollector : MonoBehaviour
{
    public GameObject sphere7;
    public TMP_Text scoreText;
    public TMP_Text winMessage;
    public TMP_Text directionsMessage;
    public TMP_Text controlText;
    public TMP_Text displayControlsText;
    public TMP_Text loseMessage;
    private int collectedCount = 0;
    private int totalSpheres = 6;
    private bool gameWon = false;
    private bool gameStarted = false;
    private bool showingControls = false;
    private bool gameLost = false;
    private Vector3 spawnPoint;
    private CharacterController characterController;
    private Transform currentPlatform = null;

    private bool isOnPlatform = false;

    void Start()
    {
        spawnPoint = transform.position;
        characterController = GetComponent<CharacterController>();

        if (sphere7 != null) sphere7.SetActive(false);
        if (winMessage != null) winMessage.gameObject.SetActive(false);
        if (loseMessage != null) loseMessage.gameObject.SetActive(false);

        if (directionsMessage != null) directionsMessage.gameObject.SetActive(true);
        if (controlText != null) controlText.gameObject.SetActive(false);
        if (displayControlsText != null) displayControlsText.gameObject.SetActive(false);

        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!gameStarted && !gameLost)
            {
                gameStarted = true;
                directionsMessage?.gameObject.SetActive(false);
                controlText?.gameObject.SetActive(true);
                Time.timeScale = 1;
            }
            else if (gameLost)
            {
                RestartGame();
            }
        }

        if (gameStarted && Input.GetKeyDown(KeyCode.J))
        {
            showingControls = !showingControls;
            displayControlsText?.gameObject.SetActive(showingControls);
            controlText?.gameObject.SetActive(!showingControls);
        }

        if (isOnPlatform && currentPlatform == null)
        {
            UnparentPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameWon || gameLost) return;

        if (other.CompareTag("PlayerObj"))
        {
            Destroy(other.gameObject);
            collectedCount++;
            UpdateScoreText();

            if (collectedCount == totalSpheres && sphere7 != null)
            {
                sphere7.SetActive(true);
                Debug.Log("All artifacts collected! The golden sphere has appeared.");
            }

            if (collectedCount == totalSpheres + 1)
            {
                GameOver();
            }
        }
        else if (other.CompareTag("TeleportObject"))
        {
            TeleportToSpawn();
        }
        else if (other.CompareTag("Lava"))
        {
            LoseGame();
        }
        else if (other.CompareTag("MovingPlatform"))
        {
            ParentPlayerToPlatform(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform") && currentPlatform == other.transform)
        {
            UnparentPlayer();
        }
    }

    private void ParentPlayerToPlatform(Transform platform)
    {
        currentPlatform = platform;
        transform.SetParent(currentPlatform);
        isOnPlatform = true;
        Debug.Log("Player is now a child of the platform.");
    }

    private void UnparentPlayer()
    {
        transform.SetParent(null);
        currentPlatform = null;
        isOnPlatform = false;
        Debug.Log("Player left the platform and is no longer a child.");
    }

    private void TeleportToSpawn()
    {
        characterController.enabled = false;
        transform.position = spawnPoint;
        characterController.enabled = true;
        Debug.Log("Teleported to the starting point.");
    }

    private void UpdateScoreText()
    {
        scoreText?.SetText($"Artifacts Found: {collectedCount}/7");
    }

    private void GameOver()
    {
        gameWon = true;
        winMessage?.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void LoseGame()
    {
        gameLost = true;
        loseMessage?.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}