using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TP_GameOver : MonoBehaviour
{
    public TextMeshProUGUI score;

    private DataPersistence dataPersistence;

    void Start()
    {
        dataPersistence = FindObjectOfType<DataPersistence>();

        score.text = dataPersistence.GetString("SCORE");
    }
}
