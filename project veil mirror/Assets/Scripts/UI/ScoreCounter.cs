// ScoreCounter.cs
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnScoreChanged))]
    public int scoreValue = 0;

    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + scoreValue;
    }

    void OnScoreChanged(int oldValue, int newValue)
    {
        UpdateScoreText();
    }
}
