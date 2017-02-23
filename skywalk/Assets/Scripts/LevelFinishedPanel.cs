using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishedPanel : SOPanel {

	public int percentageScore;
	public int starRating;
	public int reward;
	public Text scoreText;
	public Text rewardText;
	public int level;
	public int currentPercentage;
	public int currentRating;
	public int currentReward;
	public float percentageDelay;
	public float rewardDelay;
	public float ratingDealy;
	public float gemProgressMaxWidth = 0f;
	public string scoreString;

	public Image star1;
	public Image star2;
	public Image star3;
	public Image gemProgress;

	public Text levelText;

	public int skipEffect = 0;

	private int skip = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
		
	public void resetAll()
	{
		level = 0;
		starRating = 0;
		reward = 0;
		currentPercentage = 0;
		currentRating = 0;
		currentReward = 0;
		rewardText.text = "0";
		levelText.text = "";
		scoreString = "";
		star1.gameObject.SetActive (true);
		star2.gameObject.SetActive (true);
		star3.gameObject.SetActive (true);
	}

	public void updateUI()
	{
		UserData.addCoinsCount (reward);
		levelText.text = "Level " + level.ToString ();
		scoreText.text = scoreString;
		excuateInSeconds(animateRating, 1.1f);
	}

	public void animateRating()
	{
		if (currentPercentage != percentageScore) 
		{
			excuateInSeconds (animatePercentageProgressImage, ratingDealy);
		}
		else if (currentRating != starRating) 
		{
			excuateInSeconds (updateStarRatingImage, ratingDealy);
		} else 
		{
			excuateInSeconds (animateReward, ratingDealy);
		}
	}

	void animatePercentageProgressImage()
	{
		if (currentPercentage != percentageScore) {
			excuateInSeconds (updatePercentageProgressImage, percentageDelay);
		} else {
			animateRating ();
		}
	}
		
	void updatePercentageProgressImage()
	{
		currentPercentage = currentPercentage + 1;

		scoreText.text = currentPercentage.ToString () + "%";

		if (skip == skipEffect) 
		{
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.count);
			skip = 0;
		} else {
			skip++;
		}

		float percentage = ((float)(percentageScore - currentPercentage)) / 100.0f;

		gemProgress.rectTransform.localScale = new Vector2 (gemProgressMaxWidth * percentage , gemProgress.rectTransform.localScale.y);

		animatePercentageProgressImage ();
	}


	void updateStarRatingImage()
	{
		currentRating = currentRating + 1;

		if (currentRating == 1) {
			star1.gameObject.SetActive (true);
		}
		else if (currentRating == 2) {
			star2.gameObject.SetActive (true);
		}
		else {
			star3.gameObject.SetActive (true);
		}

		SoundManager.Instance.PlayOneShot(SoundManager.Instance.rating);

		animateRating ();
	}

	public void animateReward()
	{
		if (currentReward!=reward)
		{
			excuateInSeconds (updateReward, rewardDelay);
		}
	}

	void updateReward()
	{
		currentReward = currentReward + 1;

		rewardText.text = currentReward.ToString ();

		//SoundManager.Instance.PlayOneShot (SoundManager.Instance.count);

		if (skip == skipEffect) 
		{
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.count);
			skip = 0;
		} else {
			skip++;
		}

		animateReward ();
	}

	public void replayed()
	{
		excuateInSeconds (doReplayed, 1f);
	}

	public void homed()
	{
		excuateInSeconds (doHomed, 1f);
	}

	public void continued()
	{
		excuateInSeconds (doContinued, 1f);
	}

	public void doReplayed()
	{
		LevelManager.sharedManager.playCurrentLevel ();
	}

	public void doHomed()
	{
		LevelManager.sharedManager.currentLevelFinishedAndRetured ();
	}

	public void doContinued()
	{
		LevelManager.sharedManager.playNextLevel ();
	}
}
