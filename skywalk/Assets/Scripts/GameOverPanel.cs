using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : SOPanel {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
		LevelManager.sharedManager.currentLevel.saveLevelRating (1);
		LevelManager.sharedManager.playCurrentLevel ();
	}

	public void doHomed()
	{
		LevelManager.sharedManager.currentLevel.saveLevelRating (1);
		LevelManager.sharedManager.currentLevelFinishedAndRetured ();
	}
}
