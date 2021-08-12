using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuManager : MonoBehaviour
{
    public Canvas optionsMenu;
    public Canvas mainMenu;

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
}
