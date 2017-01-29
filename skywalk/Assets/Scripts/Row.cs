using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Row : MonoBehaviour {

	public Text rankText;
	public Text nameText;
	public Text scoreText;
	public Image rankImage;

	public string rank;
	public string userName;
	public float score;

	// Use this for initialization
	void Start () {
		
	}

	public void refreshUI()
	{
		if (rank == "1") {
			rank = rank + "st";
		} else if (rank == "2") {
			rank = rank + "nd";
		} else if (rank == "3") {
			rank = rank + "rd";
		} else {
			rank = rank + "th";
		}
		rankText.text = rank;
		nameText.text = userName;
		scoreText.text = score.ToString("0.00");

		updateRankImage ();
////		Debug.Log ("1 ROW: " + userName);
////		Debug.Log ("2 ROW: " + UserData.getUserName ());
//		if (userName == UserData.getUserName ()) 
//		{
//			nameText.text = "YOU";
//			rankText.color = Color.yellow;
//			nameText.color = Color.yellow;
//			scoreText.color = Color.yellow;
//		}
	}

	public void updateRankImage()
	{
		Sprite sprite = RankImage.Instance.getRankSpriteForScore (score);
		rankImage.sprite = sprite;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
