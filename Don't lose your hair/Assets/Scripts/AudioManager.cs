using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private float defaultVolume = 0.8f;

    public AudioClip[] soundClips;
    public AudioClip gameOverMusic;


    public AudioSource bgMusic;
    public AudioSource soundEffectsPlayer;
    public AudioSource soundEffectsPlayerLoop;
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
        GameManager.instance.playerLost += OnPlayerDie;

        bgMusic.volume = defaultVolume;
    }


    private void OnPlayerDie()
    {
        
        bgMusic.volume = 0;
       
    }

    public void PlaySound(int soundID, float volume, bool doLoop)
    {
        soundEffectsPlayer.volume = volume;
        soundEffectsPlayerLoop.volume = volume;
        AudioClip audioClip = soundClips[soundID];

        if (doLoop)
        {
            soundEffectsPlayerLoop.loop = true;
            soundEffectsPlayerLoop.clip = audioClip;
            soundEffectsPlayerLoop.Play();
        }
        else
        {
        soundEffectsPlayer.PlayOneShot(soundClips[soundID]);
        }

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

    public void StopSoundLoop()
    {
        soundEffectsPlayerLoop.clip = null;
        soundEffectsPlayerLoop.Stop();
    }
}
