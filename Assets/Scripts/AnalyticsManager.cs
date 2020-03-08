using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;

public class AnalyticsManager : MonoBehaviour
{


    // Awake function from Unity's MonoBehavior
    void Awake()
    {

        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
        GameUiController.OnStartGame += StartEvent;
        PlayerController.OnGameOver += GameOverEvent;

    }

    void OnDestroy()
    {
        GameUiController.OnStartGame -= StartEvent;
        PlayerController.OnGameOver -= GameOverEvent;
    }

#region FACEBOOK

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
#endregion


#region GAME_ANALYTICS
    private void GameOverEvent(float score)
    {
    }

    private void StartEvent()
    {
    }


#endregion

}
