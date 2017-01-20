using System.Collections;
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

	public float uiMoveSpeed;
	public float alphaChangeSpped;

	private Vector3 pauseButtonPosition;
	private Vector3 resumeButtonPosition;
	private Vector3 restartButtonPosition;

	private float ifYouFailAlpha;

	// Use this for initialization
	void Start () {
		pauseButtonPosition = pauseButton.transform.position;
		resumeButtonPosition = resumeButton.transform.position;
		restartButtonPosition = restartButton.transform.position;
		ifYouFailAlpha = ifYouFail.color.a;
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
		Debug.Log ("old a = "  + newColor.a);
		Debug.Log ("new a = " + a);
		newColor.a = Mathf.Lerp (newColor.a, a, Time.deltaTime * uiMoveSpeed);
		t.color = newColor;
	}

	void Update()
	{
		lerpButtonToPosition (restartButton, restartButtonPosition);
		lerpTextAlpha (ifYouFail, ifYouFailAlpha);
	}

	void updateButtonsPosition()
	{
		moveButtonToPosition (pauseButton, pauseButtonPosition);
		moveButtonToPosition (resumeButton, resumeButtonPosition);
	}

	void hideAll()
	{

	}

	public void pauseGame()
	{
		Debug.Log ("Pause");
		Time.timeScale = 0f;
		pauseButtonPosition.x = pauseButtonPosition.x - 60f;
		resumeButtonPosition.x = resumeButtonPosition.x + 60f;
		updateButtonsPosition ();
	}

	public void resumeGame()
	{
		Debug.Log ("resume");
		Time.timeScale = 1f;
		pauseButtonPosition.x = pauseButtonPosition.x + 60f;
		resumeButtonPosition.x = resumeButtonPosition.x - 60f;
		updateButtonsPosition ();
	}

	public void playedFailed()
	{
		pauseButtonPosition.x = pauseButtonPosition.x - 60f;
		updateButtonsPosition ();

		restartButtonPosition.y = restartButtonPosition.y + 250f;

		ifYouFailAlpha = 1f;
	}

	public void restartGame()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void setDiamond(int count)
	{
		diamondCount.text = count.ToString();
	}

}
