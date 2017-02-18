﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGrowth : Skill {

	static List<coin_num> dropletsData = new List<coin_num> {
		// (coins required to next level, droplets player need to get)
		{new coin_num(21,7)},
		{new coin_num(42,6)},
		{new coin_num(84,5)},
		{new coin_num(168,4)},
		{new coin_num(336,3)}
	};

	static List<coin_num> durationData = new List<coin_num> {
		// (coins required to next level, duration time)
		{new coin_num(6,6)},
		{new coin_num(12,7)},
		{new coin_num(24,8)},
		{new coin_num(48,9)},
		{new coin_num(96,10)},
		{new coin_num(192,11)},
		{new coin_num(384,12)}
	};

	void Awake()
	{
		skillID = SkillID.Growth;
		load ();
	}

	public override List<coin_num> getSkillDropletLevelData()
	{
		return dropletsData;
	}

	public override List<coin_num> getSkillDurationLevelData()
	{
		return durationData;
	}

	public override string getSkillDescription()
	{
		return "Allowing you to take longer strides.";
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
