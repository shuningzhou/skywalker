using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockPanel  : SOPanel {

	public Button tapButton;

	// Use this for initialization
	void Start () {
		dismissDelay = 0.3f;
		tapButton.gameObject.SetActive (false);
		excuateInSeconds (playSound, 1.8f);
	}

	// Update is called once per frame
	void Update () {

	}

	void playSound()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.unlock);
		excuateInSeconds (showTapButton, 2f);
	}

	void showTapButton()
	{
		tapButton.gameObject.SetActive (true);
	}

	public void hide()
	{
		this.dismiss (false);

		GameManager.sharedManager.isOnLastTutorialTrigger = true;
		GameManager.sharedManager.tutorialUserTapped ();
	}
}
