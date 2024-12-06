using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    // create instance of the game timer text
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime = 60;
    private int minutes;
    private int seconds;

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
        // if the time is greater than zero and the game is still active then count down the timer
        if (remainingTime > 0 && gameManager.isGameActive && gameManager != null)
        {
            remainingTime -= Time.deltaTime;
            minutes = Mathf.FloorToInt(remainingTime / 60);  
            seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);  // designated string format of the timer 
        }
        // if the time is less than zero, stop the clock at 00:00 and then end the game if the game is still active
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            timerText.text = "Time: 00:00";

            if (gameManager.isGameActive)
            {
                gameManager.GameOver();
            }
        }
    }
}
