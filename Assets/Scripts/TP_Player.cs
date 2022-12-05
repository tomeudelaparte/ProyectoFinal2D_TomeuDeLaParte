using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Player : MonoBehaviour
{
    [SerializeField] private float forcePower = 4f;
    [SerializeField] private float maxVelocity = 3f;
    [SerializeField] private Vector3 torquePower = new Vector3(0, 0, 5);

    public GameObject shotPrefab;
    public ParticleSystem propulse;

    private bool shootTrigger = true;
    private float shootCooldown = 0.15f;

    private Rigidbody2D playerRigidbody;
    private GameManager gameManager;

    private bool isBlastColliding = false;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();

        propulse.Stop();
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

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            playerRigidbody.AddForce(transform.up * forcePower);

            if (propulse.isStopped)
            {
                propulse.Play();
            }
        }
        else
        {
            if (propulse.isPlaying)
            {
                propulse.Stop();

                playerRigidbody.velocity *= new Vector2(0.96f,0.96f);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.rotation * torquePower);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
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
            gameObject.SetActive(false);
            gameObject.transform.position = new Vector3(0, 0, 1);
            gameObject.SetActive(true);

            shootTrigger = true;

            StartCoroutine(TriggerEnterOn());
        }
    }

    private IEnumerator TriggerEnterOn()
    {
        yield return new WaitForEndOfFrame();
        isBlastColliding = false;
    }
}