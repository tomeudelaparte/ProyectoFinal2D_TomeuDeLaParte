using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_OutOfBounds : MonoBehaviour
{
    // Values
    private float xBound = 10f;
    private float yBound = 6f;
    private float zBound = 1;

    private void FixedUpdate()
    {
        // When position its over +X
        if (transform.position.x > xBound)
        {
            // Move position to -X
            transform.position = new Vector3(-xBound, transform.position.y, zBound);
        }

        // When position its under -X
        if (transform.position.x < -xBound)
        {
            // Move position to +X
            transform.position = new Vector3(xBound, transform.position.y, zBound);
        }

        // When position its over +Y
        if (transform.position.y > yBound)
        {
            // Move position to -Y
            transform.position = new Vector3(transform.position.x, -yBound, zBound);
        }

        // When position its under -Y
        if (transform.position.y < -yBound)
        {
            // Move position to +Y
            transform.position = new Vector3(transform.position.x, yBound, zBound);
        }
    }
}
