using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private PlayerController playerScript;

    private int playerHearts = 3;
    public int playerScore = 0;
    public float timePoints = 0;

    private bool gameOver = false;

    public delegate void ChangedGameState();
    public event ChangedGameState playerLost; 

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

        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        playerHearts = 3;
        playerScore = 0;
        timePoints = 0;

        playerScript = FindObjectOfType<PlayerController>();

        playerScript.heartLost += PlayerScript_heartLost;
    }

    private void PlayerScript_heartLost() // decreases player hearts when the subscribed event is raised
    {
        DecreasePlayerHearts(1);
    }

    // Update is called once per frame
    void Update()
    {
        timePoints += Time.deltaTime;

        if (timePoints >= 5)
        {
            UpdateScore(1);
            timePoints = 0;
        }



        if (playerHearts == 0)
        {
            Debug.Log("You lost!");
            gameOver = true;
        }

        if (gameOver)
        {
            playerLost?.Invoke();
        }

    }

    void UpdateScore(int value) // updates player score based on input value
    {
        playerScore += value;


    }

    void DecreasePlayerHearts(int value) // decreases player hearts
    {
        playerHearts -= value;
    }
}