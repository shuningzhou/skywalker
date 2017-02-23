using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager sharedManager = null;

	public enum GameState {tutorial, playing, gamefinished, gamewon, gamerevive, gameover};

	public delegate void GameStateChange();
	public static event GameStateChange onTutotial;
	public static event GameStateChange onGamePlay;
	public static event GameStateChange onGameFinished;
	public static event GameStateChange onGameWon;
	public static event GameStateChange onGameRevive;
	public static event GameStateChange onGameOver;

	public GameState gameState = GameState.tutorial;

	public delegate void RefreshUI();
	public static event RefreshUI coinCountChanged;
	public static event RefreshUI percentageChanged;

	public delegate void PlayerPowerUp();
	public static event PlayerPowerUp onPowerUp;

	public int totalGemThisRound = 0;

	private int gemCollectedThisRound = 0;
	private int coinsCollectedThisRound = 0;
	public bool isOnLastTutorialTrigger = false;
	private bool alreadyRevived = false;

	void Awake()
	{
		if (sharedManager == null) 
		{
			sharedManager = this;
		} else if (sharedManager != this) {
			Destroy(gameObject);
			return;
		}

		gameState = GameState.tutorial;

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
		case GameState.playing:
			Debug.Log ("notify playing");
			if (onGamePlay != null) {
				onGamePlay ();
			}
			break;
		case GameState.gamewon:
			Debug.Log ("notify game won");
			if (onGameWon != null) {
				onGameWon ();
			}
			break;
		case GameState.gameover:
			Debug.Log ("notify game over");
			if (onGameOver != null) {
				onGameOver ();
			}
			break;
		case GameState.gamefinished:
			Debug.Log ("notify game finished");
			if (onGameFinished != null) {
				onGameFinished ();
			}
			break;
		case GameState.gamerevive:
			Debug.Log ("notify game revive");
			if (onGameRevive != null) 
			{
				onGameRevive ();
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

	public void excuateInSeconds(Action action, float seconds)
	{
		StartCoroutine (delayStart(seconds, action));
	}

	IEnumerator delayStart(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action ();
	}

	public void playWon ()
	{
		Debug.Log ("Player WON!");

		excuateInSeconds (enterWinMode, 1f);
	}

	public void playerFailed()
	{
		Debug.Log ("player failed");
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.dropped);

		gameState = GameState.gamefinished;
		notifyStateListener ();

		bool adsReady = SCAds.rewardedVideoReady();
		bool hasEnoughCoins = UserData.getCoinsCount () >= 5;

		if (alreadyRevived || ((!adsReady) && (!hasEnoughCoins)))
		{
			excuateInSeconds (enterGameOverMode, 1.5f);
		}
		else 
		{
			excuateInSeconds (enterReviveMode, 1.5f);
			alreadyRevived = true;
		}
	}

	void enterWinMode()
	{
		gameState = GameState.gamewon;
		notifyStateListener ();
	}

	void enterReviveMode()
	{
		gameState = GameState.gamerevive;
		notifyStateListener ();
	}

	void enterGameOverMode()
	{
		gameState = GameState.gameover;
		notifyStateListener ();
	}

	public void collectedGem()
	{
		gemCollectedThisRound = gemCollectedThisRound + 1;

		percentageChanged ();
	}

	public void collectedCoin ()
	{
		coinsCollectedThisRound = coinsCollectedThisRound + 1;

		coinCountChanged ();
	}

	public void coinCountUpdated()
	{
		coinCountChanged ();
	}

	public void revivePlayer()
	{
		CharacterMovement cm = FindObjectOfType<CharacterMovement> ();
		cm.revive ();

		startPlaying ();
	}

	public void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			revivePlayer ();
			break;
		case ShowResult.Skipped:
			Debug.Log ("The ad was skipped before reaching the end.");
			excuateInSeconds (enterGameOverMode, 1f);
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			excuateInSeconds (enterGameOverMode, 1f);
			break;
		}
	}

	public void tutorialTriggered (bool isLastTrigger)
	{
		Debug.Log ("Tutorial triggered");
		GameGUI.Instance.showTutorial ();

		CharacterMovement cm = FindObjectOfType<CharacterMovement> ();
		cm.inTutorial = true;

		isOnLastTutorialTrigger = isLastTrigger;
	}

	public void tutorialUserTapped ()
	{
		excuateInSeconds (doTutorialUserTapped, 0.1f);
	}

	public void doTutorialUserTapped()
	{
		CharacterMovement cm = FindObjectOfType<CharacterMovement> ();
		cm.inTutorial = false;
		cm.doTurn ();

		if (isOnLastTutorialTrigger) {
			startPlaying();
		}
	}

	void startPlaying()
	{
		gameState = GameState.playing;
		notifyStateListener ();

		SoundManager.Instance.PlayOneShot(SoundManager.Instance.gameStarted);
	}

	public float percentGem()
	{
		return (float)gemCollectedThisRound / (float)totalGemThisRound;
	}
}
