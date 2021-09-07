using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    # region Variables
    public string playerName = null;
    public Difficulty difficulty = Difficulty.Easy;


    public Text inputText;
    public static MainManager instance;
    # endregion
    // called before Start
    void Awake()
    {
        // applying singleton pattern
        if(instance == null){
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameDifficulty(string difficultySetting){
        
        if (difficultySetting == "easy")
        {
            difficulty = Difficulty.Easy;
        }
        else if (difficultySetting == "medium")
        {
            difficulty = Difficulty.Medium;
        }
        else if (difficultySetting == "hard")
        {
            difficulty = Difficulty.Hard;
        }

        Debug.Log("Difficulty set to " + difficulty);
    }

    public void SetPlayerName()
    {
        playerName = inputText.text;
    }
}
