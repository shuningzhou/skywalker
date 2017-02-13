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

	public Image gemProgress;
	public Text progressText;

	private float gemProgressMaxWidth;

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

		gemProgressMaxWidth = gemProgress.rectTransform.localScale.x;
	}

	void GameManager_onGamePlay ()
	{
		
	}

	void Start()
	{
		gemProgress.rectTransform.localScale = new Vector2 (0 , gemProgress.rectTransform.localScale.y);
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
		float percentage = GameManager.sharedManager.percentGem();
		showWinPanel (percentage, LevelManager.sharedManager.currentLevel.level);
	}
		
	void GameManager_percentageChanged()
	{
//		float distance = GameManager.sharedManager.totalDistance;
//		distanceCount.text = distance.ToString("0.00");
		float percentage = GameManager.sharedManager.percentGem();
		gemProgress.rectTransform.localScale = new Vector2 (gemProgressMaxWidth * percentage , gemProgress.rectTransform.localScale.y);
		string message = (percentage * 100).ToString ("0") + "%";
		Debug.Log (message);
		if (progressText == null) {
			Debug.Log ("wtf");
		} else {
			progressText.text = message;
		}

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
		if (LevelManager.sharedManager.currentLevel.level == 1) {
			tutorialPanel.show(false);
		} else {
			readyPanel.show(false);
		}
	}

	public void showWinPanel(float percentage, int level)
	{
		winPanel.show(false);

		int starRating = 0;
		int reward = 0;

		Debug.Log (percentage);

		if (percentage > 0.33f) 
		{
			starRating = 2;
			reward = 10;
		}

		if (percentage > 0.66f) 
		{
			starRating = 3;
			reward = 15;
		}

		winPanel.starRating = starRating;
		winPanel.scoreString = (percentage * 100).ToString ("0") + "%";
		winPanel.reward = reward;
		winPanel.level = level;
		winPanel.updateUI ();
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
