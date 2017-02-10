using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SOPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void show(bool playSound)
	{
		if (playSound) 
		{
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		}

		this.gameObject.SetActive (true);
	}

	public virtual void dismiss(bool playSound)
	{
		if (playSound) 
		{
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		}

		Animator a = GetComponent<Animator> ();
		a.Play ("close");
		excuateInSeconds (doDeactive, 1f);
	}

	public void doDeactive()
	{
		this.gameObject.SetActive (false);
	}

	public void excuateInSeconds(Action action, float seconds)
	{
		StartCoroutine (delayStart(seconds, action));
	}

	IEnumerator delayStart(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action ();
	}
}
