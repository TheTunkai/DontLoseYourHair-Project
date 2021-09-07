using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private float defaultVolume = 0.6f;
    public AudioSource menuMusic;
    public Canvas optionsMenu;
    public Canvas mainMenu;
    public Slider volumeSlider;
    #endregion

    private void Start()
    {

        menuMusic.volume = defaultVolume;
        volumeSlider.value = defaultVolume;

    }


    public void StartGame() // loads main game scene
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions() // changes to options menu
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void CloseOptions() // chages to main menu
    {
        mainMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }

    public void ChangeVolume()
    {
        menuMusic.volume = volumeSlider.value;
    }
}
