﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviour {

	public Button pauseButton;
	public Button resumeButton;
	public Button restartButton;
	public Text ifYouFail;
	public Text diamondCount;
	public GameObject tipPanel;
	public GameManager gameManager;

	public float uiMoveSpeed;
	public float alphaChangeSpped;
	public float ifYouFailAlpha = 0f;

	private Vector3 restartButtonPosition;

	// Use this for initialization
	void Start () {
		restartButtonPosition = restartButton.transform.position;
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
		newColor.a = Mathf.Lerp (newColor.a, a, Time.deltaTime * uiMoveSpeed);
		t.color = newColor;
	}

	void Update()
	{
		lerpButtonToPosition (restartButton, restartButtonPosition);
		lerpTextAlpha (ifYouFail, ifYouFailAlpha);
	}

	public void pauseGame()
	{
		Debug.Log ("Pause");
		Time.timeScale = 0f;
		showPause (false);
		gameManager.pauseGame ();
	}

	public void resumeGame()
	{
		Debug.Log ("resume");
		Time.timeScale = 1f;
		showPause (true);
		gameManager.resumeGame ();
	}

	public void playerFailed()
	{
		hideBothPause ();
		ifYouFailAlpha = 1;
		restartButtonPosition.y = ifYouFail.transform.position.y - (float)(Screen.height / 10);
	}

	public void restartGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void showTipPanel()
	{
		tipPanel.SetActive (true);
		hideBothPause ();
	}

	public void hideTipPanel()
	{
		tipPanel.SetActive (false);
		gameManager.gameStart ();
		showPause (true);
	}

	void showPause(bool show)
	{
		pauseButton.gameObject.SetActive(show);
		resumeButton.gameObject.SetActive(!show);
	}

	void hideBothPause()
	{
		pauseButton.gameObject.SetActive(false);
		resumeButton.gameObject.SetActive(false);
	}

	public void setDiamond(int count)
	{
		diamondCount.text = count.ToString();
	}

}