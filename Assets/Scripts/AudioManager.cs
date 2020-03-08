using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public AudioSource soundEffectSource;

    public List<AudioClip> acceleratorSounds;
    public AudioClip WinSound;
    public AudioClip LoseSound;

    // Use this for initialization
    void Awake()
    {
        PlayerController.OnClick += AccelSound;
        // PlayerController.OnUpdateScore += WinOrLoseSound;
        PlayerController.OnGameOver += GameOverSound;
        GameUiController.OnAudioChange += ToggleAudio;
    }

    private void ToggleAudio(int i)
    {
        BackgroundMusic.enabled = i == 1;
        soundEffectSource.enabled = i == 1;
    }



    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        PlayerController.OnClick -= AccelSound;
        // PlayerController.OnUpdateScore -= WinOrLoseSound;
        PlayerController.OnGameOver -= GameOverSound;
        GameUiController.OnAudioChange -= ToggleAudio;

    }

    public void AccelSound(AccelType i)
    {
        if((int)i > 0)
        soundEffectSource.PlayOneShot(acceleratorSounds[(int)i]);
    }

    public void WinOrLoseSound(int t)
    {
        // Debug.Log("WATAATATA");

        // if (t >= dt.goalSpeed)
        // {
        //     soundEffectSource.Stop();
        //     soundEffectSource.PlayOneShot(WinSound);
        //     PlayerController.OnUpdateScore -= WinOrLoseSound;
        // }
        // else if (t <= dt.minSpeed)
        // {
        //     soundEffectSource.Stop();
        //     soundEffectSource.PlayOneShot(LoseSound);
        //     PlayerController.OnUpdateScore -= WinOrLoseSound;
        // }


    }

    
    private void GameOverSound(float score)
    {
        soundEffectSource.PlayOneShot(LoseSound);
        // throw new NotImplementedException();
    }


}
