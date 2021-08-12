using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Canvas optionsMenu;
    public Canvas mainPauseMenu;

    public void BackToMenu() // loads main menu scene
    {
        SceneManager.LoadScene(0);
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
