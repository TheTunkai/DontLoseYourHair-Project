using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private float defaultVolume = 1f;

    public AudioClip[] soundClips;
    public AudioClip gameOverMusic;


    public AudioSource bgMusic;
    public AudioSource soundEffectsPlayer;
    public AudioSource musicPlayer;
    public static AudioManager instance = null;
    #endregion


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.playerLost += Instance_playerLost;

        bgMusic.volume = defaultVolume;
    }


    private void Instance_playerLost()
    {
        
        bgMusic.volume = 0;
       
    }

    public void PlaySound(int soundID)
    {
        soundEffectsPlayer.volume = bgMusic.volume;
        soundEffectsPlayer.PlayOneShot(soundClips[soundID]);

    }

    public void PlayMusic(string condition)
    {
        if (condition == "game_over")
        {
            musicPlayer.PlayOneShot(gameOverMusic);
        }
        else if(condition == "level_finished")
        {
            return;
        }
    }
}
