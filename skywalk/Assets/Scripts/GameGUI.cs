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

	public UnlockPanel unlockLevitationPanel;
	public UnlockPanel unlockHastePanel;
	public UnlockPanel unlockGrowthPanel;
	public UnlockPanel unlockMagnetPanel;

	public GameOverPanel gameOverPanel;

	public Image gemProgress;
	public Text progressText;

	public SkillStatus hastSkillStatus;
	public SkillStatus growthSkillStatus;
	public SkillStatus levitationSkillStatus;
	public SkillStatus magnetSkillStatus;

	public Skill magnet;
	public Skill levitation;
	public Skill growth;
	public Skill haste;

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
		showWinPanel (LevelManager.sharedManager.currentLevel.level);
	}
		
	void GameManager_percentageChanged()
	{
		float percentage = GameManager.sharedManager.percentGem();
		gemProgress.rectTransform.localScale = new Vector2 (gemProgressMaxWidth * percentage , gemProgress.rectTransform.localScale.y);
	}

	public void GameManager_onGameOver ()
	{
		gameOverPanel.resetUI ();

		gameOverPanel.show (false);
		float percentage = GameManager.sharedManager.percentGem();
		gameOverPanel.scoreString = (percentage * 100).ToString ("0") + "%";

		gameOverPanel.updateUI ();
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
		if (LevelManager.sharedManager.currentLevel.level == 1) 
		{
			tutorialPanel.show(false);
		} 
		else if (LevelManager.sharedManager.currentLevel.level == 3 && magnet.info.unlockShown == 0 )
		{
			unlockMagnetPanel.show (false);
			magnet.showSkill ();
		}
		else if (LevelManager.sharedManager.currentLevel.level == 6 && levitation.info.unlockShown == 0 )
		{
			unlockLevitationPanel.show (false);
			levitation.showSkill ();
		}
		else if (LevelManager.sharedManager.currentLevel.level == 15 && growth.info.unlockShown == 0 )
		{
			unlockGrowthPanel.show (false);
			growth.showSkill ();
		}
		else if (LevelManager.sharedManager.currentLevel.level == 40 && haste.info.unlockShown == 0 )
		{
			unlockHastePanel.show (false);
			haste.showSkill ();
		}
		else
		{
			readyPanel.show(false);
		}

		if (magnet.info.isLocked == 0) 
		{
			magnetSkillStatus.gameObject.SetActive (true);
		}

		if (levitation.info.isLocked == 0) 
		{
			levitationSkillStatus.gameObject.SetActive (true);
		}

		if (growth.info.isLocked == 0) 
		{
			growthSkillStatus.gameObject.SetActive (true);
		}

		if (haste.info.isLocked == 0) 
		{
			hastSkillStatus.gameObject.SetActive (true);
		}
	}

	public void showWinPanel(int level)
	{
		winPanel.show(false);
		winPanel.gemProgressMaxWidth = gemProgressMaxWidth;
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
