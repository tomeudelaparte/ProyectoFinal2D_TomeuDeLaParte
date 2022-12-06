using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Asteroid : MonoBehaviour
{
    public int asteroidID = 1;

    public GameObject asteroidPrefab;
    public GameObject explosionParticles;

    private TP_GameManager gameManager;
    private TP_AudioManager audioManager;

    private float velocity = 2f;

    private Quaternion spawnRotation;
    private float randomRotZ;

    private bool isTriggering = false;

    private void Start()
    {
        gameManager = FindObjectOfType<TP_GameManager>();
        audioManager = FindObjectOfType<TP_AudioManager>();

        TransformBehaviour();
    }

    void Update()
    {
        transform.Translate(Vector3.up * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot"))
        {
            if (isTriggering) return;
            isTriggering = true;

            Instantiate(explosionParticles, transform.position, transform.rotation);

            if (asteroidID == 1 || asteroidID == 2)
            {
                for (int i = 0; i <= 1; i++)
                {
                    spawnRotation = RandomRotation();

                    GameObject asteroidSpawned = Instantiate(asteroidPrefab, transform.position, spawnRotation);

                    if (asteroidID == 1)
                    {
                        asteroidSpawned.GetComponent<TP_Asteroid>().asteroidID = 2;
                    }

                    if (asteroidID == 2)
                    {
                        asteroidSpawned.GetComponent<TP_Asteroid>().asteroidID = 3;
                    }
                }
            }

            AsteroidScore();

            audioManager.PlayAudioScore();

            Destroy(other.gameObject);
            Destroy(gameObject);

            StartCoroutine(TriggerEnterOn());
        }
    }

    private void TransformBehaviour()
    {
        if (asteroidID == 1)
        {
            transform.localScale = new Vector3(2, 2, 2);

            velocity = 2f;
        }

        if (asteroidID == 2)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

            velocity = 2.5f;
        }

        if (asteroidID == 3)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            velocity = 3f;
        }
    }

    private void AsteroidScore()
    {
        if (asteroidID == 1)
        {
            gameManager.UpdateScore(250);
        }

        if (asteroidID == 2)
        {
            gameManager.UpdateScore(100);
        }

        if (asteroidID == 3)
        {
            gameManager.UpdateScore(25);
        }
    }

    private Quaternion RandomRotation()
    {
        randomRotZ = Random.Range(0, 361);

        return Quaternion.Euler(0, 0, randomRotZ);
    }

    private IEnumerator TriggerEnterOn()
    {
        yield return new WaitForEndOfFrame();
        isTriggering = false;
    }
}
