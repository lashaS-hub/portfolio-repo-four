using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConvertToJson : MonoBehaviour {

	[MenuItem("Prefs/Clear")]
	static void Clear()
	{
		PlayerPrefs.DeleteAll();
	}


	[MenuItem("Assets/Convert To Json", true)]
    static bool CheckData()
    {
        // Return false if no transform is selected.
        return Selection.activeObject is LevelsData;
    }

	[MenuItem("Assets/Convert To Json")]
    static void ToJson()
    {
		LevelsData dat = (LevelsData)Selection.activeObject;
		string json = JsonUtility.ToJson(dat.list);
		System.IO.File.WriteAllText("Assets/Data/data.json", json);
    }


}
