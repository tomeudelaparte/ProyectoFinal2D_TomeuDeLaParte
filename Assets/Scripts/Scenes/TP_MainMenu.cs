using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_MainMenu : MonoBehaviour
{
    public GameObject asteroidPrefab;

    private int asteroidsCount = 20;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private float randomPosX, randomPosY;
    private float randomRotZ;

    private float maxRangeX = 9f;
    private float minRangeX = 0f;

    private float maxRangeY = 5f;
    private float minRangeY = 0f;

    private void Start()
    {
        Time.timeScale = 1;

        QualitySettings.vSyncCount = 1;

        for (int i = 0; i < asteroidsCount; i++)
        {
            if (i < 2)
            {
                SpawnAsteroid(1);
            }
            else if (i < 10)
            {
                SpawnAsteroid(2);

            }
            else if (i < asteroidsCount)
            {
                SpawnAsteroid(3);
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

    private void SpawnAsteroid(int asteroidID)
    {
        spawnPosition = RandomPosition();
        spawnRotation = RandomRotation();

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, spawnRotation);

        asteroid.GetComponent<TP_Asteroid>().asteroidID = asteroidID;
    }
}
