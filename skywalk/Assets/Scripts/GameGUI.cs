using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviour {

	public static GameGUI Instance = null;

	public Text diamondCount;
	public Text distanceCount;

	public float uiMoveSpeed;
	public float alphaChangeSpped;

	public int lifeCost = 0;

	public MenuPanel menuPanel;
	public AlertPanel alertPanel;
	public TutorialPanel tutorialPanel;

	// Use this for initialization
	void Awake () {

		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy(gameObject);
		}

		GameManager.onMenu += onMenu;
		GameManager.onGameOver += onGameOver;
		GameManager.redCountChanged += redCountChanged;
		GameManager.distanceChanged += distanceChanged;
	}

	void OnDestroy()
	{
		GameManager.onMenu -= onMenu;
		GameManager.onGameOver -= onGameOver;
		GameManager.redCountChanged -= redCountChanged;
		GameManager.distanceChanged -= distanceChanged;
	}

	public void refreshGUI()
	{
		menuPanel.refreshRankings ();
	}

	void redCountChanged ()
	{
		int count = UserData.getRedsCount ();
		diamondCount.text = count.ToString();
	}

	void distanceChanged()
	{
		float distance = GameManager.sharedManager.totalDistance;
		distanceCount.text = distance.ToString("0.00");
	}
		
	public void onMenu ()
	{
		showMenu ();
	}

	public void onGameOver ()
	{
		showMenu ();
	}

	public void showMenu()
	{
		menuPanel.gameObject.SetActive (true);
		menuPanel.refreshRankings ();
	}

	public void showAlert(string message)
	{
		alertPanel.gameObject.SetActive (true);
		alertPanel.messageText.text = message;
	}

	public void hideAlert()
	{
		alertPanel.gameObject.SetActive (false);
	}
		
	void moveButtonToPosition(Button b, Vector3 p)
	{
		b.transform.position = p;
	}

	void lerpButtonToPosition (Button b, Vector3 p)
	{
		b.transform.position = Vector3.Lerp(b.transform.position,
			p, Time.deltaTime * uiMoveSpeed);
	}

	void lerpTextAlpha (Text t, float a)
	{
		Color newColor = t.color;
		newColor.a = Mathf.Lerp (newColor.a, a, Time.deltaTime * alphaChangeSpped);
		t.color = newColor;
	}

	void Update()
	{

	}

	public void playDemo()
	{
		EveryPlayHelper.Instance.playDemo ();
	}

	public void restartGame()
	{
		int count = UserData.getRedsCount ();
		if (count >= lifeCost) {
			UserData.addRedsCount (- lifeCost);
			GameManager.sharedManager.playNewGame ();
		} else {
			EveryPlayHelper.Instance.append ("Not enough Reds.");
		}
	}

	public void showVideo()
	{
		SCAds.ShowRewardedAd (GameManager.sharedManager);
	}

	public void watchReplay()
	{
		EveryPlayHelper.Instance.playLastRecording ();
	}

	public void showTutorial()
	{
		tutorialPanel.gameObject.SetActive (true);
	}
}
