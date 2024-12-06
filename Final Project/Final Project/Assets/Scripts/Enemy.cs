using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // get instance of enemy rigid body and the players game object
    public float speed = 5f;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // create a reference to the enemy rigid body component and player game object
        enemyRb = GetComponent<Rigidbody>();    
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // move the enemy toward the player
        transform.position += direction * speed * Time.deltaTime;
    }

}
