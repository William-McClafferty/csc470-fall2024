using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TMP_Text dialogueText; // TMP_Text for displaying dialogue
    [TextArea] public string dialogue; // Dialogue text for the NPC
    public bool HasBeenClicked = false; // Track if NPC has been clicked

    void Start()
    {
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false); // Hide dialogue at start
        }
    }

    public void ShowDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.text = dialogue;
            dialogueText.gameObject.SetActive(true);
        }
    }

    public void HideDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }
    }
}
