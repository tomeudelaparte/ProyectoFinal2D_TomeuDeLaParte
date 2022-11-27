using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject asteroidPrefab;

    public GameObject score;

    public GameObject[] livesSprites;

    private int totalScore;
    private int totalLives;

    private TextMeshProUGUI scoreNumber;
    private Animation scoreAnimation;

    private int asteroidsCount = 4;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private float randomPosX, randomPosY;
    private float randomRotZ;

    private float maxRangeX = 9f;
    private float minRangeX = 8f;

    private float maxRangeY = 5f;
    private float minRangeY = 4f;

    private void Awake()
    {
        scoreNumber = score.GetComponent<TextMeshProUGUI>();
        scoreAnimation = score.GetComponent<Animation>();

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
        int side = Random.Range(0, 2);

        if (side == 0)
        {
            randomPosX = Random.Range(minRangeX, maxRangeX);
            randomPosY = Random.Range(-minRangeY, -maxRangeY);

        }
        else
        {
            randomPosX = Random.Range(-minRangeX, -maxRangeX);
            randomPosY = Random.Range(minRangeY, maxRangeY);
        }

        return new Vector3(randomPosX, randomPosY, 1);
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

        if (totalLives <= 0)
        {

        }
        else
        {
            livesSprites[totalLives].SetActive(false);
        }
    }
}
