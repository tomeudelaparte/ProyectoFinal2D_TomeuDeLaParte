using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_MainMenu : MonoBehaviour
{
    // Public references
    public GameObject asteroidPrefab;

    // Important values
    private int totalAsteroids = 20;

    // Screen bounds
    private float maxRangeX = 9f;
    private float minRangeX = 0f;

    private float maxRangeY = 5f;
    private float minRangeY = 0f;

    // Random values
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private float randomPosX, randomPosY;
    private float randomRotZ;

    private void Start()
    {
        // Unpauses the game
        Time.timeScale = 1;

        // Shows the cursor
        Cursor.visible = true;

        // Vertical sync activated
        QualitySettings.vSyncCount = 1;

        // Spawns 10% of the total number of big asteroids
        for (int i = 0; i < ((totalAsteroids * 10) / 100) ; i++)
        {
            SpawnAsteroid(1);
        }

        // Spawns 40% of the total number of medium asteroids
        for (int i = 0; i < ((totalAsteroids * 40) / 100); i++)
        {
            SpawnAsteroid(2);
        }

        // Spawns 50% of the total number of small asteroids.
        for (int i = 0; i < ((totalAsteroids * 50) / 100); i++)
        {
            SpawnAsteroid(3);
        }
    }

    // Returns a random position 
    private Vector3 RandomPosition()
    {
        // Random choose between 0 and 1
        int side = Random.Range(0, 2);

        // Returns a random position with +X and -Y
        if (side == 0)
        {
            randomPosX = Random.Range(minRangeX, maxRangeX);
            randomPosY = Random.Range(-minRangeY, -maxRangeY);

        }

        // Returns a random position with -X and +Y
        else
        {
            randomPosX = Random.Range(-minRangeX, -maxRangeX);
            randomPosY = Random.Range(minRangeY, maxRangeY);
        }

        return new Vector3(randomPosX, randomPosY, 1);
    }

    // Returns a random rotation in Z
    private Quaternion RandomRotation()
    {
        // Random value between 0 and 360
        randomRotZ = Random.Range(0, 361);

        return Quaternion.Euler(0, 0, randomRotZ);
    }

    // Spawns an asteroid with a random position and rotation with ID Size Type.
    private void SpawnAsteroid(int asteroidID)
    {
        spawnPosition = RandomPosition();
        spawnRotation = RandomRotation();

        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, spawnRotation);

        asteroid.GetComponent<TP_Asteroid>().asteroidID = asteroidID;
    }
}
