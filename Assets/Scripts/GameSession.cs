using System.Collections;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int initialLives = 3;
    int currentScore = 0;
    int currentLives;

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
    private void Start()
    {
        currentLives = initialLives;
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
    public int GetInitialLives()
    {
        return initialLives;
    }

    //--------------------------------------------------------------------------
    public int GetLivesLeft()
    {
        return currentLives;
    }

    //--------------------------------------------------------------------------
    public int DecrementLivesLeft()
    {
        if (currentScore > 0)
        {
            --currentLives;
        }
        return currentLives;
    }

    //--------------------------------------------------------------------------
    // Activate a inactive object after a delay
    //--------------------------------------------------------------------------
    public void WaitThenActivate(float delayS, GameObject objectToActivate)
    {
        StartCoroutine(WaitThenActivateImpl(delayS, objectToActivate));
    }

    //--------------------------------------------------------------------------
    IEnumerator WaitThenActivateImpl(float delayS, GameObject objectToActivate)
    {
        yield return new WaitForSeconds(delayS);
        objectToActivate.SetActive(true);
    }

    //--------------------------------------------------------------------------
}
