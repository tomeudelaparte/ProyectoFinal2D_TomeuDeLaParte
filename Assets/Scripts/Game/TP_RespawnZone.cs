using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_RespawnZone : MonoBehaviour
{
    public bool isSafe = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            isSafe = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            isSafe = true;
        }
    }
}
