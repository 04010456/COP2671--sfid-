using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float horizontalInput;

    public float playerJump = 7;
    public bool whileOnGround = true;
    public float playerSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
        }
    }

    // method that sets the whileOnGround variable to true if the player has a collision with something 
    private void OnCollisionEnter(Collision collision)
    {
        whileOnGround = true;
    }
}
