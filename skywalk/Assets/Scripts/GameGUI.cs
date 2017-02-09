using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameGUI : MonoBehaviour {

	public static GameGUI Instance = null;

	public Text diamondCount;
	public Text distanceCount;

	public float uiMoveSpeed;
	public float alphaChangeSpped;

	public int lifeCost = 0;
	public float tokenGenPeriod = 30.0f;

	public MenuPanel menuPanel;
	public AlertPanel alertPanel;
	public RevivePanel revivePanel;
	public TutorialPanel tutorialPanel;
	public LeaderBoardPanel leaderBoardPanel;
	public StorePanel storePanel;
	public LevelFinishedPanel winPanel;

	public Image newTokenImage;
	public Image tokenImage;
	public enum TokenImageState {growing, moving};
	public TokenImageState tokenImageState;

	public Vector3 origionalNewTokenImagePosition;
	public Vector3 tokenImagePosition;

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
		origionalNewTokenImagePosition = newTokenImage.rectTransform.position;
		tokenImagePosition = tokenImage.rectTransform.position;
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
		//menuPanel.refreshRankings ();
		leaderBoardPanel.refreshGUI ();
		redCountChanged ();
		distanceChanged ();
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
		distanceCount.gameObject.SetActive (false);
		menuPanel.gameObject.SetActive (true);
		//menuPanel.refreshRankings ();
	}

	public void hideMenu()
	{
		distanceCount.gameObject.SetActive (true);
		menuPanel.gameObject.SetActive (false);
	}

	public void showRevive()
	{
		revivePanel.gameObject.SetActive (true);
	}

	public void hideRevive()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		revivePanel.gameObject.SetActive (false);
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

	void Start()
	{
		redCountChanged ();
	}

	void Update()
	{
		switch(tokenImageState)
		{
		case TokenImageState.growing: 
			Vector2 currentSize = newTokenImage.rectTransform.sizeDelta;
			if (currentSize.x >= 30.0f) {
				tokenImageState = TokenImageState.moving;
			} else {
				Vector2 newSize = new Vector2 (currentSize.x + tokenGenPeriod * Time.deltaTime, currentSize.y + tokenGenPeriod * Time.deltaTime);
				newTokenImage.rectTransform.sizeDelta = newSize;
			}
			break;

		case TokenImageState.moving:
			Vector3 currentPosition = newTokenImage.rectTransform.position;
			
			if (currentPosition.y >= tokenImagePosition.y) 
			{
				tokenImageState = TokenImageState.growing;
				newTokenImage.rectTransform.sizeDelta =  new Vector2 (0f, 0f);
				newTokenImage.rectTransform.position = origionalNewTokenImagePosition;
				UserData.addRedsCount (1);
				redCountChanged ();

			} else {
				newTokenImage.rectTransform.position = new Vector3 (currentPosition.x, currentPosition.y + 120.0f * Time.deltaTime, currentPosition.z);
			}

			break;

		default:
			Debug.Log ("WTF");
			break;
		}
	}

	public void restartGame()
	{
		GameManager.sharedManager.playNewGame ();
	}

	public void showVideo()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		hideRevive ();
		SCAds.ShowRewardedAd (GameManager.sharedManager);
	}

	public void useToken()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		hideRevive ();

		if (UserData.getRedsCount () >= 100) {
			UserData.addRedsCount (-100);
			redCountChanged ();
		}

		GameManager.sharedManager.revivePlayer ();
	}

	public void skipRevive()
	{
		hideRevive ();
		GameManager.sharedManager.playerFailed (true);
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
	}

	public void showTutorial()
	{
		tutorialPanel.gameObject.SetActive (true);
	}

	public void hideTutorial()
	{
		Debug.Log ("Hide tutorial");
		tutorialPanel.gameObject.SetActive (false);
		GameManager.sharedManager.tutorialUserTapped ();
	}

	public void showLeaderBoard()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		leaderBoardPanel.gameObject.SetActive (true);
		App42Helper.Instance.getTop10Rankers ();
		hideMenu ();
	}

	public void hideLeaderBoard()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		leaderBoardPanel.gameObject.SetActive (false);
		showMenu ();
	}
	 
	public void showStorePanel()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		storePanel.gameObject.SetActive (true);
		hideMenu ();
		distanceCount.gameObject.SetActive (false);
	}

	public void hideStorePanel()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		Animator a = storePanel.GetComponent<Animator> ();
		a.SetBool ("shouldClose", true);
		excuateInSeconds (deactiveStorePanle, 1.5f);
	}

	public void deactiveStorePanle()
	{
		storePanel.gameObject.SetActive (false);
		showMenu ();
	}

	public void showWinPanel(int starRating, int reward, int level)
	{
		winPanel.gameObject.SetActive (true);
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
		showMenu ();
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
