using System.Collections;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int extraLifeAfterScore = 10000;
    [SerializeField] int initialLives = 3;
    int currentScore = 0;
    int extraLifeCounter = 0;
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
        ResetGame();
    }

    //--------------------------------------------------------------------------
    public void ResetGame()
    {
        currentScore = 0;
        currentLives = initialLives;
        extraLifeCounter = 0;
    }

    //--------------------------------------------------------------------------
    public int GetScore()
    {
        return currentScore;
    }

    //--------------------------------------------------------------------------
    public int AddPoints(int points)
    {
        currentScore += points;

        // Player gets an extra life bonus after reaching a score
        if (currentScore >= ((extraLifeCounter + 1) * extraLifeAfterScore))
        {
            extraLifeCounter++;
            int numLifes = IncrementLivesLeft();
            FindObjectOfType<LivesDisplay>()?.SetNumLives(numLifes);
        }

        return currentScore;
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
        if (currentLives > 0)
        {
            --currentLives;
        }
        return currentLives;
    }

    //--------------------------------------------------------------------------
    public int IncrementLivesLeft()
    {
        return ++currentLives;
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
