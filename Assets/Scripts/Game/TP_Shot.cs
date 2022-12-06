using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Shot : MonoBehaviour
{
    public float forcePower = 3f;
    public float maxVelocity = 3f;

    private Rigidbody2D shotRigidbody;

    private float lifeTime = 2f;

    void Start()
    {
        shotRigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        shotRigidbody.AddForce(transform.up * forcePower,ForceMode2D.Impulse);

        if (shotRigidbody.velocity.magnitude > maxVelocity)
        {
            shotRigidbody.velocity = shotRigidbody.velocity.normalized * maxVelocity;
        }
    }
}
