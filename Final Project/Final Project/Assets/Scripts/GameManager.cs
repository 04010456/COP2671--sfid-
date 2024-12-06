using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private int score;
    public int difficulty;
    public bool isGameActive = false;
    public Button restartButton;
    public SpawnManager spawnManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI instructionsText;
    public Button pauseButton;
    public Button resumeButton;
    public Button restartGame;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartGame.onClick.AddListener(RestartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // update score function
    public void UpdateScore(int scoreToAdd)
    {
        // add the score to the text whenever the user hits an enemy
        score += scoreToAdd;
        scoreText.text = "Score " + score;
    }

    // game over function
    public void GameOver()
    {
        // set the restart and game over button to active when the game is over and the pause button to inactive
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        isGameActive = false;                // end the game when the game is over
        spawnManager.StopSpawningEnemies();  // stop spawning enemies when the game is over 
    }

    // start game function
    public void StartGame(int selectedDifficulty)
    {
        difficulty = selectedDifficulty;
        score = 0;
        UpdateScore(0);  

        isGameActive = true;                        // start the game 
        spawnManager.StartSpawning();               // spawn enemies when the game start
        instructionsText.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false);    // set the title screen to inactive when the game starts
        pauseButton.gameObject.SetActive(true);     // set the pause button to active when the game starts
    }

    // pause game function
    public void PauseGame()
    {
        // set the pause, restart and resume game buttons to active when the pause menu is clicked 
        pauseButton.gameObject.SetActive(true);
        restartGame.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        isGameActive = false;  // stop the game 
        Time.timeScale = 0f;  // pause the game completely
    }

    // resume game function
    public void ResumeGame()
    {
        // if the resume button is clicked, start the game where it left off and set the resume and restart button to inactive and pause button to active
        isGameActive = true;
        resumeButton.gameObject.SetActive(false);
        restartGame.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;  // resume the game
    }

    public void RestartGame()
    {
        // once the game is restarted resume load the scene  
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
