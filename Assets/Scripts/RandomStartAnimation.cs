using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomStartAnimation : MonoBehaviour {

	public int maxDelay;
	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(Random.Range(0, maxDelay));
		GetComponent<Animator>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
