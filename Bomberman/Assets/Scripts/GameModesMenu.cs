using UnityEngine;
using UnityEngine.SceneManagement;

// Class responsible for GameModesMenu's behaviour
public class GameModesMenu : MonoBehaviour
{
    // Function changing scene to MultiGame
    public void PlayMultiPlayer()
    {
        SceneManager.LoadScene("MultiGame");
    }

}