using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Asteroid : MonoBehaviour
{
    // Asteroid Type (Size and velocity)
    public int asteroidID = 1;

    // Public references
    public GameObject asteroidPrefab;
    public GameObject explosionParticles;

    // Private references
    private TP_GameManager gameManager;
    private TP_AudioManager audioManager;

    // Values
    private float velocity = 2f;

    // Random values
    private Quaternion spawnRotation;
    private float randomRotZ;

    // Trigger fix
    private bool isTriggering = false;

    private void Start()
    {
        // Gets references
        gameManager = FindObjectOfType<TP_GameManager>();
        audioManager = FindObjectOfType<TP_AudioManager>();

        // Transforms to asteroid type
        TransformBehaviour();
    }

    void Update()
    {
        // Moves forward
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If gets shot
        if (other.CompareTag("Shot"))
        {
            // Checks that only one shot is colliding (FIX)
            // Prevents it from executing more than once by exiting the function.
            if (isTriggering) return;
            isTriggering = true;

            // Spawns particles
            Instantiate(explosionParticles, transform.position, transform.rotation);

            // If is a type 1 or type 2 asteroid
            if (asteroidID == 1 || asteroidID == 2)
            {
                // Spawns 2 asteroids in his position with a random rotation
                for (int i = 0; i <= 1; i++)
                {
                    spawnRotation = RandomRotation();

                    GameObject asteroidSpawned = Instantiate(asteroidPrefab, transform.position, spawnRotation);

                    // Is this asteroids is Type 1, changes asteroid spawned to Type 2
                    if (asteroidID == 1)
                    {
                        asteroidSpawned.GetComponent<TP_Asteroid>().asteroidID = 2;
                    }

                    // Is this asteroids is Type 2, changes asteroid spawned to Type 3
                    if (asteroidID == 2)
                    {
                        asteroidSpawned.GetComponent<TP_Asteroid>().asteroidID = 3;
                    }
                }
            }

            // Updates score
            AsteroidScore();

            // Plays audio
            audioManager.PlayAudioScore();

            // Destroys each other, shot and asteroid
            Destroy(other.gameObject);
            Destroy(gameObject);

            // Calls to this function
            StartCoroutine(TriggerEnterOn());
        }
    }

    // Transforms the asteroid according to type
    private void TransformBehaviour()
    {
        // Type 1 (Big asteroid)
        if (asteroidID == 1)
        {
            transform.localScale = new Vector3(2, 2, 2);

            velocity = 2f;
        }

        // Type 2 (Medium asteroid)
        if (asteroidID == 2)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

            velocity = 2.5f;
        }

        // Type 3 (Small asteroid)
        if (asteroidID == 3)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            velocity = 3f;
        }
    }

    // When the asteroid is destroyed, scores according to type
    private void AsteroidScore()
    {
        // Type 1 (Big asteroid)
        if (asteroidID == 1)
        {
            gameManager.UpdateScore(250);
        }

        // Type 2 (Medium asteroid)
        if (asteroidID == 2)
        {
            gameManager.UpdateScore(100);
        }

        // Type 3 (Small asteroid)
        if (asteroidID == 3)
        {
            gameManager.UpdateScore(25);
        }
    }

    // Returns random rotation
    private Quaternion RandomRotation()
    {
        randomRotZ = Random.Range(0, 361);

        return Quaternion.Euler(0, 0, randomRotZ);
    }

    // Fix to only check a single collision of a shot
    private IEnumerator TriggerEnterOn()
    {
        yield return new WaitForEndOfFrame();
        isTriggering = false;
    }
}
