using UnityEngine;
using UnityEngine.SceneManagement;

// Class responsible for LostSingleMenu's behaviour
public class LostSingleMenu : MonoBehaviour
{
    // Function changing scene to Game
    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
    
    // Function changing scene to Menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

