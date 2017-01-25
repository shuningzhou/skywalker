using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour {

	public Row row;
	public Row row1;
	public Row row2;
	public Row row3;
	public Row row4;
	public Row row5;

	public Text playerRankingText;
	public Text userIDText;
	public InputField userIdField;

	public RankData rd;
	public RankData rd1;
	public RankData rd2;
	public RankData rd3;
	public RankData rd4;
	public RankData rd5;

	// Use this for initialization
	void Start () {
		refreshRankings ();
	}

	public void refreshRankings()
	{
		List<RankData> rankDatas = App42Helper.Instance.rankDatas;
		row.rank = rankDatas[0].userRank;
		row.userName = rankDatas[0].userName;
		row.score = rankDatas[0].userScore;
		row.refreshUI ();

		row1.rank = rankDatas[1].userRank;
		row1.userName = rankDatas[1].userName;
		row1.score = rankDatas[1].userScore;
		row1.refreshUI ();

		row2.rank = rankDatas[2].userRank;
		row2.userName = rankDatas[2].userName;
		row2.score = rankDatas[2].userScore;
		row2.refreshUI ();

		row3.rank = rankDatas[3].userRank;
		row3.userName = rankDatas[3].userName;
		row3.score = rankDatas[3].userScore;
		row3.refreshUI ();

		row4.rank = rankDatas[4].userRank;
		row4.userName = rankDatas[4].userName;
		row4.score = rankDatas[4].userScore;
		row4.refreshUI ();

		row5.rank = rankDatas[5].userRank;
		row5.userName = rankDatas[5].userName;
		row5.score = rankDatas[5].userScore;
		row5.refreshUI ();

		string rankString = "";

		if (App42Helper.Instance.userRanking.Length == 0) {
			rankString = "You are not ranked.";
		} else {
			rankString = "You are ranked #" + App42Helper.Instance.userRanking.ToString () + " in the world!";
		}
		playerRankingText.text = rankString;

		Debug.Log ("Refresh Rankings " + App42Helper.Instance.userName);
		userIdField.text = App42Helper.Instance.userName;
		//userIDText.text = App42Helper.Instance.userName;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void sharePressed()
	{
	}

	public void PurchasePressed()
	{
	}

	public void startPressed()
	{
		GameGUI.Instance.restartGame ();
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

public class RankData
{
	public string userRank;
	public string userName;
	public string userScore;
}
