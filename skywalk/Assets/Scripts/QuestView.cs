using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour {

	public Quest quest;
	public Text description;
	public Button rewardButton;
	public Text rewardText;

	public Image level1;
	public Image level2;
	public Image level3;
	public Image level4;
	public Image level5;

	public Image rewardInfo;
	public Text rewardInfoText;

	// Use this for initialization
	void Start () {
		updateGUI ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public string getCurrentQuestDes()
	{
		return quest.getCurrentDescription ();
	}

	public int getCurrentQuestLevel()
	{
		return quest.level;
	}

	public int getCurrentReword()
	{
		return quest.getCurrentReward ();
	}

	public void claimReward()
	{
		quest.upgradeLevel ();
		updateGUI ();
		QuestManager.sharedManager.checkQuestConditions ();
	}

	public void updateGUI()
	{
		if (showReward () && getCurrentReword() > 0) {
			rewardButton.gameObject.SetActive (true);
			rewardInfo.gameObject.SetActive (false);
		} else {
			rewardButton.gameObject.SetActive (false);
			rewardInfo.gameObject.SetActive (true);
		}

		description.text = getCurrentQuestDes ();
		rewardText.text = getCurrentReword ().ToString();
		rewardInfoText.text = getCurrentReword ().ToString () + " coins";

		if (getCurrentReword () == 0) {
			rewardInfoText.text = "Cleared";
		}

		int level = getCurrentQuestLevel ();

		level1.gameObject.SetActive (false);
		level2.gameObject.SetActive (false);
		level3.gameObject.SetActive (false);
		level4.gameObject.SetActive (false);
		level5.gameObject.SetActive (false);

		if (level >= 1) {
			level1.gameObject.SetActive (true);
		}

		if (level >= 2) {
			level2.gameObject.SetActive (true);
		}

		if (level >= 3) {
			level3.gameObject.SetActive (true);
		}

		if (level >= 4) {
			level4.gameObject.SetActive (true);
		}

		if (level >= 5) {
			level5.gameObject.SetActive (true);
		}
	}

	public bool showReward()
	{
		return quest.meetCondition ();
	}
}
