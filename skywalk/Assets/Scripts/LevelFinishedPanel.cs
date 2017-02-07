using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelFinishedPanel : MonoBehaviour {

	public int starRating;
	public int reward;
	public Text rewardText;
	public int level;
	public int currentRating;
	public int currentReward;
	public float rewardDelay = 0.2f;
	public float ratingDealy = 0.7f;

	public Image star1;
	public Image star2;
	public Image star3;

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
		rewardText.text = "0";
		star1.gameObject.SetActive (true);
		star2.gameObject.SetActive (true);
		star3.gameObject.SetActive (true);
	}

	public void updateUI()
	{
		excuateInSeconds(animateRating, 1.1f);
	}

	public void animateRating()
	{
		if (currentRating != starRating) 
		{
			excuateInSeconds (updateStarRatingImage, ratingDealy);
		} else 
		{
			excuateInSeconds (animateReward, ratingDealy);
		}
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

		if (true){//skip == skipEffect) {
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
