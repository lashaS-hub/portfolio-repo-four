using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AccelType
{
	BAD,
	GOOD,
	PERFECT
}

public class Accelerator : MonoBehaviour {

	public ParticleSystem trail;
	public Indicator indicator;
	private Animator animator;
	public static Accelerator nextOne;

	public void Activate(AccelType state, float speed)
	{
		animator.SetTrigger("Accel");

		// trail.startSpeed = speed;
		// if(state == AccelType.PERFECT)
		// 	trail.Play();
	}
	
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if(nextOne == null){
			nextOne = this; // FIRST
			// Debug.Log("NAni");
		}
		animator = GetComponent<Animator>();
	}


	/// <summary>
	/// This function is called when the MonoBehaviour will be destroyed.
	/// </summary>
	void OnDestroy()
	{
		nextOne = null;
	}

	public void HideAccelerator()
	{
		gameObject.SetActive(false);
		indicator.ResetIndicator();
	}
}
