using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

//commented out because timer caculation is done by Peter
//	public delegate void TimeIsUp(Skill skill);
//	public static event TimeIsUp OnTimeIsUp;
//	public static event TimeIsUp ThreeSecBeforeTimeIsUp;

	public struct coin_num {
		public int coin, num;
		public coin_num(int x, int y) 
		{
			this.coin = x;
			this.num = y;
		}
	}

	public List<coin_num> duration_coin_time_list;
	public List<coin_num> droplets_coin_drops_list;

	public enum skillID {
		Haste = 0,
		Levitation = 1,
		Growth = 2,
		Magnet = 3,
		Conjure = 4,
	};//the enum value is the hardcoded skill getting order

	public enum levelID{
		Duration = 0,
		Droplets,
	};

	public struct skillLevel
	{
		public int durationLevel;
		public int requiredConinsDurationLvl;
		public int dropletsLevel;
		public int reuqiredConinsDropletLvl;
	};

	public struct skillTimer
	{
		public float startTime;
		public float durationTime;
		public float endTime;
	};

	public struct skillInfo
	{
		public skillID id;
		public skillLevel level;
		public string description;
		public bool playerOrGame;
		public bool isActivate;
		public bool alertIsSent;
		public skillTimer timer;
		public int requiredDroplets;
	};

	public skillInfo thisSkillInfo = new skillInfo();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
//commented out because timer caculation is done by Peter
//		// send event 3s before time is up
//		if ((thisSkillInfo.timer.endTime - 3f) < Time.time && thisSkillInfo.alertIsSent == false) {
//			thisSkillInfo.alertIsSent = true;
//			ThreeSecBeforeTimeIsUp (this);
//		}
//		// send event when time is up
//		if (thisSkillInfo.timer.endTime < Time.time && thisSkillInfo.isActivate == true) {
//			thisSkillInfo.isActivate = false;
//			OnTimeIsUp (this);
//		}
	}

	public virtual void SetGameObjectFlag (GameObject myobject){
	}

	public virtual void ClearGameObjectFlag(GameObject myobject){
	}

	public virtual void Activate(GameObject myobject){

		// Set activate flag and timer
		thisSkillInfo.isActivate = true;
		thisSkillInfo.alertIsSent = false;
		thisSkillInfo.timer.startTime = Time.time;
		thisSkillInfo.timer.endTime = thisSkillInfo.timer.startTime + thisSkillInfo.timer.durationTime;

		SetGameObjectFlag (myobject);
	}

	public virtual void Deactivate(GameObject myobject){
		ClearGameObjectFlag (myobject);
	}

	public virtual skillInfo GetSkillInfo(){
		return thisSkillInfo;
	}

	public virtual int SkillLevelUpgrade(skillID skill_id, levelID level_id){
		if (level_id == levelID.Duration) {
			if (thisSkillInfo.id == skill_id) {
				if (thisSkillInfo.level.durationLevel == 0 || thisSkillInfo.level.durationLevel == 10) {
					// If skill is not unlocked, or if skill reaches the max level,
					// we don't upgrade it
				} else {
					thisSkillInfo.level.durationLevel++;
					thisSkillInfo.timer.durationTime = (float)duration_coin_time_list [thisSkillInfo.level.durationLevel].num;
					thisSkillInfo.level.requiredConinsDurationLvl = (int)duration_coin_time_list [thisSkillInfo.level.durationLevel].coin;
				}
				return thisSkillInfo.level.durationLevel;
			} else {
				// id is invalid
				return -1;
			}
		} else if (level_id == levelID.Droplets) {
			if (thisSkillInfo.id == skill_id) {
				if (thisSkillInfo.level.dropletsLevel == 0 || thisSkillInfo.level.dropletsLevel == 5) {
					// If skill is not unlocked, or if skill reaches the max level,
					// we don't upgrade it
				} else {
					thisSkillInfo.level.dropletsLevel++;
					thisSkillInfo.requiredDroplets = (int)droplets_coin_drops_list [thisSkillInfo.level.dropletsLevel].num;
					thisSkillInfo.level.requiredConinsDurationLvl = (int)droplets_coin_drops_list [thisSkillInfo.level.dropletsLevel].coin;
				}
				return thisSkillInfo.level.dropletsLevel;
			} else {
				// id is invalid
				return -1;
			}
		} else {
			return -1;
		}
	}

	public virtual bool IsSkillUnlocked(){
		if (thisSkillInfo.level.durationLevel > 0 && thisSkillInfo.level.dropletsLevel > 0) {
			return true;
		} else {
			return false;
		}
	}

	public virtual bool UnlockSkill(skillID id){
		if (thisSkillInfo.id == id) {
			if (thisSkillInfo.level.durationLevel == 0) {
				thisSkillInfo.level.durationLevel++;
				thisSkillInfo.timer.durationTime = (float)duration_coin_time_list [thisSkillInfo.level.durationLevel].num;
				thisSkillInfo.level.requiredConinsDurationLvl = (int)duration_coin_time_list [thisSkillInfo.level.durationLevel].coin;
			}
			if (thisSkillInfo.level.dropletsLevel == 0) {
				thisSkillInfo.level.dropletsLevel++;
				thisSkillInfo.requiredDroplets = (int)droplets_coin_drops_list [thisSkillInfo.level.dropletsLevel].num;
				thisSkillInfo.level.requiredConinsDurationLvl = (int)droplets_coin_drops_list [thisSkillInfo.level.dropletsLevel].coin;
			}
			return true;
		} else {
			return false;
		}
	}
}
