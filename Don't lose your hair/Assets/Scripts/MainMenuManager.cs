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

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        mainMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void CloseOptions()
    {
        mainMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }
}
