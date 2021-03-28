using UnityEngine;
using UnityEngine.SceneManagement;

// Class responsible for MainMenu's behaviour
public class MainMenu : MonoBehaviour
{
    // Function quitting the game
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    // Function changing scene to Highscores
    public void GoToHighscores()
    {
        SceneManager.LoadScene("Highscores");
    }
}
