using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHaste : Skill {

	public static event TimeIsUp OnTimeIsUp;
	public static event TimeIsUp ThreeSecBeforeTimeIsUp;

	new public Hashtable duration_level_coin_table = new Hashtable()
	{// {current level, coins required to next level}
		{0, 26},
		{1, 26},
		{2, 26},
		{3, 26},
		{4, 26},
		{5, 26},
		{6, 26},
		{7, 26},
		{8, 26},
		{9, 26}
	};

	new public Hashtable duration_level_time_table = new Hashtable()
	{// {current level, duration time}
		{0, 0},
		{1, 6},
		{2, 7},
		{3, 8},
		{4, 9},
		{5, 10},
		{6, 11},
		{7, 12},
		{8, 13},
		{9, 14},
		{10, 15}
	};

	new public Hashtable droplets_level_coin_table = new Hashtable()
	{// {current level, coins required to next level}
		{0, 26},
		{1, 26},
		{2, 26},
		{3, 26},
		{4, 26},
		{5, 26}
	};

	new public Hashtable droplets_level_drops_table = new Hashtable()
	{// {current level, droplets to collect}
		{0, 10000},
		{1, 5},
		{2, 4},
		{3, 3},
		{4, 2},
		{5, 1}
	};

	// Use this for initialization
	void Start () {
		thisSkillInfo.id = skillID.Haste;
		thisSkillInfo.level.durationLevel = 0;
		thisSkillInfo.level.dropletsLevel = 0;
		thisSkillInfo.description = "Allowing you to sprint wildly.";
		thisSkillInfo.playerOrGame = true;
		thisSkillInfo.isActivate = false;
		thisSkillInfo.timer.durationTime = (float)duration_level_time_table [thisSkillInfo.level.durationLevel];
		thisSkillInfo.requiredDroplets = (int)droplets_level_drops_table [thisSkillInfo.level.dropletsLevel];
	}
	
	// Update is called once per frame
	void Update () {
		// send event 3s before time is up
		if ((thisSkillInfo.timer.endTime - 3f) > Time.time && thisSkillInfo.alertIsSent == false) {
			thisSkillInfo.alertIsSent = true;
			ThreeSecBeforeTimeIsUp (this);
		}
		// send event when time is up
		if (thisSkillInfo.timer.endTime > Time.time && thisSkillInfo.isActivate == true) {
			thisSkillInfo.isActivate = false;
			OnTimeIsUp (this);
		}
	}

	public override void Activate(GameObject myobject){

		CharacterMovement player = myobject.GetComponent<CharacterMovement>();

		// Set activate flag and timer
		thisSkillInfo.isActivate = true;
		thisSkillInfo.alertIsSent = false;
		thisSkillInfo.timer.startTime = Time.time;
		thisSkillInfo.timer.endTime = thisSkillInfo.timer.startTime + thisSkillInfo.timer.durationTime;

		// Change the gameobject property
		//myobject = FindObjectOfType<CharacterMovement> ();
		player.hasteIsActive = true;
	}
}
