using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGrowth : Skill {
	// Use this for initialization
	void Awake () {
		thisSkillInfo = new skillInfo();
		thisSkillInfo.id = skillID.Growth;
		thisSkillInfo.level.durationLevel = 0;
		thisSkillInfo.level.dropletsLevel = 0;
		thisSkillInfo.description = "Allowing you to take longer strides.";
		thisSkillInfo.playerOrGame = true;
		thisSkillInfo.isActivate = false;

		droplets_coin_drops_list = new List<coin_num> {
			// (coins required to next level, drops player need to get)
			{new coin_num(26,6)},
			{new coin_num(26,5)},
			{new coin_num(26,4)},
			{new coin_num(26,3)},
			{new coin_num(26,2)},
			{new coin_num(26,1)},
		};

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

		thisSkillInfo.timer.durationTime = (float)duration_coin_time_list [thisSkillInfo.level.durationLevel].num;
		thisSkillInfo.requiredDroplets = (int)droplets_coin_drops_list [thisSkillInfo.level.dropletsLevel].num;
	}

	// Update is called once per frame
	void Update () {

		}

	public override void SetGameObjectFlag (GameObject myobject) {
		CharacterMovement player = myobject.GetComponent<CharacterMovement>();

		Vector3 scale = player.gameObject.transform.localScale;
		Vector3 position = player.gameObject.transform.position;

		scale.x = 1f;
		scale.z = 4f;
		scale.y = 3f;

		position.y = 71.2f;

		Vector3 footPosition = player.getFootPosition ();

		player.gameObject.transform.localScale = scale;

		Vector3 newFootPosition = player.getFootPosition ();

		Vector3 footPositionChange = newFootPosition - footPosition;

		position = position - footPositionChange;

		position.y = 71.2f;

		player.gameObject.transform.position = position;

		Animator a = player.gameObject.GetComponent<Animator> ();
		a.SetBool("growth", true);

		// Change the gameobject property
		player.GrowthIsActive = true;
	}

	public override void ClearGameObjectFlag(GameObject myobject){
		CharacterMovement player = myobject.GetComponent<CharacterMovement>();

		Vector3 scale = player.gameObject.transform.localScale;
		Vector3 position = player.gameObject.transform.position;

		scale.x = 0.5f;
		scale.z = 2f;
		scale.y = 2f;

		Vector3 footPosition = player.getFootPosition ();

		player.gameObject.transform.localScale = scale;

		Vector3 newFootPosition = player.getFootPosition ();

		Vector3 footPositionChange = newFootPosition - footPosition;

		position = position - footPositionChange;

		position.y = 70.7f;

		player.gameObject.transform.localScale = scale;
		player.gameObject.transform.position = position;

		Animator a = player.gameObject.GetComponent<Animator> ();
		a.SetBool("growth", false);

		// Change the gameobject property
		player.GrowthIsActive = false;
	}
}
