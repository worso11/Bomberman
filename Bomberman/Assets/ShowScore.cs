using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Class responsible for showing score after game
// Have to be in main asset folder!
public class ShowScore : MonoBehaviour
{
    public TextMeshProUGUI score;
    public InputField nick;

    private float time;
    
    // Client's function called on start
    void Start()
    {
        time = CurrentScore.TotalTime;
        float seconds = Mathf.FloorToInt(time);
        float milliSeconds = (time % 1) * 100;
        if (milliSeconds > 99.5)
        {
            seconds++;
            milliSeconds = 0;
        }
        
        score.text = string.Format("YOU LOST!\nSCORE: {0:00}:{1:00}", seconds, milliSeconds);
    }

    // Function to save score to the file
    public void Save()
    {
        SaveScores.SaveScore(CurrentScore.TotalTime, nick.text);
    }
}
