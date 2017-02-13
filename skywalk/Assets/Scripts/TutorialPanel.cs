using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialPanel : SOPanel {

	// Use this for initialization
	void Start () {
		dismissDelay = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void hide()
	{
		GameManager.sharedManager.tutorialUserTapped ();
		this.dismiss (false);
	}
}
