using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Shot : MonoBehaviour
{
    // Values
    public float forcePower = 3f;
    public float maxVelocity = 3f;

    // Private references
    private Rigidbody2D shotRigidbody;

    // Private values
    private float lifeTime = 2f;

    void Start()
    {
        // Gets references
        shotRigidbody = GetComponent<Rigidbody2D>();

        // Autodestroys after time
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        // Adds force forward
        shotRigidbody.AddForce(transform.up * forcePower,ForceMode2D.Impulse);

        // Limits the velocity
        if (shotRigidbody.velocity.magnitude > maxVelocity)
        {
            shotRigidbody.velocity = shotRigidbody.velocity.normalized * maxVelocity;
        }
    }
}
