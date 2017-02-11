using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyPanel : SOPanel {

	// Use this for initialization
	void Start () {
		dismissDelay = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void hide()
	{
		GameManager.sharedManager.isOnLastTutorialTrigger = true;
		GameManager.sharedManager.tutorialUserTapped ();
		this.dismiss (false);
	}
}
