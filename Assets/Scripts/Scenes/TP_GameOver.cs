using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TP_GameOver : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    private int currentScore, highScore;

    private TP_DataPersistence dataPersistence;

    void Start()
    {
        Time.timeScale = 1;

        Cursor.visible = true;

        dataPersistence = FindObjectOfType<TP_DataPersistence>();

        currentScore = dataPersistence.GetInt("CURRENT SCORE");

        currentScoreText.text = "CURRENT SCORE: " + currentScore.ToString();

        if (dataPersistence.HasKey("HIGH SCORE"))
        {
            highScore = dataPersistence.GetInt("HIGH SCORE");

            highScoreText.text = "HIGH SCORE: " + highScore.ToString();

            if (currentScore > highScore)
            {
                dataPersistence.SetInt("HIGH SCORE", currentScore);
            }
        }
        else
        {
            dataPersistence.SetInt("HIGH SCORE", 0);
        }
    }
}
