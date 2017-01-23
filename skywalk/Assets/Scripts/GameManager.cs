using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager sharedManager = null;

	public enum GameState {tutorial, playing, menu, gameover, willStart};

	public delegate void GameStateChange();
	public static event GameStateChange onTutotial;
	public static event GameStateChange onGamePlay;
	public static event GameStateChange onMenu;
	public static event GameStateChange onGameOver;
	public static event GameStateChange onWillStart;

	public GameState gameState = GameState.menu;

	public delegate void RefreshUI();
	public static event RefreshUI redCountChanged;
	public static event RefreshUI distanceChanged;

	public float totalDistance = 0f;
	private int redsCollectedThisRound = 0;


	void Awake()
	{
		if (sharedManager == null) 
		{
			sharedManager = this;
		} else if (sharedManager != this) {
			Destroy(gameObject);
			return;
		}

		this.transform.parent = null;

		DontDestroyOnLoad(gameObject);

		gameState = GameState.menu;
		Debug.Log ("Game manager awake");
	}

	void Start () 
	{
		Debug.Log("GameManager started");
		notifyStateListener();
	}

	void notifyStateListener()
	{
		switch(gameState)
		{
		case GameState.menu: 
			Debug.Log ("Menu");
			onMenu ();
			break;
		case GameState.playing:
			Debug.Log ("playing");
			onGamePlay ();
			break;
		case GameState.gameover:
			Debug.Log ("gameover");
			onGameOver ();
			break;
		case GameState.willStart:
			Debug.Log ("willStart");
			onWillStart ();
			break;
		default:
			Debug.Log ("tutorial");
			onTutotial ();
			break;
		}
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

	public void playerFailed(bool forced)
	{
		Debug.Log ("player failed");

		gameState = GameState.gameover;

		SCAnalytics.logGameOverEvent (totalDistance, redsCollectedThisRound);
		UserData.updateBestDistance (totalDistance);

		excuateInSeconds (enterMenuMode, 6f);
	}

	void enterMenuMode()
	{
		gameState = GameState.menu;
		notifyStateListener ();
	}

	public void collectedRed()
	{
		redsCollectedThisRound = redsCollectedThisRound + 1;
		UserData.addRedsCount (1);
		redCountChanged ();
	}

	public void playerMoved(float distance)
	{
		Debug.Log ("player moved");
		totalDistance = totalDistance + distance;
		distanceChanged ();
	}

	public void pauseGame()
	{
		Time.timeScale = 0f;
	}

	public void resumeGame()
	{
		excuateInSeconds (doResume, 0.2f);
	}

	public void playNewGame()
	{
		Debug.Log ("Play new game");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		excuateInSeconds (doPlayNewGame, 1f);
	}

	void doPlayNewGame()
	{
		gameState = GameState.playing;
		doResume ();
	}

	void doResume()
	{
		Time.timeScale = 1.0f;
	}

	public void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			playNewGame ();
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
