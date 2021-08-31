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
        // implementing singleton pattern
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
    {   // subscribe to player event
        GameManager.instance.playerLost += OnPlayerDie;
        // set background music volume to default value
        bgMusic.volume = defaultVolume;
    }


    private void OnPlayerDie()
    {
        // turns off music, when player loses
        bgMusic.volume = 0;
       
    }

    public void PlaySound(int soundID, float volume, bool doLoop) // plays sound clip with given ID and volume,
    {
        soundEffectsPlayer.volume = volume;
        soundEffectsPlayerLoop.volume = volume;
        AudioClip audioClip = soundClips[soundID];

        if (doLoop) // loops sound, when looping is wished
        {
            soundEffectsPlayerLoop.loop = true;
            soundEffectsPlayerLoop.clip = audioClip;
            soundEffectsPlayerLoop.Play();
        }
        else // plays sound clip one time
        {
        soundEffectsPlayer.PlayOneShot(soundClips[soundID]);
        }

    }

    
    public void PlayMusic(string condition) // plays specific music, when an in-game condition is met
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

    public void StopSoundLoop() // stops sound loops played by soundEffectsPlayerLoop
    {
        soundEffectsPlayerLoop.clip = null;
        soundEffectsPlayerLoop.Stop();
    }
}
