using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // create public gameobjects for the obstacl, enemy and player prefabs
    public GameObject[] obstaclePrefab;
    public GameObject[] enemyPrefab;
    public GameObject player;

    private Vector3 offsetObstacle = new Vector3(30, 0, 0);
    private Vector3 offsetEnemy = new Vector3(25, 0, 0);
    private float startDelay = 5f;
    private float repeatRate = 5f;
    // Start is called before the first frame update
    void Start()
    {
        // call the function after a certain amount of time and continue to call the function at a certain rate
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        InvokeRepeating("SpawnEnemy", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn obstacle function
    void SpawnObstacle()
    {
        // variable to spawn a random obstacle
        int obstacleIndex = Random.Range(0, obstaclePrefab.Length);

        // spawn new objects in the scene 
        Vector3 spawnPosition = player.transform.position + offsetObstacle;
        Instantiate(obstaclePrefab[obstacleIndex], spawnPosition, obstaclePrefab[obstacleIndex].transform.rotation);
    }

    // spawn enemy function
    void SpawnEnemy()
    {
        // variable to spawn a random enemy
        int enemyIndex = Random.Range(0, enemyPrefab.Length);

        // spawn new enemies in the scene
        Vector3 spawnPosition = player.transform.position + offsetEnemy;
        Instantiate(enemyPrefab[enemyIndex], spawnPosition, enemyPrefab[enemyIndex].transform.rotation);
    }
}
