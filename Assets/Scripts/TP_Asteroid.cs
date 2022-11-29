using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Asteroid : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject shotParticles;

    public int size = 1;
    public float velocity = 2f;

    private GameManager gameManager;

    private Quaternion spawnRotation;
    private float randomRotZ;

    void Update()
    {
        gameManager = FindObjectOfType<GameManager>();

        transform.Translate(Vector3.up * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot"))
        {
            Instantiate(shotParticles, transform.position, transform.rotation);

            if (size == 1 || size == 2)
            {
                for (int i = 0; i <= 1; i++)
                {
                    spawnRotation = RandomRotation();

                    GameObject asteroidSpawned = Instantiate(asteroidPrefab, transform.position, spawnRotation);

                    if (size == 1)
                    {
                        asteroidSpawned.transform.localScale = new Vector3(1, 1, 1);

                        asteroidSpawned.GetComponent<TP_Asteroid>().size = 2;
                        asteroidSpawned.GetComponent<TP_Asteroid>().velocity = 2.5f;
                    }

                    if (size == 2)
                    {
                        asteroidSpawned.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                        asteroidSpawned.GetComponent<TP_Asteroid>().size = 3;
                        asteroidSpawned.GetComponent<TP_Asteroid>().velocity = 3f;
                    }
                }
            }

            if (size == 1)
            {
                gameManager.UpdateScore(250);
            }

            if (size == 2)
            {
                gameManager.UpdateScore(100);
            }

            if (size == 3)
            {
                gameManager.UpdateScore(25);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private Quaternion RandomRotation()
    {
        randomRotZ = Random.Range(0, 361);

        return Quaternion.Euler(0, 0, randomRotZ);
    }
}
