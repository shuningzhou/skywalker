using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviour {

	public Text diamondCount;
	public Text distanceCount;

	public float uiMoveSpeed;
	public float alphaChangeSpped;

	public int lifeCost = 20;

	public MenuPanel menuPanel;

	private List<GameObject> planes = new List<GameObject>();

	// Use this for initialization
	void Awake () {

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
}
