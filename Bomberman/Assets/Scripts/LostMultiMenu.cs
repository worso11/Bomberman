using UnityEngine;
using UnityEngine.SceneManagement;

// Class responsible for LostMultiMenu's behaviour
public class LostMultiMenu : MonoBehaviour
{

    // Function changing scene to MultiGame
    public void PlayAgain()
    {
        SceneManager.LoadScene("MultiGame");
    }
    
    // Function changing scene to Menu
    public void Go2Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
