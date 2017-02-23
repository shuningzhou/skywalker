using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePanel : SOPanel {

	public int reviveCost = 5;

	// Use this for initialization
	void Start () {
		int coins = UserData.getCoinsCount ();

		Debug.Log ("Coins = " + coins.ToString ());

		if (coins < reviveCost) {
			excuateInSeconds (notEnoughCoin, 0.7f);
		} else if (!SCAds.rewardedVideoReady ()) {
			excuateInSeconds (adsNotReady, 0.7f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void adsNotReady()
	{
		Animator a = GetComponent<Animator> ();
		a.Play ("ReviveAdsNotReady");
	}

	void notEnoughCoin()
	{
		Animator a = GetComponent<Animator> ();
		a.Play ("ReviveNotEnoughCoin");
	}

	public void skipped()
	{
		GameManager.sharedManager.playerFailed ();
	}

	public void watchVideo()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		SCAds.ShowRewardedAd (GameManager.sharedManager);
	}

	public void useToken()
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);

		if (UserData.getCoinsCount () >= reviveCost) {
			UserData.addCoinsCount (-reviveCost);
			GameManager.sharedManager.coinCountUpdated();
			GameManager.sharedManager.revivePlayer ();
		} else {
		}
	}
}
