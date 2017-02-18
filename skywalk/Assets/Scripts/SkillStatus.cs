using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillStatus : MonoBehaviour {

	public Image droplet1;
	public Image droplet2;
	public Image droplet3;
	public Image droplet4;
	public Image droplet5;
	public Image droplet6;
	public Image droplet7;

	public Image droplet1_background;
	public Image droplet2_background;
	public Image droplet3_background;
	public Image droplet4_background;
	public Image droplet5_background;
	public Image droplet6_background;
	public Image droplet7_background;

	public Image progressImage;

	public Skill skill;

	int currentDropletsCount = 0;

	bool skillIsActive = false;

	float skillStartedTime = 0f;
	float skillEndTime = 0f;

	public GameObject player;
	public GameObject roadGenenrator;

	// Use this for initialization
	void Start ()
	{
		int dropletsRequired = skill.thisSkillInfo.requiredDroplets;
		Debug.Log ("Haste skill required droplets = " + dropletsRequired.ToString ());

		if (dropletsRequired <= 6) 
		{
			droplet7_background.gameObject.SetActive (false);
		}

		if (dropletsRequired <= 5) 
		{
			droplet6_background.gameObject.SetActive (false);
		}

		if (dropletsRequired <= 4) 
		{
			droplet5_background.gameObject.SetActive (false);
		}

		if (dropletsRequired <= 3) 
		{
			droplet4_background.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (skillIsActive) 
		{
			float hastFillRate = (float)(skillEndTime - Time.time) / (float)skill.thisSkillInfo.timer.durationTime;
			progressImage.fillAmount = hastFillRate;

			if (skillEndTime < Time.time) 
			{
				skillIsActive = false;

				if (skill.thisSkillInfo.playerOrGame) 
				{
					skill.Deactivate (player);
				} 
				else 
				{
					skill.Deactivate (roadGenenrator);
				}
			}
		}
	}

	public void addDroplet()
	{
		currentDropletsCount = currentDropletsCount + 1;

		if (currentDropletsCount >= skill.thisSkillInfo.requiredDroplets) 
		{
			skillIsActive = true;

			if (skill.thisSkillInfo.playerOrGame) 
			{
				skill.Activate (player);
			} 
			else 
			{
				skill.Activate (roadGenenrator);
			}
				
			currentDropletsCount = 0;
			skillStartedTime = Time.time;
			skillEndTime = skillStartedTime + skill.thisSkillInfo.timer.durationTime;
		}

		updateGUI ();
	}

	public void updateGUI()
	{
		Debug.Log ("Current hast droplets count = " + currentDropletsCount.ToString ());
		if (currentDropletsCount == 0) {
			droplet1.gameObject.SetActive (false);
			droplet2.gameObject.SetActive (false);
			droplet3.gameObject.SetActive (false);
			droplet4.gameObject.SetActive (false);
			droplet5.gameObject.SetActive (false);
			droplet6.gameObject.SetActive (false);
			droplet7.gameObject.SetActive (false);
		}

		if (currentDropletsCount >= 1) {
			droplet1.gameObject.SetActive (true);
		}

		if (currentDropletsCount >= 2) {
			droplet2.gameObject.SetActive (true);
		}

		if (currentDropletsCount >= 3) {
			droplet3.gameObject.SetActive (true);
		}

		if (currentDropletsCount >= 4) {
			droplet4.gameObject.SetActive (true);
		}

		if (currentDropletsCount >= 5) {
			droplet5.gameObject.SetActive (true);
		}

		if (currentDropletsCount >= 6) {
			droplet6.gameObject.SetActive (true);
		}

		if (currentDropletsCount >= 7) {
			droplet7.gameObject.SetActive (true);
		}
	}
}
