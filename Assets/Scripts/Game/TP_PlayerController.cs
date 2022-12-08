using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_PlayerController : MonoBehaviour
{
    // Public references
    public GameObject shotPrefab;
    public ParticleSystem propulse;
    public TP_RespawnZone respawnZone;
    
    // Values
    [SerializeField] private float forcePower = 4f;
    [SerializeField] private float maxVelocity = 3f;
    [SerializeField] private Vector3 torquePower = new Vector3(0, 0, 5);

    private bool shootTrigger = true;
    private float shootCooldown = 0.15f;

    private bool canMove = true;
    private bool isTriggering = false;

    // Private references
    private TP_GameManager gameManager;
    private Rigidbody2D _playerRigidbody;
    private PolygonCollider2D _playerCollider;
    private Animator _playerAnimator;
    private TP_AudioManager audioManager;

    void Start()
    {
        // Gets references
        gameManager = FindObjectOfType<TP_GameManager>();
        audioManager = FindObjectOfType<TP_AudioManager>();

        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<PolygonCollider2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Shoot function
        Shoot();
    }

    private void FixedUpdate()
    {
        // Movement function
        Movement();
    }

    // Player movement
    private void Movement()
    {
        // If you press the UP key and canMove is true
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && canMove)
        {
            // Adds force forward
            _playerRigidbody.AddForce(transform.up * forcePower);

            // Plays particles and audio if they are not activated
            if (propulse.isStopped)
            {
                propulse.Play();
                audioManager.PlayAudioPropellant();
            }
        }
        else
        {
            // Stop particles and audio if they are activated
            if (propulse.isPlaying)
            {
                propulse.Stop();
                audioManager.StopAudioPropellant();

                // Reduces velocity
                _playerRigidbody.velocity *= new Vector2(0.96f, 0.96f);
            }
        }

        // Rotate to LEFT
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.rotation * torquePower);
        }

        // Rotate to RIGHT
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.rotation * -torquePower);
        }

        // Limits the velocity
        if (_playerRigidbody.velocity.magnitude > maxVelocity)
        {
            _playerRigidbody.velocity = _playerRigidbody.velocity.normalized * maxVelocity;
        }
    }

    // Player's shot
    private void Shoot()
    {
        // If you press the Space key and shootTrigger is True & canMove is true
        if (Input.GetKey(KeyCode.Space) && shootTrigger && canMove)
        {
            // Plays audio
            audioManager.PlayAudioShoot();

            // Spawns shot from a determined position
            GameObject shot = Instantiate(shotPrefab, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);

            // Changes shot velocity depending on the player's speed
            shot.GetComponent<TP_Shot>().maxVelocity = shot.GetComponent<TP_Shot>().forcePower + _playerRigidbody.velocity.magnitude * 1.5f;

            // Starts the cooldown
            StartCoroutine(ShootCooldown());
        }
    }

    // Cooldown between each shot
    private IEnumerator ShootCooldown()
    {
        shootTrigger = false;
        yield return new WaitForSeconds(shootCooldown);
        shootTrigger = true;
    }

    // Gets hit by Asteroid
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            // Checks that only one asteroid is colliding (FIX)
            // Prevents it from executing more than once by exiting the function.
            if (isTriggering) return;
            isTriggering = true;

            // Update lives remaining
            gameManager.UpdateLives();

            // Respawn player
            StartCoroutine(RespawnPlayer());

            // Restarts trigger fix
            StartCoroutine(TriggerEnterOn());
        }
    }

    // Respawn player on the middle
    public IEnumerator RespawnPlayer()
    {
        // Player can't move and collider is deactivated
        canMove = false;
        _playerCollider.enabled = false;

        // Respawn the player
        transform.position = respawnZone.transform.position;
        _playerRigidbody.velocity = Vector2.zero;

        // Plays animation
        _playerAnimator.Play("PlayerRespawn");

        // Plays audio
        audioManager.PlayAudioRespawn();

        // Waits a time
        yield return new WaitForSeconds(0.1f);

        // If is safe to spawn
        if (respawnZone.isSafe)
        {
            // Player can move and collider is ctivated
            canMove = true;
            _playerCollider.enabled = true;

            // Stops audio
            audioManager.StopAudioRespawn();
        }
        // If is not safe to spawn, execute this same function
        else
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    // Respawn player variant when a round is ended
    public void RespawnPlayerRound()
    {
        // Respawns to middle of the screen
        transform.position = respawnZone.transform.position;
        _playerRigidbody.velocity = Vector2.zero;

        // Removes all shots maded by the player
        foreach (TP_Shot shot in FindObjectsOfType<TP_Shot>())
        {
            Destroy(shot.gameObject);
        }
    }

    // Fix to only check a single collision of a asteroid
    private IEnumerator TriggerEnterOn()
    {
        yield return new WaitForEndOfFrame();
        isTriggering = false;
    }
}