using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Levels Data", menuName = "hyper-loop/Levels", order = 1)]
public class LevelsData : ScriptableObject
{
    public LevelList list;
}

[System.Serializable]
public class LevelList
{
    public List<LevelData> levels;
    
}
[System.Serializable]
public class LevelData
{
    // [Range(0,4)]
    public int loop;
    public int offset;
    public bool[] Pattern;
    public float startDPS;
    public float perfectDist;
    public float mediumDist;
    public float speedModifier;
    public int minScore;
    public float goal;
}