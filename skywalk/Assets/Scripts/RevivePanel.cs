using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevivePanel : SOPanel {

	public int reviveCost = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
