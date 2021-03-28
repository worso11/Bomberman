using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.UI;
public class RandomBombs : MonoBehaviour
{
    public GameObject bomb;
    public int time;
    public Text countdown;
    public float totalTime = 0;
    public Text timer;
    
    private bool gameStarted = false;
    
    // Function called on start
    void Start()
    {
        StartCoroutine(Countdown());
    }

    // Function called once per frame responsible for updating timer
    void Update()
    {
        UpdateTimer();
    }

    // Function for updating timer
    void UpdateTimer()
    {
        if (gameStarted)
        {
            totalTime += Time.deltaTime;
            //float minutes = Mathf.FloorToInt(totalTime / 60);  
            float seconds = Mathf.FloorToInt(totalTime);
            float milliSeconds = (totalTime % 1) * 100;
            Debug.Log(milliSeconds);
            if (milliSeconds > 99.5)
            {
                seconds++;
                milliSeconds = 0;
            }
            Debug.Log(milliSeconds);
            
            timer.text = string.Format("Time: {0:00}:{1:00}", seconds, milliSeconds);
        }
    }
    
    // Function for countdown
    private IEnumerator Countdown()
    {
        for (int t = time; t > 0; t--)
        {
            countdown.text = t.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdown.text = "GO!";
        yield return new WaitForSeconds(1f);
        gameStarted = true;
        StartCoroutine(RainOfBombs());
        countdown.gameObject.SetActive(false);
    }

    // Function responsible for spawning bombs in random locations
    private IEnumerator RainOfBombs()
    {
        Vector2 start = new Vector2(-9, -3);
        int x;
        int y;
        while (true)
        {
            x = Random.Range(0,18);
            y = Random.Range(0,7);
            GameObject newBomb =  Instantiate(bomb, start + Vector2.up*y + Vector2.right*x, Quaternion.identity);
            newBomb.GetComponent<Bomb>().Boom(4);
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Function changing scene to Menu
    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Function to save the score (time) in static class
    public void OnDestroy()
    {
        CurrentScore.TotalTime = totalTime;
    }
}
