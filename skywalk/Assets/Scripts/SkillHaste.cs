using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHaste : Skill {

	public static event TimeIsUp OnTimeIsUp;
	public static event TimeIsUp ThreeSecBeforeTimeIsUp;

	new public List<coin_num> duration_coin_time_list = new List<coin_num> {
		// (coins required to next level, duration time)
		{new coin_num(26,0)},
		{new coin_num(26,6)},
		{new coin_num(26,7)},
		{new coin_num(26,8)},
		{new coin_num(26,9)},
		{new coin_num(26,10)},
		{new coin_num(26,11)},
		{new coin_num(26,12)},
		{new coin_num(26,13)},
		{new coin_num(26,14)},
		{new coin_num(26,15)},
	};

	new public List<coin_num> droplets_coin_drops_list = new List<coin_num> {
		// (coins required to next level, drops player need to get)
		{new coin_num(26,10000)},
		{new coin_num(26,5)},
		{new coin_num(26,4)},
		{new coin_num(26,3)},
		{new coin_num(26,2)},
		{new coin_num(26,1)},
	};

	// Use this for initialization
	void Start () {
		thisSkillInfo.id = skillID.Haste;
		thisSkillInfo.level.durationLevel = 0;
		thisSkillInfo.level.dropletsLevel = 0;
		thisSkillInfo.description = "Allowing you to sprint wildly.";
		thisSkillInfo.playerOrGame = true;
		thisSkillInfo.isActivate = false;
		thisSkillInfo.timer.durationTime = (float)duration_coin_time_list [thisSkillInfo.level.durationLevel].num;
		thisSkillInfo.requiredDroplets = (int)droplets_coin_drops_list [thisSkillInfo.level.dropletsLevel].num;
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
