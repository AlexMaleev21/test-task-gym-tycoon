using UnityEngine;
using UnityEngine.SceneManagement;

/* handles transition from main menu to main scene and ext */
public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Если игра собрана, выйдем
        Application.Quit();
#endif
    }
}

