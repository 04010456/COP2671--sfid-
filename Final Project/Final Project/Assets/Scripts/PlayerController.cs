using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;

    private AudioSource playerAudio;
    private Rigidbody playerRb;
    private float horizontalInput;

    public float playerJump = 7;
    public bool whileOnGround = true;
    public float playerSpeed = 7f;
    public float gravityModifier;
    public bool hasPowerup = false;
    public AudioClip jumpSound;
    public AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();  
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // allow the user to move left and right on the game
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed * horizontalInput);

        // the user can only press the UpArrow key to jump while they are on the ground
        if (Input.GetKeyDown(KeyCode.UpArrow) && whileOnGround)
        {
            playerRb.AddForce(Vector3.up * playerJump, ForceMode.Impulse);
            whileOnGround = false;

            playerAudio.PlayOneShot(jumpSound, 1.5f);
        }

        // if the user presses the spacebar, a projectile will fly
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // have the projectile shoot 1f above the users y position
            Vector3 projectilePosition = transform.position;
            projectilePosition.y += 1.0f;
            projectilePosition.x += 3.0f;

            // launch the projectile
            Instantiate(projectilePrefab, projectilePosition, projectilePrefab.transform.rotation);

            playerAudio.PlayOneShot(shootSound, 1.5f);
        }
    }

    // method that sets the whileOnGround variable to true if the player has a collision with something 
    private void OnCollisionEnter(Collision collision)
    {
        whileOnGround = true;
    }

    // method for when the player collides with the powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;  
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());  // call the IEnumerator method that contains the powerup timer
            playerSpeed = 15f;
        }
    }

    // allow the powerup to last for 10 seconds
    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(10);
        hasPowerup = false;
        playerSpeed = 7f;
    }
}
