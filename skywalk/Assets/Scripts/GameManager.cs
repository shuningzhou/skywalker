using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using UnityEngine.SceneManagement;

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

	public string PREF_COLLECTED = "collected";
	public string PREF_DISTANCE = "distance";
	public string PREF_DATE = "date";

	// Use this for initialization
	void Awake()
	{

	}

	void Start () 
	{
		diamondCount = PlayerPrefs.GetInt(PREF_COLLECTED);

		if (diamondCount < 20) {
			playerFailed (true);
		} else {
			PlayerPrefs.SetInt (PREF_COLLECTED, diamondCount - 20);
			PlayerPrefs.Save();

			diamondCount = PlayerPrefs.GetInt(PREF_COLLECTED);

			if (shouldShowTip) {
				gameGUI.showTipPanel ();
			} else {
				gameStart ();
			}

			gameGUI.setDiamond (diamondCount);
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
	
		EveryPlayHelper.Instance.startRecording ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public bool isGameOver()
	{
		return gameOver;
	}

	public void playerFailed(bool forced)
	{
		characterMovement.doGameEnd ();

		StartCoroutine (doFailed (forced));
	}

	IEnumerator doFailed(bool forced)
	{
		yield return new WaitForSeconds(3);
		gameOver = true;
		cameraMovement.playerFailed (forced);
		SCAnalytics.logGameOverEvent (totalDistance, diamondCountThisRound);

		PlayerPrefs.SetInt (PREF_COLLECTED, diamondCount);
		PlayerPrefs.Save();

		if (totalDistance > PlayerPrefs.GetInt (PREF_DISTANCE)) 
		{
			PlayerPrefs.SetString (PREF_DATE, DateTime.Now.ToString ());
			PlayerPrefs.SetFloat (PREF_DISTANCE, totalDistance);
			PlayerPrefs.Save();
		}

		gameGUI.playerFailed (diamondCount);

		EveryPlayHelper.Instance.stopRecording ();
		EveryPlayHelper.Instance.setDemo ();
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
			PlayerPrefs.SetInt (PREF_COLLECTED, diamondCount + 20);
			PlayerPrefs.Save();
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
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
}
