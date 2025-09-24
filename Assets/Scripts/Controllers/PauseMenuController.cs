using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Handles pause menu manipulations */
public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseUI;
    public Button pauseButton;
    public Button resumeButton;
    public Button mainMenuButton;
    private bool isPaused = false;


    private void OnEnable()
    {
        pauseButton.onClick.AddListener(TogglePauseMenu);
        resumeButton.onClick.AddListener(Resume);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void OnDisable()
    {
        pauseButton.onClick.RemoveAllListeners();
        resumeButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }

    /* Handle pause-resume operation */
    public void TogglePauseMenu()
    {
        if (isPaused) Resume();
        else Pause();
    }

    /* Resumes game and hides menu */
    public void Resume()
    {
        pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1f;   
        isPaused = false;
    }

    /* Shows menu */
    public void Pause()
    {
        pauseUI.gameObject.SetActive(true); 
        isPaused = true;
    }

    /* Loads main menu scene */
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

