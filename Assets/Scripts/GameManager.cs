using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{

    // public PlayerController pc;

    public static GameManager instance;

    // public List<WorldData>
    public LevelsData LevelsFile;

    public List<WorldData> Worlds;
    public List<LevelData> Levels;

    public SkinsData SkinsFile;


    private int currentLevelIndex;
    private int currentWorldIndex;

    public LevelData CurrentLevel
    {
        get{return Levels[currentLevelIndex];}
    }

    public int CurrentLevelIndex
    {
        get{return currentLevelIndex;}
    }
    public WorldData CurrentWorld
    {
        get{return Worlds[currentWorldIndex];}
    }
    public int CurrentSkinIndex
    {
        get{return PlayerPrefs.GetInt("skin_index");}
        set{PlayerPrefs.SetInt("skin_index", value);}
    }


    void Awake()
    {
        instance = this;
        Levels = LevelsFile.list.levels;
        if(PlayerPrefs.HasKey("levels")){
            // Debug.Log(PlayerPrefs.GetString("levels"));
            Levels = JsonUtility.FromJson<LevelList>(PlayerPrefs.GetString("levels")).levels;
        }
        DownloadLevels();
        currentLevelIndex = PlayerPrefs.GetInt("current_level") - 1;
        currentWorldIndex = -1;//TODO CURRENT
        PlayerController.OnChangeLevel += NextLevel;
        SkinUI.OnChangeSkin += ChangeSkin;
        PlayerController.OnCombo += ComboSave;
        GameUiController.OnAudioChange += ChangeAudio;
        // NextLevel();
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        PlayerController.OnChangeLevel -= NextLevel;
        SkinUI.OnChangeSkin -= ChangeSkin;
        PlayerController.OnCombo -= ComboSave;


    }

    private void ChangeSkin(int i)
    {
        CurrentSkinIndex = i;
    }

    private void ChangeAudio(int i)
    {
        PlayerPrefs.SetInt("audio", i);
    }

    private void ComboSave(int c)
    {
        PlayerPrefs.SetInt("max_combo", Mathf.Max(PlayerPrefs.GetInt("max_combo"), c));
    }

    public void NextLevel()
    {
        NextWorld();
        if(currentLevelIndex < Levels.Count-1)
            currentLevelIndex++;
        PlayerPrefs.SetInt("current_level", currentLevelIndex);
            // currentIndex=0;
        // curLev = Levels[currentIndex];
    }

    public void NextWorld()
    {
        currentWorldIndex++;
        if(currentWorldIndex == Worlds.Count)
            currentWorldIndex = 0;
    }

    
    private void DownloadLevels()
    {
        StartCoroutine(GetLevels());
    }

    IEnumerator GetLevels()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://raw.githubusercontent.com/bukamakh/Boxel/master/data.json"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                PlayerPrefs.SetString("levels", www.downloadHandler.text);
                PlayerPrefs.Save();
                Debug.Log("Downloaded levels");
            }
        }
    }

    
    public SkinModel GetSkinByIndex(int i)
    {
        return SkinsFile.skins[i];
    }

}
