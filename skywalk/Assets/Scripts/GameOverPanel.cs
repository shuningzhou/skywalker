using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : SOPanel {

	public string scoreString;

	public Text scoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void resetUI()
	{
		scoreText.text = "";
	}

	public void updateUI()
	{
		scoreText.text = scoreString;
	}

	public void replayed()
	{
		excuateInSeconds (doReplayed, 1f);
	}

	public void homed()
	{
		excuateInSeconds (doHomed, 1f);
	}

	public void doReplayed()
	{
		LevelManager.sharedManager.playCurrentLevel ();
	}

	public void doHomed()
	{
		LevelManager.sharedManager.currentLevelFinishedAndRetured ();
	}
}
