using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Shot : MonoBehaviour
{

    public float forcePower = 3f;

    public float maxVelocity = 3f;
    [SerializeField] private float lifeTime = 2f;

    public Rigidbody2D shotRigidbody;

    private void Awake()
    {
        shotRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
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
