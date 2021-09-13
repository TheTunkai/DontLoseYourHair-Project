using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;
    private PlayerController playerScript;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public Text scoreText;
    public Text endScoreText;
    public Text heartsText;

    public int playerHearts = 3;
    public int enemyCount = 0;
    [SerializeField] private int playerScore = 0;
    [SerializeField] private float timePoints = 0;


    public bool gameOver = false;
    public bool gameIsPaused = false;
    public bool gameOverCalled = false;

    public event Action playerLost;
    public event Action startEnemyWave;

    public event Action gamePaused;
    public event Action gameResumed;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null) // makes singleton instance
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

       
    }

    private void Start()
    {
        playerHearts = 3;
        playerScore = 0;
        timePoints = 0;
        

        playerScript = FindObjectOfType<PlayerController>();

        playerScript.heartLost += OnPlayerHeartLost;

        if (pauseMenu == null)
        {
            pauseMenu = GameObject.Find("PauseMenu");
        }

        if (gameOverMenu == null)
        {
            gameOverMenu = GameObject.Find("GameOverMenu");
        }
    }

    private void OnPlayerHeartLost() // decreases player hearts when the subscribed event is raised
    {
        DecreasePlayerHearts(1);
    }


    // Update is called once per frame
    void Update()
    {
        if (!gameOver) // only does the code as long as game isn't over
        {
            if (playerScore > 0 && playerScore % 10 == 0 && enemyCount == 0) // condition for enemy wave
            {
                startEnemyWave?.Invoke();
            }

            timePoints += Time.deltaTime;

            if (timePoints >= 2.5)
            {
                UpdateScore(1);
                timePoints = 0;
            }

            if (Input.GetKeyDown(KeyCode.Escape)) // esc is declared as pause button
            {
                gameIsPaused = !gameIsPaused;

                if (gameIsPaused)
                {
                    PauseGame();
                }
                else if (!gameIsPaused)
                {
                    ResumeGame();
                }
            }
        }


        if (playerHearts <= 0)
        {
            gameOver = true;
        }

        if (gameOver && !gameOverCalled) // invokes game over, only once!
        {
            AudioManager.instance.PlayMusic("game_over");
            gameOverCalled = true;
            gameOverMenu.SetActive(true);
            endScoreText.text = "Your Score: " + playerScore;
            if (playerScore > MainManager.instance.highScore && MainManager.instance.difficulty == Difficulty.Hard)
            {
                MainManager.instance.SaveHighScore(playerScore, MainManager.instance.playerName);
            }
            
            playerLost?.Invoke();
        }

        

        

    }

    void UpdateScore(int value) // updates player score based on input value
    {
        playerScore += value;
        scoreText.text = "Score: " + playerScore;

    }

    void DecreasePlayerHearts(int value) // decreases player hearts
    {
        playerHearts -= value;
        heartsText.text = "Hearts: " + playerHearts;
    }

    public void PauseGame() // pauses game by setting time scale to zero
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        gamePaused?.Invoke();
    }

    public void ResumeGame() // sets time scale to 1 and thus unpauses game
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        gameResumed?.Invoke();
    }

    public void RestartGame() // loads game scene
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerScore = 0;
        playerHearts = 3;
        timePoints = 0;
        gameOver = false;
    }

    public void QuitGame() // quits game out of editor
    {
        Application.Quit();
    }
}
