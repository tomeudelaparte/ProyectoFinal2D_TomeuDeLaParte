using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject asteroidPrefab;

    public TextMeshProUGUI scoreNumber;

    public GameObject[] livesSprites;

    private int totalScore;
    private int totalLives;

    private int asteroidsCount = 4;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private float randomPosX, randomPosY;
    private float randomRotZ;

    private float xRange = 9f;
    private float yRange = 5f;

    private void Awake()
    {
        totalScore = 0;
        totalLives = 3;

        for (int i = 0; i < asteroidsCount; i++)
        {
            SpawnAsteroid();
        }
    }


    private void Update()
    {
        int asteroidsInScene = GameObject.FindGameObjectsWithTag("Asteroid").Length;

        if (asteroidsInScene <= 0)
        {
            asteroidsCount++;

            for (int i = 0; i < asteroidsCount; i++)
            {
                SpawnAsteroid();
            }
        }
    }

    private Vector3 RandomPosition()
    {
        randomPosX = Random.Range(-xRange, xRange);
        randomPosY = Random.Range(-yRange, yRange);

        return new Vector3(randomPosX, randomPosY, 0);
    }

    private Quaternion RandomRotation()
    {
        randomRotZ = Random.Range(0, 361);

        return Quaternion.Euler(0, 0, randomRotZ);
    }

    private void SpawnAsteroid()
    {
        spawnPosition = RandomPosition();
        spawnRotation = RandomRotation();

        Instantiate(asteroidPrefab, spawnPosition, spawnRotation);
    }

    public void UpdateScore(int value)
    {
        totalScore += value;

        scoreNumber.text = (totalScore).ToString();
    }

    public void UpdateLives()
    {
        totalLives -= 1;

        livesSprites[totalLives].SetActive(false);
    }
}
