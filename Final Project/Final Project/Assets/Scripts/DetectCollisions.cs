using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    // create an instance of the game manager
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            // have the projectile destroy the enemy and the obstacles
            gameManager.UpdateScore(5);
            Destroy(gameObject);
        }

        Destroy(other.gameObject);
    }
}
