using UnityEngine;

public class GameSession : MonoBehaviour
{
    int currentScore = 0;

    //--------------------------------------------------------------------------
    // Singleton
    //--------------------------------------------------------------------------
    void Awake()
    {
        int numSessions = FindObjectsOfType<GameSession>().Length;
        if (numSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    //--------------------------------------------------------------------------
    public void ResetGame()
    {
        Destroy(gameObject);
    }

    //--------------------------------------------------------------------------
    public int GetScore()
    {
        return currentScore;
    }

    //--------------------------------------------------------------------------
    public void AddPoints(int points)
    {
        currentScore += points;
    }

    //--------------------------------------------------------------------------
}
