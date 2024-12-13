using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    public void LoadTitleScreen()
    {
        Debug.Log("LoadTitleScreen called");
        SceneManager.LoadScene("TitleScreen");
    }
}
