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

    private bool canPlay;

    private Rigidbody2D playerRigidbody;
    private GameManager gameManager;

    private bool isBlastColliding = false;

    void Start()
    {
        canPlay = true;

        playerRigidbody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
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
        if (canPlay)
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
    }
    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && shootTrigger && canPlay)
        {
            GameObject shot = Instantiate(shotPrefab, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);

            shot.GetComponent<TP_Shot>().maxVelocity = shot.GetComponent<TP_Shot>().forcePower + playerRigidbody.velocity.magnitude * 1.5f;

            StartCoroutine(ShootCooldown());
        }
    }

    private IEnumerator ShootCooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            if (isBlastColliding) return;
            isBlastColliding = true;

            gameManager.UpdateLives();

            canPlay = false;
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(0, 0, 1);
            gameObject.SetActive(true);
            canPlay = true;

            StartCoroutine(TriggerEnterOn());
        }
    }

    private IEnumerator TriggerEnterOn()
    {
        yield return new WaitForEndOfFrame();
        isBlastColliding = false;
    }
}