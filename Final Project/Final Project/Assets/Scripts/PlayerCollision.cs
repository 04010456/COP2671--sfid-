using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // create instance of the game manager
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        // create a reference to the game manager component
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        // if the user hits the enemy, end the game
        if (other.gameObject.CompareTag("Enemies"))
        {
            // Handle game-over logic when the player collides with an enemy
            gameManager.GameOver();
        }
    }
}
