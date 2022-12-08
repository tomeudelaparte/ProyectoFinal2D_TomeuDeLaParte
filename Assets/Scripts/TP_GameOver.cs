using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TP_GameOver : MonoBehaviour
{
    // Public references
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    // Private references
    private TP_DataPersistence dataPersistence;

    // Store values
    private int currentScore, highScore;

    void Start()
    {
        // Gets references
        dataPersistence = FindObjectOfType<TP_DataPersistence>();

        // Unpauses the game
        Time.timeScale = 1;

        // Shows the cursor
        Cursor.visible = true;

        // Sets current score and high score
        CurrentScoreFunction();
        HighScoreFunction();
    }

    // Gets and show the current score
    private void CurrentScoreFunction()
    {
        currentScore = dataPersistence.GetInt("CURRENT SCORE");

        currentScoreText.text = "CURRENT SCORE: " + currentScore.ToString();
    }

    // Gets and show the high score
    private void HighScoreFunction()
    {
        // If it exists
        if (dataPersistence.HasKey("HIGH SCORE"))
        {
            // Gets the score
            highScore = dataPersistence.GetInt("HIGH SCORE");

            // Shors the score
            highScoreText.text = "HIGH SCORE: " + highScore.ToString();

            // If the current score if greater than the high score
            if (currentScore > highScore)
            {
                // Saves current score as high score
                dataPersistence.SetInt("HIGH SCORE", currentScore);
            }
        }

        // If not exists
        else
        {
            // Saves 0 as high score
            dataPersistence.SetInt("HIGH SCORE", 0);

            // Call this same function again
            HighScoreFunction();
        }
    }
}
