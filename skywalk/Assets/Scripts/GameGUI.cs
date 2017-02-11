using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameGUI : MonoBehaviour {

	public static GameGUI Instance = null;

	public AlertPanel alertPanel;
	public RevivePanel revivePanel;
	public TutorialPanel tutorialPanel;
	public LevelFinishedPanel winPanel;
	public ReadyPanel readyPanel;
	public GameOverPanel gameOverPanel;

	// Use this for initialization
	void Awake () {

		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}
			
		GameManager.onGameOver += GameManager_onGameOver;
		GameManager.onGameRevive += GameManager_onGameRevive;
		GameManager.onGameWon += GameManager_onGameWon;
		GameManager.onGamePlay += GameManager_onGamePlay;

		GameManager.coinCountChanged += GameManager_coinCountChanged;
		GameManager.percentageChanged += GameManager_percentageChanged;
	}

	void GameManager_onGamePlay ()
	{
		
	}

	void OnDestroy()
	{
		GameManager.onGameOver -= GameManager_onGameOver;
		GameManager.onGameRevive -= GameManager_onGameRevive;
		GameManager.onGameWon -= GameManager_onGameWon;
		GameManager.onGamePlay -= GameManager_onGamePlay;

		GameManager.coinCountChanged -= GameManager_coinCountChanged;
		GameManager.percentageChanged -= GameManager_percentageChanged;
	}

	void GameManager_coinCountChanged ()
	{
//		int count = UserData.getRedsCount ();
//		diamondCount.text = count.ToString();
	}

	void GameManager_onGameRevive()
	{
		showRevive ();
	}

	void GameManager_onGameWon()
	{
		showWinPanel (0,0,0);
	}
		
	void GameManager_percentageChanged()
	{
//		float distance = GameManager.sharedManager.totalDistance;
//		distanceCount.text = distance.ToString("0.00");
	}

	public void GameManager_onGameOver ()
	{
		gameOverPanel.show (false);
	}

	public void showRevive()
	{
		revivePanel.show (false);
	}
		
	public void showAlert(string message)
	{
		alertPanel.gameObject.SetActive (true);
		alertPanel.messageText.text = message;
	}

	public void hideAlert()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		alertPanel.gameObject.SetActive (false);
	}
		
	public void useCoin()
	{

	}

	public void showTutorial()
	{
		if (true) {
			tutorialPanel.show(false);
		} else {
			readyPanel.show(false);
		}
	}

	public void showWinPanel(int starRating, int reward, int level)
	{
		winPanel.show(false);
		winPanel.starRating = starRating;
		winPanel.reward = reward;
		winPanel.level = level;
		winPanel.updateUI ();
	}

	public void hideWinPanel()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		winPanel.gameObject.SetActive (false);
		winPanel.resetAll ();
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
}
