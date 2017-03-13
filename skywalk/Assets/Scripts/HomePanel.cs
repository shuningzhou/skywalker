using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanel : SOPanel {

	public Text coinCountText;
	public Image questBadge;

	// Use this for initialization
	void Start () {
		coinsUpdated ();
		UserData.CoinChanged += UserData_CoinChanged;
		QuestManager.badgeChanged += QuestManager_badgeChanged;
		QuestManager.sharedManager.checkQuestConditions ();

		badgeUpdated ();
	}

	void QuestManager_badgeChanged ()
	{
		badgeUpdated ();
	}

	void OnDestroy()
	{
		UserData.CoinChanged -= UserData_CoinChanged;
		QuestManager.badgeChanged -= QuestManager_badgeChanged;
	}

	void UserData_CoinChanged ()
	{
		coinsUpdated ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void coinsUpdated()
	{
		coinCountText.text = UserData.getCoinsCount ().ToString();
	}

	public void badgeUpdated()
	{
		if (QuestManager.sharedManager.showBadge) {
			questBadge.gameObject.SetActive (true);
		} else {
			questBadge.gameObject.SetActive (false);
		}
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
		PanelManager.sharedManager.storePanel.refreshUI ();
	}
}
