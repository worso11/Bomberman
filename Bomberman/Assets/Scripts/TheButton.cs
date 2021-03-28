using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class responsible for button's behaviour
public class TheButton : MonoBehaviour
{
    // Function changing scene to Menu
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
