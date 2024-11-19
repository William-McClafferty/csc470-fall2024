using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public TMP_Text introText;
    public TMP_Text scoreText; // Reference to the TMP_Text for the score
    public TMP_Text winMessageText; // Reference to the TMP_Text for the win message
    public GameObject door; // Reference to the door object
    public float interactionDistance = 4.0f;

    private UnitScript playerUnit;
    private DialogueScript activeDialogue;
    private bool introTextVisible = true;
    private int score = 0; // Player's score

    void Start()
    {
        playerUnit = FindObjectOfType<UnitScript>();
        if (playerUnit == null)
        {
            Debug.LogError("No player unit found in the scene.");
        }

        if (introText == null)
        {
            Debug.LogError("Intro text is not assigned in the inspector.");
        }

        if (scoreText == null)
        {
            Debug.LogError("Score text is not assigned in the inspector.");
        }
        else
        {
            UpdateScoreText(); // Initialize score text
        }

        if (door == null)
        {
            Debug.LogError("Door object is not assigned in the inspector.");
        }

        if (winMessageText != null)
        {
            winMessageText.gameObject.SetActive(false); // Hide win message at start
        }
    }

    void Update()
    {
        if (introTextVisible)
        {
            HandleIntroText();
        }
        else
        {
            HandleGameplay();
        }
    }

    void HandleIntroText()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DisableIntroText();
        }
    }

    void DisableIntroText()
    {
        if (introText != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            introText.gameObject.SetActive(false);
            introTextVisible = false;
        }
    }

    void HandleGameplay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("NPC"))
                {
                    HandleNPCClick(hitInfo.collider.gameObject);
                }
                else if (hitInfo.collider.CompareTag("ground"))
                {
                    HandleGroundClick(hitInfo.point);
                }
                else
                {
                    ClearDialogue();
                }
            }
        }
    }

    void HandleNPCClick(GameObject npc)
    {
        if (playerUnit == null) return;

        float distance = Vector3.Distance(playerUnit.transform.position, npc.transform.position);

        if (distance <= interactionDistance)
        {
            DialogueScript dialogue = npc.GetComponent<DialogueScript>();
            if (dialogue != null)
            {
                if (!dialogue.HasBeenClicked) // Check if NPC is clicked for the first time
                {
                    dialogue.HasBeenClicked = true;
                    IncreaseScore(5); // Increase score by 5
                }
                dialogue.ShowDialogue();
                activeDialogue = dialogue;
            }
        }
    }

    void HandleGroundClick(Vector3 point)
    {
        if (playerUnit != null)
        {
            playerUnit.MoveToPosition(point);
            ClearDialogue();
        }
    }

    void ClearDialogue()
    {
        if (activeDialogue != null)
        {
            activeDialogue.HideDialogue();
            activeDialogue = null;
        }
    }

    void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();

        if (score >= 100)
        {
            RemoveDoor(); // Check and remove the door when score reaches 100
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Affinity: " + score; // Update the score display
        }
    }

    void RemoveDoor()
    {
        if (door != null && door.activeSelf)
        {
            Debug.Log("Score reached 100. Door is removed!");
            door.SetActive(false); // Disable the door object
            DisplayWinMessage(); // Show the win message
        }
    }

    void DisplayWinMessage()
    {
        if (winMessageText != null)
        {
            winMessageText.text = "You made it! Good job getting to know everyone and making some new friends. Time to explore!!";
            winMessageText.gameObject.SetActive(true); // Show the win message
        }
    }
}
