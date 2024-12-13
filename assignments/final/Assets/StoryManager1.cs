using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryManager1 : MonoBehaviour
{
    [Header("UI Elements")]
    public Canvas storyCanvas; // Reference to the Canvas
    public TextMeshProUGUI storyText; // Reference to the TMP text
    public Image storyImage; // Reference to the Image

    private bool isStoryActive = true; // Tracks if the story is active

    void Start()
    {
        // Ensure the canvas and story elements are visible when the game starts
        if (storyCanvas != null)
        {
            storyCanvas.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // Check if the story is active and the spacebar is pressed
        if (isStoryActive && Input.GetKeyDown(KeyCode.Space))
        {
            // Hide the story elements
            HideStoryElements();
        }
    }

    void HideStoryElements()
    {
        if (storyText != null)
        {
            storyText.gameObject.SetActive(false);
        }

        if (storyImage != null)
        {
            storyImage.gameObject.SetActive(false);
        }

        if (storyCanvas != null)
        {
            storyCanvas.gameObject.SetActive(false);
        }

        // Deactivate story mode
        isStoryActive = false;
    }
}
