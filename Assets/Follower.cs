using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
    public Transform target;
	private Vector3 lastPos;


	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Start()
	{
		lastPos = target.position;
	}


	public void LateUpdate()
    {
		this.transform.Translate(target.position - lastPos, Space.World);
		lastPos = target.position;
		
    }	

}
