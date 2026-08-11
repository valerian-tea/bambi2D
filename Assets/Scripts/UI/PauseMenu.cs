using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    private CanvasGroup canvasGroup;

    void Start()
    {
        if (pauseMenuUI != null)
        {
            canvasGroup = pauseMenuUI.GetComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        if (gameIsPaused)
        {
            canvasGroup.alpha = 1f; // Show the menu
            canvasGroup.interactable = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            canvasGroup.alpha = 0f; // Hide the menu
            canvasGroup.interactable = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnPause()
    {
        gameIsPaused = !gameIsPaused;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit(); // Works in the final built version
    }
}
