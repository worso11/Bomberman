// Serializable class for keeping 5 best scores
[System.Serializable]
public class PlayerScores
{
    public string[] name = new string [5];
    public float[] time = new float[5];

    // Constructor without parameters
    public PlayerScores()
    {
        for (int i = 0; i < 5; i++)
        {
            name[i] = "Default";
            time[i] = 0;
        }
        
    }
    
    // Constructor with parameters
    public PlayerScores(float[] t, string[] n)
    {
        time = t;
        name = n;
    }
}
