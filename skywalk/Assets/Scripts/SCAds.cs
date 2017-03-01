using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class SCAds : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	public static void ShowAd()
//	{
//		if (Advertisement.IsReady())
//		{
//			Advertisement.Show();
//		}
//	}

	public static bool rewardedVideoReady()
	{
		return Advertisement.IsReady ("rewardedVideo");
	}

	public static void ShowRewardedAd(GameManager gameManager)
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = gameManager.HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	public static void showDoubleReward(LevelFinishedPanel panel)
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = panel.HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}
}
