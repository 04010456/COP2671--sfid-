using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // create public gameobjects for the obstacl, enemy and player prefabs
    public GameObject[] obstaclePrefab;
    public GameObject[] enemyPrefab;
    public GameObject powerupPrefab;
    public GameObject player;
    public int enemyCount;
    public int waveNumber = 1;

    public bool isEnemiesBeingSpawned = false;
    private Vector3 offsetObstacle = new Vector3(30, 0, 0);
    private Vector3 offsetEnemy = new Vector3(25, 0, 0);
    private float startDelay = 5f;
    private float repeatRate = 5f;
    private GameManager gameManager;
 
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            enemyCount = FindObjectsOfType<Enemy>().Length;
            if (enemyCount == 0 && !isEnemiesBeingSpawned && gameManager.isGameActive)
            {
                waveNumber++;
                StartCoroutine(SpawnEnemy(waveNumber));  // coroutine for the spawn enemies method
                Instantiate(powerupPrefab, GenerateRandomPowerupPosition(), powerupPrefab.transform.rotation);  // generate a powerup when there are not more enemies on the screen
            }
        }
    }

    // spawn obstacle function
    public void SpawnObstacle()
    { 
        // spawn obstacles when the game is active
        if (!gameManager.isGameActive)
        {
            return;
        }
        // variable to spawn a random obstacle
        int obstacleIndex = Random.Range(0, obstaclePrefab.Length);

        // spawn new objects in the scene 
        Vector3 spawnPosition = player.transform.position + offsetObstacle;
        Instantiate(obstaclePrefab[obstacleIndex], spawnPosition, obstaclePrefab[obstacleIndex].transform.rotation);
    }

    // spawn enemy function
    public IEnumerator SpawnEnemy(int enemiesSpawning)
    {
        if (isEnemiesBeingSpawned)
        {
            yield break;  // if coroutine is running, prevent another instance of it running again
        }

        isEnemiesBeingSpawned = true;

        for (int i = 0; i < enemiesSpawning; i++)
        {
            // variable to spawn a random enemy
            int enemyIndex = Random.Range(0, enemyPrefab.Length);

            // spawn new enemies in the scene
            Instantiate(enemyPrefab[enemyIndex], GenerateRandomSpawnPosition(), enemyPrefab[enemyIndex].transform.rotation);
        }
        // wait for specific time interval int between spawning the enemies
        yield return new WaitForSeconds(startDelay);

        isEnemiesBeingSpawned = false;
    }

    // generate a random spawn position of the enemies
    private Vector3 GenerateRandomSpawnPosition()
    {
        float spawnPosX = player.transform.position.x + Random.Range(15, 25);
        Vector3 randomPos = new Vector3(spawnPosX, 0, 0);
        return randomPos;
    }

    // generate a random spawn position of the powerups
    private Vector3 GenerateRandomPowerupPosition()
    {
        float spawnPosX = player.transform.position.x + Random.Range(7, 10);
        Vector3 randomPos = new Vector3(spawnPosX, 3, 0);
        return randomPos;
    }

    public void StopSpawningEnemies()
    {
        isEnemiesBeingSpawned = true;
        CancelInvoke("SpawnObstacle");
        StopAllCoroutines();
    }

    // spawn obstacles and enemies when the game starts
    public void StartSpawning()
    {
        // call the function after a certain amount of time and continue to call the function at a certain rate
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        StartCoroutine(SpawnEnemy(waveNumber));  // coroutine for the spawn enemies method
    }
}
