using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : SOPanel {

	public Text coinCountText;

	// Use this for initialization
	void Start () {
		coinsUpdated ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void coinsUpdated()
	{
		coinCountText.text = UserData.getCoinsCount ().ToString();
	}

	public void potionButtonPressed()
	{
		Debug.Log ("Potion button Pressed");
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		PanelManager.sharedManager.showPanel (PanelManager.sharedManager.potionPanel);
	}

	public void questButtonPressed()
	{
		Debug.Log ("Quest button Pressed");
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		PanelManager.sharedManager.showPanel (PanelManager.sharedManager.questPanel);
	}

	public void storeButtonPressed()
	{
		Debug.Log ("Store button Pressed");
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.buttonClicked);
		PanelManager.sharedManager.showPanel (PanelManager.sharedManager.storePanel);
	}
}
