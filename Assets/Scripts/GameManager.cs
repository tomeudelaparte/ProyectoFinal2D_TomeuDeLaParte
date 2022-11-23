using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject asteroidPrefab;

    private int asteroidsCount = 4;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private float randomPosX, randomPosY;
    private float randomRotZ;

    private float xRange = 9f;
    private float yRange = 5f;

    private void Start()
    {
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

    public void SpawnAsteroidDestruction(Vector3 asteroidPosition, int type)
    {
        spawnRotation = RandomRotation();

        GameObject asteroidSpawned = Instantiate(asteroidPrefab, asteroidPosition, spawnRotation);

        if (type == 1)
        {
            asteroidSpawned.transform.localScale = new Vector3(1, 1, 1);

            asteroidSpawned.GetComponent<TP_Asteroid>().type = 2;
            asteroidSpawned.GetComponent<TP_Asteroid>().speed = 2.5f;
        }

        if (type == 2)
        {
            asteroidSpawned.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            asteroidSpawned.GetComponent<TP_Asteroid>().type = 3;
            asteroidSpawned.GetComponent<TP_Asteroid>().speed = 3f;
        }
    }
}
