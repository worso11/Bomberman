using TMPro;
using UnityEngine;

// Class responsible for loading best scores from file
// Have to be in main asset folder!
public class LoadingScores : MonoBehaviour
{
    public TextMeshProUGUI text_score;
    
    private string scr = "";
    
    // Function called on start
    void Start()
    {
        PlayerScores scores =  SaveScores.LoadScore();

        for (int i = 0; i < 5; i++)
        {
            float seconds = Mathf.FloorToInt(scores.time[i]);
            float milliSeconds = (scores.time[i] % 1) * 100;
            if (milliSeconds > 99.5)
            {
                seconds++;
                milliSeconds = 0;
            }
            
            scr += string.Format(scores.name[i] + ": {0:00}:{1:00}\n", seconds, milliSeconds);
        }

        text_score.text = scr;
    }
}
