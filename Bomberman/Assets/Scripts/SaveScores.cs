using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// Static class responsible for saving/loading scores to/from the file
public static class SaveScores
{
    // Function saving score
    public static void SaveScore(float t, string n)
    {
        PlayerScores oldScores = LoadScore();
        Debug.Log("Rozpoczynam zapis");
        string tmp_name;
        float tmp_time;

        for (int i = 0; i < 5; i++)
        {
            if (Mathf.Approximately(t, oldScores.time[i]) && n == oldScores.name[i]) { break; }

            if (t > oldScores.time[i])
            {
                Debug.Log("Lepszy od pozycji" + i);
                tmp_name = oldScores.name[i];
                tmp_time = oldScores.time[i];
                oldScores.name[i] = n;
                oldScores.time[i] = t;
                n = tmp_name;
                t = tmp_time;
            }
        }
        
        
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "highscoresv2.txt";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerScores scores = new PlayerScores(oldScores.time, oldScores.name);
        
        formatter.Serialize(stream, scores);
        stream.Close();
    }

    // Function loading score
    public static PlayerScores LoadScore()
    {
        string path = Application.persistentDataPath + "highscoresv2.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerScores scores = formatter.Deserialize(stream) as PlayerScores;
            stream.Close();
            
            return scores;
        }
        else
        {
            Debug.Log("Nie ma takiego pliku");
            PlayerScores scores = new PlayerScores();
            return scores;
        }
    }
}
