using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {
	public GameGUI gameGUI;
	public CameraMovement cameraMovement;
	public RoadGenerator roadGenerator;
	public CharacterMovement characterMovement;

	private bool gameOver = false;
	private int diamondCount = 0;
	private bool shouldShowTip = true;
	private float totalDistance = 0;
	//private float bestDistance = 0;
	private int diamondCountThisRound = 0;

	private string debugText;

	// Use this for initialization
	void Awake()
	{
		append ("Awake");
//		GameData oldData = loadGameData ();
//		append ("Awake1");
//		diamondCount = oldData.collectableCount;
//		append ("Awake2");
	}

	void Start () 
	{
		if (shouldShowTip) {
			gameGUI.showTipPanel ();
		} else {
			gameStart ();
		}
	}

	public void gameStart()
	{
		StartCoroutine (doStart ());
	}

	IEnumerator doStart()
	{
		yield return new WaitForSeconds(1);
		roadGenerator.doGameStart ();
		characterMovement.doGameStart ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public bool isGameOver()
	{
		return gameOver;
	}

	public void playerFailed()
	{
		append ("playerFailed");
		gameOver = true;
		cameraMovement.playerFailed ();
		characterMovement.doGameEnd ();
		//SCAnalytics.logGameOverEvent (totalDistance, diamondCountThisRound);

		GameData oldData = loadGameData ();
		oldData.collectableCount = diamondCount;
		append ("playerFailed1");
		if (totalDistance > oldData.bestEntryDistance) 
		{
			oldData.bestEntryDate = DateTime.Now;
			oldData.bestEntryDistance = totalDistance;
		}
		append ("playerFailed2");
		saveGameData (oldData);

		gameGUI.playerFailed (diamondCount);
	}

	public void collectedDiamond()
	{
		diamondCount = diamondCount + 1;
		diamondCountThisRound = diamondCountThisRound + 1;
		gameGUI.setDiamond (diamondCount);
	}

	public void playerMoved(float distance)
	{
		totalDistance = totalDistance + distance;
		gameGUI.setDistance (totalDistance);
	}

	public void pauseGame()
	{
		characterMovement.doGamePaused ();
	}

	public void resumeGame()
	{
		StartCoroutine (doResume());
	}

	IEnumerator doResume()
	{
		yield return new WaitForSeconds(0.2f);
		characterMovement.doGameResume ();
	}

	public void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	public GameData loadGameData()
	{
		//return new GameData();
		try {
			var scoresFile = Application.persistentDataPath +
				"/" + "gameData.dat";
			append (scoresFile);
			var stream = File.Open(scoresFile, FileMode.Open);
			append ("scoresFile1");
			var bin = new BinaryFormatter();
			append ("scoresFile2");
			var gd = (GameData)bin.Deserialize(stream);
			append ("scoresFile3");
			return gd;
		}
		catch (IOException ex) {
			append ("Couldn’t load GameData." + " Exception: " + ex.Message);

			Debug.LogWarning("Couldn’t load GameData." + " Exception: " + ex.Message);
			return new GameData();
		}
	}

	public void saveGameData(GameData gameData) 
	{
		append ("Save1");
		var bFormatter = new BinaryFormatter();
		var filePath = Application.persistentDataPath +
			"/" + "gameData.dat";
		using (var file = File.Open(filePath, FileMode.Create)) {
			bFormatter.Serialize(file, gameData);
		}
		append ("Save2");
	}

	public void append(string txt)
	{
		debugText = debugText + txt;
		debugText = debugText + "\n";
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
		string text = debugText;
		GUI.Label(rect, text, style);
	}
}
