using UnityEngine;
using UnityEngine.SceneManagement;

// Class responsible for SingleModeMenu's behaviour
public class SingleModeMenu : MonoBehaviour
{
    // Function changing scene to RainOfBombs
    public void PlayEndlessMode()
    {
        SceneManager.LoadScene("RainOfBombs");
    }
    
    // Function changing scene to Game
    public void ChooseSingleLevel()
    {
        SceneManager.LoadScene("Game");
    }

}
