using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (gameIsPaused)
        {
            Debug.Log("Pause state YES");
            pauseMenuUI.SetActive(true); // Show the menu
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.Log("Pause state NO");
            pauseMenuUI.SetActive(false); // Show the menu
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
