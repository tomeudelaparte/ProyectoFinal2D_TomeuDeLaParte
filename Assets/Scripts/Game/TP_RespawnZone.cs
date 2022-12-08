using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_RespawnZone : MonoBehaviour
{
    // Values
    public bool isSafe = false;

    // When asteoroids are in, not safe to spawn
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            isSafe = false;
        }
    }

    // When asteoroids are out, is safe to spawn
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            isSafe = true;
        }
    }
}
