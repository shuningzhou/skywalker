using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPanel  : SOPanel {

	// Use this for initialization
	void Start () {
		dismissDelay = 0.3f;
		excuateInSeconds (playSound, 1.8f);
	}

	// Update is called once per frame
	void Update () {

	}

	void playSound()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.unlock);
	}

	public void hide()
	{
		this.dismiss (false);

		GameManager.sharedManager.isOnLastTutorialTrigger = true;
		GameManager.sharedManager.tutorialUserTapped ();
	}
}
