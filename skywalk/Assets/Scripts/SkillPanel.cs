using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {

	public Text durationText;
	public Text nextDurationText;
	public Text nextDropletText;
	public Text dropletText;

	public Skill skill;

	public Text durationUpgradeCost;
	public Text dropletUpgradeCost;

	public Button durationButton;
	public Button dropletButton;

	public GameObject lockedPanel;

	// Use this for initialization
	void Start () {
		updateUI ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void updateUI()
	{
		durationText.text = skill.currentDurationDes ();
		dropletText.text = skill.currentDropletDes ();

		nextDurationText.text = skill.nextDurationDes ();
		nextDropletText.text = skill.nextDropletDes ();

		durationUpgradeCost.text = skill.durationUpgradeCost ().ToString ();
		dropletUpgradeCost.text = skill.dropletUpgradeCost ().ToString ();

		if (skill.durationMaxed ()) {
			durationButton.gameObject.SetActive (false);
		} else {
			durationButton.gameObject.SetActive (true);
		}

		if (skill.dropletMaxed ()) {
			dropletButton.gameObject.SetActive (false);
		} else {
			dropletButton.gameObject.SetActive (true);
		}

		if (skill.info.isLocked == 1) {
			lockedPanel.gameObject.SetActive (true);
		} else {
			lockedPanel.gameObject.SetActive (false);
		}
	}

	public void durationButtonPressed()
	{
		if (UserData.getCoinsCount () >= skill.durationUpgradeCost ()) {
			UserData.addCoinsCount (-skill.durationUpgradeCost ());
			skill.upgradeDuration ();
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.unlock);
			SCAnalytics.logDyeEvent (skill.info.durationlevel, skill.skillID.ToString (), "duration");
		} else {
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.uiFailed);
		}

		updateUI ();
	}

	public void dropletButtonPressed()
	{
		if (UserData.getCoinsCount () >= skill.dropletUpgradeCost ()) {
			UserData.addCoinsCount (-skill.dropletUpgradeCost ());
			skill.upgradeDroplets ();
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.unlock);
			SCAnalytics.logDyeEvent (skill.info.dropletLevel, skill.skillID.ToString (), "droplet");
		} else {
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.uiFailed);
		}

		updateUI ();
	}
}
