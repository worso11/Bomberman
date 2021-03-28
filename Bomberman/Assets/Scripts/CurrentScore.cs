// Static class containing current player's score
public static class CurrentScore
{
    private static float totalTime;

    // Function for getting and setting value of totalTime
    public static float TotalTime
    {
        get
        {
            return totalTime;
        }
        set
        {
            totalTime = value;
        }
    }
}
