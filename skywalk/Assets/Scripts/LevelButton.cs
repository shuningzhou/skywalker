using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
	public Image starImage;
	public Image lockImage;
	public Text levelText;

	public Sprite star0;
	public Sprite star1;
	public Sprite star2;
	public Sprite star3;

	public Level level;
	public int levelIndex;

	// Use this for initialization
	void Start () {
		this.level = LevelManager.sharedManager.levels [levelIndex];
		int userProgressLevel = Level.getUerProgressLevel ();
		Debug.Log ("LevelButton for level " + level.level.ToString () + ", User progress is " + userProgressLevel.ToString ());

		if (userProgressLevel < levelIndex) 
		{
			lockImage.gameObject.SetActive (true);
			starImage.gameObject.SetActive (false);
			levelText.gameObject.SetActive (false);
		} 
		else 
		{
			int rating = this.level.getLevelRating ();

			lockImage.gameObject.SetActive (false);
			starImage.gameObject.SetActive (true);
			levelText.gameObject.SetActive (true);

			if (rating == 0) 
			{
				starImage.sprite = star0;
			} 
			else if (rating == 1) 
			{
				starImage.sprite = star1;
			} 
			else if (rating == 2) 
			{
				starImage.sprite = star2;
			} 
			else 
			{
				starImage.sprite = star3;
			}

			levelText.text = this.level.level.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
