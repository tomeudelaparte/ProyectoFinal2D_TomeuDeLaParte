using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Asteroid : MonoBehaviour
{
    public int type = 1;
    public float speed = 2f;

    private GameManager gameManager;



    void Update()
    {
        gameManager = FindObjectOfType<GameManager>();

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot"))
        {
            if (type == 1 || type == 2)
            {
                for (int i = 0; i <= 1; i++)
                {
                    gameManager.SpawnAsteroidDestruction(transform.position, type);
                }
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
