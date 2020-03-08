using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skins Data", menuName = "hyper-loop/Skins", order = 2)]
public class SkinsData : ScriptableObject {

	public List<SkinModel> skins;
}


[System.Serializable]
public class SkinModel
{
	public Sprite icon;
	public GameObject prefab;
	public string text;
	public int levelUnlock;
	public int comboUnlock;
	public int scoreUnlock;
}
