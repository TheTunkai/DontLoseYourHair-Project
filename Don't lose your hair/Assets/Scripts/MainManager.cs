using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainManager : MonoBehaviour
{
    # region Variables
    public string playerName = null;
    public int highScore = 0;
    public string playerNameHighScore = "";
    public Difficulty difficulty = Difficulty.Easy;


    public Text inputText;
    public Text highScoreText;
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
        LoadHighScore();
        highScoreText.text = "High Score " + "(" + playerNameHighScore + ") " + ": " + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputText == null && SceneManager.GetActiveScene().name == "MainMenu")
        {
            inputText = GameObject.Find("InputName").GetComponent<Text>();
        }
        if (highScoreText == null && SceneManager.GetActiveScene().name == "MainMenu")
        {
            highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
            LoadHighScore();
            highScoreText.text = "High Score " + "(" + playerNameHighScore + ") " + ": " + highScore;
        }

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

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string playerName;
    }

    public void SaveHighScore(int score, string name)
    {
        SaveData data = new SaveData();
        data.highScore = score;
        data.playerName = name;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            playerNameHighScore = data.playerName;
        }
    }
    
}




