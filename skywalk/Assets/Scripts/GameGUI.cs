using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviour {

	public Button restartButton;
	public Button videoButton;
	public Button watchReplayButton;
	public Button demoButton;

	public Text ifYouFail;
	public Text diamondCount;
	public Text distanceCount;

	public GameObject tipPanel;

	public float uiMoveSpeed;
	public float alphaChangeSpped;
	public float ifYouFailAlpha = 0f;

	private Vector3 restartButtonPosition;
	private Vector3 videoButtonPosition;
	private Vector3 watchReplayButtonPosition;
	private Vector3 demoButtonPosition;

	// Use this for initialization
	void Awake () {
		restartButtonPosition = restartButton.transform.position;
		videoButtonPosition = videoButton.transform.position;
		watchReplayButtonPosition = watchReplayButton.transform.position;
		demoButtonPosition = demoButton.transform.position;
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

	void redCountChanged ()
	{
		int count = UserData.getRedsCount ();
		diamondCount.text = count.ToString();
	}

	void distanceChanged()
	{
		float distance = GameManager.sharedManager.totalDistance;
		distanceCount.text = distance.ToString("0.0");
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
		ifYouFailAlpha = 1;
		restartButtonPosition.y = ifYouFail.transform.position.y - (float)(Screen.height / 10);
		videoButtonPosition.y = ifYouFail.transform.position.y - (float)(Screen.height / 10);
		watchReplayButtonPosition.y = ifYouFail.transform.position.y + (float)(Screen.height / 10);
		demoButtonPosition.y = ifYouFail.transform.position.y + (float)(Screen.height / 10);
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
		lerpButtonToPosition (restartButton, restartButtonPosition);
		lerpButtonToPosition (videoButton, videoButtonPosition);
		lerpButtonToPosition (watchReplayButton, watchReplayButtonPosition);
		lerpButtonToPosition (demoButton, demoButtonPosition);

		lerpTextAlpha (ifYouFail, ifYouFailAlpha);
	}

	public void playDemo()
	{
		EveryPlayHelper.Instance.playDemo ();
	}

	public void restartGame()
	{
		int count = UserData.getRedsCount ();
		if (count >= 20) {
			UserData.addRedsCount (-20);
			GameManager.sharedManager.playNewGame ();
		} else {
			EveryPlayHelper.Instance.append ("Not enough Reds.");
		}
	}

	public void showVideo()
	{
		SCAds.ShowRewardedAd (GameManager.sharedManager);
	}

	public void showTipPanel()
	{
		tipPanel.SetActive (true);
		GameManager.sharedManager.pauseGame ();
	}

	public void watchReplay()
	{
		
		EveryPlayHelper.Instance.playLastRecording ();
	}

	public void hideTipPanel()
	{
		tipPanel.SetActive (false);
		GameManager.sharedManager.resumeGame ();
	}
}
