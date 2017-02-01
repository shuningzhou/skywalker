using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelFinishedPanel : MonoBehaviour {

	public Text goodText;
	public Image starRatingImage;
	public Text levelText;
	public int starRating;
	public int reward;
	public Text rewardText;
	public int level;
	public int currentRating;
	public int currentReward;
	public float rewardDelay = 0.05f;
	public float ratingDealy = 1f;

	public Sprite star0;
	public Sprite star1;
	public Sprite star2;
	public Sprite star3;

	public int skipEffect = 5;

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
		currentRating = 0;
		currentReward = 0;
		goodText.text = "";
		starRatingImage.sprite = star0;
		rewardText.text = "0";
		levelText.text = "0";
	}

	public void updateUI()
	{
		levelText.text = level.ToString();
		if(starRating == 0)
		{
			goodText.text = "Defeated";
		}
		else if (starRating == 1)
		{
			goodText.text = "Good";
		}
		else if (starRating == 1)
		{
			goodText.text = "Fantastic";
		}
		else
		{
			goodText.text = "Epic";
		}

		animateRating();
	}

	public void animateRating()
	{
		if (currentRating != starRating) {
			excuateInSeconds (updateStarRatingImage, ratingDealy);
		} else {
			animateReward ();
		}
	}

	void updateStarRatingImage()
	{
		currentRating = currentRating + 1;

		if (currentRating == 1) {
			starRatingImage.sprite = star1;
		}
		else if (currentRating == 2) {
			starRatingImage.sprite = star2;
		}
		else {
			starRatingImage.sprite = star3;
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

		if (skip == skipEffect) {
			SoundManager.Instance.PlayOneShot (SoundManager.Instance.count);
			skip = 0;
		} else {
			skip++;
		}


		animateReward ();
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
