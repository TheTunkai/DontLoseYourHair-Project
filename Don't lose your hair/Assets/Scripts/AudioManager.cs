using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private float defaulVolume = 1f;

    public AudioSource bgMusic;
    

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.playerLost += Instance_playerLost;

        bgMusic.volume = defaulVolume;
    }

    private void Instance_playerLost()
    {
        bgMusic.volume = 0;
    }

}
