using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int playerHearts = 3;
    public int playerScore = 0;

    public PlayerController playerScript;


    // Start is called before the first frame update
    void Start()
    {
        playerHearts = 3;
        playerScore = 0;

        playerScript = FindObjectOfType<PlayerController>();

        playerScript.heartLost += PlayerScript_heartLost;

    }

    private void PlayerScript_heartLost()
    {
        DecreasePlayerHearts(1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore(Mathf.RoundToInt(Time.deltaTime));

        if (playerHearts == 0)
        {
            Debug.Log("You lost!");
        }

    }

    void UpdateScore(int value)
    {
        playerScore += value;


    }

    void DecreasePlayerHearts(int value)
    {
        playerHearts -= value;
    }
}
