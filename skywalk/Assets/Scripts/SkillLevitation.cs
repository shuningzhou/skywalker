using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLevitation : Skill {

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
		skillID = SkillID.Levitation;
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
		return "Allowing you to carelessly float in the air.";
	}

	public override void SetGameObjectFlag (GameObject myobject) {
		CharacterMovement player = myobject.GetComponent<CharacterMovement>();
		Animator a = player.gameObject.GetComponent<Animator> ();
		a.SetBool("levitation", true);

		// Change the gameobject property
		player.LeviationIsActive = true;
	}

	public override void ClearGameObjectFlag(GameObject myobject){
		CharacterMovement player = myobject.GetComponent<CharacterMovement>();
		Animator a = player.gameObject.GetComponent<Animator> ();
		a.SetBool("levitation", false);

		// Change the gameobject property
		player.LeviationIsActive = false;
	}
}
