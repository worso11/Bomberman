using UnityEngine;
using UnityEngine.SceneManagement;

// Class responsible for LostEndlessMenu's behaviour
public class LostEndlessMenu : MonoBehaviour
{
    // Function changing scene to RainOfBombs
    public void PlayAgain()
    {
        SceneManager.LoadScene("RainOfBombs");
    }
    
    // Function changing scene to Menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}


