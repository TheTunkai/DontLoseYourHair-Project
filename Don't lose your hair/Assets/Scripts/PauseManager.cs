using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public Canvas optionsMenu;
    public Canvas mainPauseMenu;


    public void OpenOptions()
    {
        mainPauseMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void CloseOptions()
    {
        mainPauseMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }
}
