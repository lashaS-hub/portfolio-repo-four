using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class GameUiController : MonoBehaviour
{
    #region Private_Fields

    private float sliderValue;
    private float sliderWidth;
    #endregion

    #region Public_Fields
    // public Transform Canvas;
    [Header("Panels")]
    public Transform ScoreParent;
    public Transform ComboParent;
    public GameObject GameWinPanel;
    public GameObject GameOverPanel;
    public Text GameOverText;

    public Transform SkinContainer;
    public GameObject SkinPrefab;
    // public Button SkinsButton;

    [Space]
    public Text distanceText;
    public Text winText;
    public Slider slider;
    public RectTransform ProgressBar;
    public Text fromLevel;
    public Text toLevel;
    public Button StartButton;
    public Button WinNextButton;
    public Button WinMainButton;
    public Button LoseRestartButton;
    public Button LoseMainButton;

    public GameObject ComboPrefab;
    public GameObject ScorePrefab;

    [Header("Bottom Panel")]
	public Button audioOnButton;
	public Button audioOffButton;
    public Button InstaButton;
    public Button FbButton;
    public Button RateButton;
    
    #endregion


    #region delegates_and_events
    public static event UnityAction OnStartGame;
    public delegate void AudioToggle(int i);
    public static event AudioToggle OnAudioChange;

    #endregion

    // Use this for initialization
    void Awake()
    {

        PlayerController.OnUpdateScore += UpdateScore;
        PlayerController.OnUpdateProgress += UpdateProgress;
        PlayerController.OnClick += ClickRespond;
        PlayerController.OnGameOver += GameOver;
        PlayerController.OnChangeLevel += ChangeLevel;
        // PlayerController.OnAddScore += AddScore;
        PlayerController.OnCombo += AddCombo;
        OnAudioChange += ToggleAudioButtons;


        WinNextButton.onClick.AddListener(() =>
        {
            GameManager.instance.NextLevel();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        });

        WinMainButton.onClick.AddListener(LoadMenu);
        LoseMainButton.onClick.AddListener(LoadMenu);

        LoseRestartButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        });

        StartButton.onClick.AddListener(OnStartGame);

        audioOnButton.onClick.AddListener(() =>
        {
            OnAudioChange(0);
        });
        audioOffButton.onClick.AddListener(() =>
        {
            OnAudioChange(1);
        });

        InstaButton.onClick.AddListener(()=>Application.OpenURL("https://www.instagram.com/_u/nefstergames"));
        FbButton.onClick.AddListener(()=>Application.OpenURL("fb://page/NefsterEntertainment"));
        // RateButton.onClick.AddListener(()=>Application.OpenURL("https://play.google.com/store/apps/details?id=com.nefster.meatbusters"));


        var skins = GameManager.instance.SkinsFile.skins;
        int currentSkin = GameManager.instance.CurrentSkinIndex;
        for (int i = 0; i < skins.Count; i++)
        {
            var skinClone = Instantiate(SkinPrefab, SkinContainer);
            skinClone.GetComponent<SkinUI>().UpdateData(skins[i], i, currentSkin == i);
        }

    }



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        sliderWidth = slider.GetComponent<RectTransform>().sizeDelta.x;
        OnAudioChange(PlayerPrefs.GetInt("audio"));
    }


    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }


    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        PlayerController.OnChangeLevel -= ChangeLevel;
        PlayerController.OnUpdateScore -= UpdateScore;
        PlayerController.OnUpdateProgress -= UpdateProgress;
        // PlayerController.OnAddScore -= AddScore;
        PlayerController.OnCombo -= AddCombo;
        PlayerController.OnClick -= ClickRespond;
        PlayerController.OnGameOver -= GameOver;
        OnAudioChange -= ToggleAudioButtons;

    }

    private void ChangeLevel()
    {
        int curLev = GameManager.instance.CurrentLevelIndex + 1;
        fromLevel.text = curLev+"";
        toLevel.text = curLev+1 +"";
    }

    private void UpdateScore(int t)
    {
        // distanceText.text = Methods.AngularSpeed(6378,t)+"";
        distanceText.text = t + "";

        // if(t >= dt.goalSpeed)
        // {
        //     GameWinPanel.SetActive(true);
        // }else 

        // if(t <= dt.minSpeed)
        //     GameOverPanel.SetActive(true);
    }

    private void UpdateProgress(float progress)
    {
        sliderValue = progress;//dt.NormalizeSpeed(s);
        Debug.Log(progress);
        GameOverText.text = (int)(progress * 100) +"% Completed";
    }

    private void GameOver(float score)
    {
        GameOverPanel.SetActive(true);
        PlayerController.OnUpdateScore -= UpdateScore;

    }

    // private void AddScore(int score)
    // {
    //     var s = Instantiate(ScorePrefab, Canvas);
    //     s.GetComponent<Text>().text = score + "";
    //     Destroy(s, 2);

    // }

    private void ClickRespond(AccelType i)
    {
        if (i == AccelType.BAD)return;

        var s = Instantiate(ScorePrefab, ScoreParent);
        s.GetComponent<Text>().text = i + "";
        Destroy(s, 2);

    }

    private void AddCombo(int combo)
    {
        var c = Instantiate(ComboPrefab, ComboParent);
        c.GetComponent<Text>().text = "x" + combo;
        Destroy(c, 2);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, sliderValue, Time.deltaTime * 3);
        ProgressBar.sizeDelta = new Vector2(slider.value * sliderWidth,  ProgressBar.sizeDelta.y);

    }


    void ToggleAudioButtons(int i)
    {
        audioOnButton.gameObject.SetActive(i == 1);
        audioOffButton.gameObject.SetActive(i == 0);
    }

}
