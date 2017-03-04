using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHaste : Skill {
		
	static List<coin_num> dropletsData = new List<coin_num> {
		// (coins required to next level, droplets player need to get)
		{new coin_num(32,7)},
		{new coin_num(84,6)},
		{new coin_num(228,5)},
		{new coin_num(446,4)},
		{new coin_num(436,3)}
	};

	static List<coin_num> durationData = new List<coin_num> {
		// (coins required to next level, duration time)
		{new coin_num(26,6)},
		{new coin_num(42,7)},
		{new coin_num(88,8)},
		{new coin_num(126,9)},
		{new coin_num(186,10)},
		{new coin_num(308,11)},
		{new coin_num(484,12)}
	};

	void Awake()
	{
		skillID = SkillID.Haste;
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
		return "Allowing you to sprint wildly.";
	}

	public override void SetGameObjectFlag (GameObject myobject) {
		CharacterMovement player = myobject.GetComponent<CharacterMovement>();

		Animator a = player.gameObject.GetComponent<Animator> ();
		a.SetBool("haste", true);
		// Change the gameobject property
		player.hasteIsActive = true;
	}

	public override void ClearGameObjectFlag(GameObject myobject){
		CharacterMovement player = myobject.GetComponent<CharacterMovement>();

		Animator a = player.gameObject.GetComponent<Animator> ();
		a.SetBool("haste", false);
		// Change the gameobject property
		player.hasteIsActive = false;
	}
}
