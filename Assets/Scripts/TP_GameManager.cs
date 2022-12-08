using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TP_GameManager : MonoBehaviour
{
    // Public references
    public GameObject asteroidPrefab;
    public GameObject scoreText;
    public Animator scoreAnimator;
    public GameObject[] livesSprites;

    // Private references
    private TP_PlayerController playerController;
    private TP_DataPersistence dataPersistence;
    private TextMeshProUGUI scoreNumber;

    // Important values
    private int totalAsteroids = 4;

    private int totalScore = 0;
    private int totalLives = 3;

    // Screen bounds
    private float maxRangeX = 9f;
    private float minRangeX = 8f;

    private float maxRangeY = 5f;
    private float minRangeY = 4f;

    // Random values
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    private float randomPosX, randomPosY;
    private float randomRotZ;

    private void Start()
    {
        // Gets references
        dataPersistence = FindObjectOfType<TP_DataPersistence>();
        playerController = FindObjectOfType<TP_PlayerController>();

        scoreNumber = scoreText.GetComponent<TextMeshProUGUI>();

        // Unpauses the game
        Time.timeScale = 1;

        // Hides the cursor
        Cursor.visible = false;
    }

    private void Update()
    {
        // Check the remaining asteroids and spawns a new round
        SpawnRound();
    }

    // Spawns a round of asteroids
    private void SpawnRound()
    {
        // Gets the number of remaining asteroids
        int asteroidsInScene = GameObject.FindGameObjectsWithTag("Asteroid").Length;

        // If the number is less than or equal to 0
        if (asteroidsInScene <= 0)
        {
            // Repositions the player and remove all player shots
            playerController.RespawnPlayerRound();

            // Adds +1 to total of asteroids
            totalAsteroids++;

            // Spawns a number of times according to the total number of asteroids
            for (int i = 0; i < totalAsteroids; i++)
            {
                SpawnAsteroid();
            }
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

    // Spawns an asteroid with a random position and rotation.
    private void SpawnAsteroid()
    {
        spawnPosition = RandomPosition();
        spawnRotation = RandomRotation();

        Instantiate(asteroidPrefab, spawnPosition, spawnRotation);
    }

    // Updates game score
    public void UpdateScore(int value)
    {
        // Updates the total score
        totalScore += value;

        scoreNumber.text = (totalScore).ToString();

        // Plays the animation
        scoreAnimator.Play("ScoreUpdate");
    }

    // Updates lives remaining
    public void UpdateLives()
    {
        // Subtract -1 from the total
        totalLives--;

        // If the total lives are less than or equal to 0
        if (totalLives <= 0)
        {
            // Saves current score in Player Prefs
            dataPersistence.SetInt("CURRENT SCORE", totalScore);

            // Loads the game over scene
            SceneManager.LoadScene("TP_GameOver");
        }
        else
        {
            // Execute the animation
            StartCoroutine(LivesAnimation());
        }
    }

    // Animation to show when the player loose a life
    private IEnumerator LivesAnimation()
    {
        // Plays the animation and sets active to false
        livesSprites[totalLives].GetComponent<Animator>().Play("LivesUpdate");
        yield return new WaitForSeconds(0.5f);
        livesSprites[totalLives].SetActive(false);
    }
}
