using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMagnet : Skill {

	// Use this for initialization
	void Awake () {
		thisSkillInfo = new skillInfo();
		thisSkillInfo.id = skillID.Magnet;
		thisSkillInfo.level.durationLevel = 0;
		thisSkillInfo.level.dropletsLevel = 0;
		thisSkillInfo.description = "Allowing you to carelessly float in the air.";
		thisSkillInfo.playerOrGame = true;
		thisSkillInfo.isActivate = false;

		duration_coin_time_list = new List<coin_num> {
			// (coins required to next level, duration time)
			{new coin_num(26,6)},
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

		droplets_coin_drops_list = new List<coin_num> {
			// (coins required to next level, drops player need to get)
			{new coin_num(26,6)},
			{new coin_num(26,5)},
			{new coin_num(26,4)},
			{new coin_num(26,3)},
			{new coin_num(26,2)},
			{new coin_num(26,1)},
		};

		thisSkillInfo.timer.durationTime = (float)duration_coin_time_list [thisSkillInfo.level.durationLevel].num;
		thisSkillInfo.requiredDroplets = (int)droplets_coin_drops_list [thisSkillInfo.level.dropletsLevel].num;
	}

	// Update is called once per frame
	void Update () {

		}

	public override void SetGameObjectFlag (GameObject myobject) {
		CharacterMovement player = myobject.GetComponent<CharacterMovement>();
		Animator a = player.gameObject.GetComponent<Animator> ();
		a.SetBool("magnet", true);
		// Change the gameobject property
		player.MagnetIsActive = true;
	}

	public override void ClearGameObjectFlag(GameObject myobject){
		CharacterMovement player = myobject.GetComponent<CharacterMovement>();
		Animator a = player.gameObject.GetComponent<Animator> ();
		a.SetBool("magnet", false);
		// Change the gameobject property
		player.MagnetIsActive = false;
	}
}
