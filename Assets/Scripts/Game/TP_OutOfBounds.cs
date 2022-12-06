using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_OutOfBounds : MonoBehaviour
{
    private float xBound = 10f;
    private float yBound = 6f;
    private float zBound = 1;

    private void FixedUpdate()
    {
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, zBound);
        }

        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, zBound);
        }

        if (transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, zBound);
        }

        if (transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, zBound);
        }
    }
}
