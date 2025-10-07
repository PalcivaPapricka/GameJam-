using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public LeaderboardManager lm;

    public void playGame()
    {
        SceneManager.LoadScene("Main");   
    }

    
    public void goToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void goToHighScoreMenu()
    {
        SceneManager.LoadScene("HighScoresMenu");
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
