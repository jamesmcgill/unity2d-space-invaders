using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameSession gameSession;

    //--------------------------------------------------------------------------
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    //--------------------------------------------------------------------------
    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString();
    }

    //--------------------------------------------------------------------------
}
