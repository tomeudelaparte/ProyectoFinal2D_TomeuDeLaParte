using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_PlayerController : MonoBehaviour
{
    public GameObject shotPrefab;
    public ParticleSystem propulse;
    public TP_RespawnZone respawnZone;

    [SerializeField] private float forcePower = 4f;
    [SerializeField] private float maxVelocity = 3f;
    [SerializeField] private Vector3 torquePower = new Vector3(0, 0, 5);

    private TP_GameManager gameManager;
    private Rigidbody2D _playerRigidbody;
    private PolygonCollider2D _playerCollider;
    private Animator _playerAnimator;
    private TP_AudioManager audioManager;

    private bool shootTrigger = true;
    private float shootCooldown = 0.15f;

    private bool canMove = true;
    private bool isTriggering = false;

    void Start()
    {
        gameManager = FindObjectOfType<TP_GameManager>();
        audioManager = FindObjectOfType<TP_AudioManager>();

        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<PolygonCollider2D>();
        _playerAnimator = GetComponent<Animator>();

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
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && canMove)
        {
            _playerRigidbody.AddForce(transform.up * forcePower);

            if (propulse.isStopped)
            {
                propulse.Play();
                audioManager.PlayAudioPropellant();
            }
        }
        else
        {
            if (propulse.isPlaying)
            {
                propulse.Stop();
                audioManager.StopAudioPropellant();

                _playerRigidbody.velocity *= new Vector2(0.96f, 0.96f);
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

        if (_playerRigidbody.velocity.magnitude > maxVelocity)
        {
            _playerRigidbody.velocity = _playerRigidbody.velocity.normalized * maxVelocity;
        }
    }

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space) && shootTrigger && canMove)
        {
            audioManager.PlayAudioShoot();

            GameObject shot = Instantiate(shotPrefab, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);

            shot.GetComponent<TP_Shot>().maxVelocity = shot.GetComponent<TP_Shot>().forcePower + _playerRigidbody.velocity.magnitude * 1.5f;

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
            if (isTriggering) return;
            isTriggering = true;

            gameManager.UpdateLives();

            StartCoroutine(RespawnPlayer());

            StartCoroutine(TriggerEnterOn());
        }
    }

    public IEnumerator RespawnPlayer()
    {
        canMove = false;
        _playerCollider.enabled = false;

        transform.position = respawnZone.transform.position;
        _playerRigidbody.velocity = Vector2.zero;

        _playerAnimator.Play("PlayerRespawn");

        audioManager.PlayAudioRespawn();

        yield return new WaitForSeconds(0.1f);

        if (respawnZone.isSafe)
        {
            canMove = true;
            _playerCollider.enabled = true;

            audioManager.StopAudioRespawn();
        }
        else
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    public void RespawnPlayerRound()
    {
        transform.position = respawnZone.transform.position;
        _playerRigidbody.velocity = Vector2.zero;
    }

    private IEnumerator TriggerEnterOn()
    {
        yield return new WaitForEndOfFrame();
        isTriggering = false;
    }
}