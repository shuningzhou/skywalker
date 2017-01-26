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

	public delegate void PlayerPowerUp();
	public static event PlayerPowerUp onPowerUp;

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
		//App42Helper.Instance.createGuestUser ();
		if (UserData.getUserName ().Length == 0)
		{
			App42Helper.Instance.createGuestUser ();
		}

		App42Helper.Instance.getUserRanking();
		App42Helper.Instance.getTop6Score ();
		//App42Helper.Instance.getUserRanking();
		//App42Helper.Instance.getTop5Score();
	}

	void notifyStateListener()
	{
		switch(gameState)
		{
		case GameState.menu: 
			Debug.Log ("Menu");
			if (onMenu != null) {
				onMenu ();
			}
			break;
		case GameState.playing:
			Debug.Log ("playing");
			if (onGamePlay != null) {
				onGamePlay ();
			}
			break;
		case GameState.gameover:
			Debug.Log ("gameover");
			if (onGameOver != null) {
				onGameOver ();
			}
			break;
		case GameState.willStart:
			Debug.Log ("willStart");
			if (onWillStart != null) {
				onWillStart ();
			}
			break;
		default:
			Debug.Log ("tutorial");
			if (onTutotial != null) {
				onTutotial ();
			}
			break;
		}
	}

	public void test()
	{
		onPowerUp ();
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
		App42Helper.Instance.uploadScoreForUser (totalDistance);

		excuateInSeconds (enterMenuMode, 6f);
		totalDistance = 0;
	}

	void enterMenuMode()
	{
		gameState = GameState.menu;
		notifyStateListener ();
		App42Helper.Instance.getTop6Score ();
		App42Helper.Instance.getUserRanking ();
	}

	public void collectedRed()
	{
		redsCollectedThisRound = redsCollectedThisRound + 1;
		UserData.addRedsCount (1);
		redCountChanged ();
	}

	public void playerMoved(float distance)
	{
		totalDistance = totalDistance + distance;
		distanceChanged ();
	}
		
	public void resumeGame()
	{
		excuateInSeconds (doResume, 0.2f);
	}

	public void playNewGame()
	{
		Debug.Log ("Play new game");
		gameState = GameState.willStart;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		excuateInSeconds (doPlayNewGame, 1f);
	}

	void doPlayNewGame()
	{
		gameState = GameState.tutorial;
		notifyStateListener ();
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

	public void tutorialTriggered ()
	{
		Debug.Log ("Tutorial triggered");
		GameGUI.Instance.showTutorial ();

		CharacterMovement cm = FindObjectOfType<CharacterMovement> ();
		cm.inTutorial = true;
	}

	public void tutorialUserTapped ()
	{
		CharacterMovement cm = FindObjectOfType<CharacterMovement> ();
		cm.inTutorial = false;
		cm.doTurn ();
	}
}
