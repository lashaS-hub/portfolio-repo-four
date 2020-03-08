using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WorldData", menuName = "hyper-loop/WorldData", order = 0)]
public class WorldData : ScriptableObject {
	
	public GameObject WorldPrefab;
	public GameObject AcceleratorPrefab;

	public Material skyBox;

	public Sprite worldIcon;


	public List<Models.Zone> zones;

	// public float NormalizeSpeed(float speed)
	// {
	// 	return (speed - minSpeed)/(goalSpeed - minSpeed);
	// }

}
