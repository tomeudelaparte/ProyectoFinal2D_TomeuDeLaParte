using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Player : MonoBehaviour
{
    [SerializeField] private float forcePower = 4f;
    [SerializeField] private float maxVelocity = 3f;
    [SerializeField] private Vector3 torquePower = new Vector3(0, 0, 5);

    public GameObject shotPrefab;
    private bool shootTrigger = true;
    private float shootCooldown = 0.15f;

    private Rigidbody2D playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerRigidbody.AddForce(transform.up * forcePower);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.rotation * torquePower);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.rotation * -torquePower);
        }

        if (playerRigidbody.velocity.magnitude > maxVelocity)
        {
            playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxVelocity;
        }
    }
    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && shootTrigger)
        {
            Instantiate(shotPrefab, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);

            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }
}