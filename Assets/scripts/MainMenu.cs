using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void goToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
