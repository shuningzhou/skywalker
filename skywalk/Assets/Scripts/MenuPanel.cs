using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour {

	public Text playerRankingText;
	public Text distanceText;
	public InputField userIdField;
	public Text localBest;
	public Image rankImage;
	public Text title;

	// Use this for initialization
	void Start () {
		refreshRankings ();
	}

	public void refreshRankings()
	{
		string rankString = "";

		if (App42Helper.Instance.userRanking.Length == 0) {
			rankString = "You are not ranked.";
		} else {
			rankString = "You are ranked #" + App42Helper.Instance.userRanking.ToString () + " in the world!";
		}
		playerRankingText.text = rankString;

		userIdField.text = App42Helper.Instance.userName;

		float lastDistance = UserData.getLastDistance ();
		if (lastDistance > 0) {
			distanceText.text = lastDistance.ToString ("0.00");
		} else {
			distanceText.text = "00.00";
		}

		float bestDistance = UserData.getLocalBestRecordValue();

		if (bestDistance > 0f) {
			localBest.text = "BEST " + bestDistance.ToString ("0.00");
		} else {
			localBest.text = "";
		}

		updateRankImage ();
		updateRankTitle ();
		//userIDText.text = App42Helper.Instance.userName;
	}

	public void updateRankImage()
	{
		Sprite sprite = RankImage.Instance.getRankSpriteForScore (UserData.getLocalBestRecordValue());
		rankImage.sprite = sprite;
	}

	public void updateRankTitle()
	{
		string rankTitle = RankImage.Instance.getRankTitleForScore (UserData.getLocalBestRecordValue ());
		title.text = rankTitle;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void sharePressed()
	{
		GameManager.sharedManager.revivePlayer ();
	}

	public void PurchasePressed()
	{
		GameGUI.Instance.showStorePanel ();
	}

	public void startPressed()
	{
		GameGUI.Instance.restartGame ();
	}

	public void showLeaderBoardPressed()
	{
		GameGUI.Instance.showLeaderBoard ();
	}

	public void startForFreePressed()
	{
		GameGUI.Instance.showVideo ();
	}

	public void userNameChanged(string value)
	{
		string newvalue = userIdField.text;

		if (newvalue.Length > 20) {
			GameGUI.Instance.showAlert ("Game ID has to be less than 20 characters");
			userIdField.text = App42Helper.Instance.userName;
		} else if (newvalue.Length < 6) {
			GameGUI.Instance.showAlert ("Game ID has to be more than 6 characters");
			userIdField.text = App42Helper.Instance.userName;
		} else {
			App42Helper.Instance.userName = newvalue;
			App42Helper.Instance.createNewUser ();
		}

		Debug.Log ("User name changed" + userIdField.text);
	}
}
