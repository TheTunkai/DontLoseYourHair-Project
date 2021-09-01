using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private float defaultVolume = 0.8f;
    public Canvas optionsMenu;
    public Canvas mainPauseMenu;
    public Slider volumeSlider;
    public AudioSource bgMusic;

    
    #endregion

    private void Start()
    {
        volumeSlider.value = defaultVolume;
    }

    private void Update()
    {
        if (GameManager.instance.gameIsPaused) // in pause volume slider changes music volume
        {
            bgMusic.volume = volumeSlider.value;
        }

    }


    public void BackToMenu() // loads main menu scene
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void OpenOptions() // changes canvas to options menu
    {
        mainPauseMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void CloseOptions() // changes canvas to pause menu
    {
        mainPauseMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }
}
