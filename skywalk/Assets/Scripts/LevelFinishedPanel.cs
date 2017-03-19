using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class LevelFinishedPanel : SOPanel {

	public int percentageScore;
	public int starRating;
	public int reward;
	public Text scoreText;
	public Text rewardText;
	public Text bestText;
	public int level;
	public int currentPercentage;
	public int currentRating;
	public int currentReward;
	float percentageDelay = 0.001f;
	float rewardDelay= 0.001f;
	float ratingDealy= 0.3f;
	public float gemProgressMaxWidth = 0f;
	public string scoreString;

	public Image star1;
	public Image star2;
	public Image star3;
	public Image gemProgress;

	public Text levelText;

	public int skipEffect = 0;
	public Button doubleButton;
	public Button continueButton;

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
		if (level == 62) {
			continueButton.gameObject.SetActive (false);
		} else {
			continueButton.gameObject.SetActive (true);
		}

		if (SCAds.rewardedVideoReady ()) {
			doubleButton.gameObject.SetActive (true);
		} else {
			doubleButton.gameObject.SetActive (false);
		}

		scoreText.text = "0%";

		updateBestText ();

		if (LevelManager.sharedManager.currentLevel.level == 5) {
			GameGUI.Instance.levitationSkillStatus.skill.unlockSkill ();
		}

		if (LevelManager.sharedManager.currentLevel.level == 2) {
			GameGUI.Instance.magnetSkillStatus.skill.unlockSkill ();
		}

		if (LevelManager.sharedManager.currentLevel.level == 14) {
			GameGUI.Instance.growthSkillStatus.skill.unlockSkill ();
		}

		if (LevelManager.sharedManager.currentLevel.level == 39) {
			GameGUI.Instance.hastSkillStatus.skill.unlockSkill ();
		}

		excuateInSeconds(animateRating, 1.1f);
	}

	void updateBestText()
	{
		int oldRating = LevelManager.sharedManager.currentLevel.getLevelRating ();
		if (oldRating > 0) {
			bestText.text = "Best: " + oldRating.ToString () + " stars";
		}

	}

	public void animateRating()
	{
		if (percentageScore == 0) {
			float percentage = GameManager.sharedManager.percentGem();

			starRating = 1;

			if (percentage >= 0.50f) 
			{
				starRating = 2;
			}

			if (percentage >= 0.75f) 
			{
				starRating = 3;
				QuestManager.sharedManager.questStar.collectedCondition ();
			}

			float rewardFloat = (float)(0.15 * (level) * Mathf.Sqrt((level)) + 2);
			reward = 0;

			int oldRating = LevelManager.sharedManager.currentLevel.getLevelRating ();

			if (starRating > oldRating)
			{
				reward = (int)((starRating - oldRating) * rewardFloat);
				LevelManager.sharedManager.currentLevel.saveLevelRating (starRating);
			}

			UserData.addCoinsCount (reward);
			Level.setUserProgressLevel (level);

			percentageScore = (int)(percentage * 100);
		}
		if (currentPercentage != percentageScore) 
		{
			excuateInSeconds (animatePercentageProgressImage, ratingDealy);
		}
		else if (currentRating != starRating) 
		{
			excuateInSeconds (updateStarRatingImage, ratingDealy);
		} else 
		{
			updateBestText ();
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
		int increment = 3;

		if (percentageScore - currentPercentage < increment) {
			increment = 1;
		}

		currentPercentage = currentPercentage + increment;

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
		int increment = 3;

		if (percentageScore - currentPercentage < increment) {
			increment = 1;
		}

		currentReward = currentReward + increment;

		rewardText.text = currentReward.ToString ();

		//SoundManager.Instance.PlayOneShot (SoundManager.Instance.count);

		if (skip == skipEffect) 
		{
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.rewards);
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

	public void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("The ad was successfully shown.");
			UserData.addCoinsCount (reward);
			reward = reward * 2;
			animateReward ();
			break;
		case ShowResult.Skipped:
			Debug.Log ("The ad was skipped before reaching the end.");
			UserData.addCoinsCount (reward);
			reward = reward * 2;
			animateReward ();
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	public void doDoubleReward()
	{
		doubleButton.gameObject.SetActive (false);
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		SCAds.showDoubleReward (this);
	}
}
