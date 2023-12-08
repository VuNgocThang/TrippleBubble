using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip eat, pop, click, fail, win, clickMenu;
    public AudioSource aus;
    public AudioSource backGroundMusic;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);


        BackgroundMusic();
    }

    public void UpdateSoundAndMusic(AudioSource audioSource, AudioClip audioClip)
    {
        int isSound = PlayerPrefs.GetInt("isSound");
        audioSource.PlayOneShot(audioClip);

        audioSource.volume = isSound == 1 ? 1 : 0;
        if (audioClip == pop && isSound == 1) audioSource.volume = 0.2f;
    }
    public void BackgroundMusic()
    {
        int isMusic = PlayerPrefs.GetInt("isMusic");

        if (isMusic == 0)
        {
            backGroundMusic.Pause();
        }
        else
        {
            backGroundMusic.Play();
            backGroundMusic.UnPause();
        }

    }
}
