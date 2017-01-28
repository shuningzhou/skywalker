using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Row : MonoBehaviour {

	public Text rankText;
	public Text nameText;
	public Text scoreText;

	public string rank;
	public string userName;
	public string score;

	// Use this for initialization
	void Start () {
		
	}

	public void refreshUI()
	{
		rankText.text = "# " + rank;
		nameText.text = userName;
		scoreText.text = score;

//		Debug.Log ("1 ROW: " + userName);
//		Debug.Log ("2 ROW: " + UserData.getUserName ());
		if (userName == UserData.getUserName ()) 
		{
			nameText.text = "YOU";
			rankText.color = Color.yellow;
			nameText.color = Color.yellow;
			scoreText.color = Color.yellow;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
