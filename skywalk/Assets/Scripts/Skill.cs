using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {
	
	public delegate void TimeIsUp(Skill skill);

	public Hashtable duration_level_coin_table;

	public Hashtable duration_level_time_table;

	public Hashtable droplets_level_coin_table;

	public Hashtable droplets_level_drops_table;

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
//	public static Skill Instance = null;

//	void Awake () {
//		if (Instance == null) {
//			Instance = this;
//		} else if (Instance != this) {
//			Destroy(gameObject);
//		}
//	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Activate(GameObject myobject){
		
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
					thisSkillInfo.timer.durationTime = (float)duration_level_time_table [thisSkillInfo.level.durationLevel];
					thisSkillInfo.level.requiredConinsDurationLvl = (int)duration_level_coin_table [thisSkillInfo.level.durationLevel];
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
					thisSkillInfo.requiredDroplets = (int)droplets_level_drops_table [thisSkillInfo.level.dropletsLevel];
					thisSkillInfo.level.requiredConinsDurationLvl = (int)droplets_level_coin_table [thisSkillInfo.level.dropletsLevel];
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
				thisSkillInfo.timer.durationTime = (float)duration_level_time_table [thisSkillInfo.level.durationLevel];
				thisSkillInfo.level.requiredConinsDurationLvl = (int)duration_level_coin_table [thisSkillInfo.level.durationLevel];
			}
			if (thisSkillInfo.level.dropletsLevel == 0) {
				thisSkillInfo.level.dropletsLevel++;
				thisSkillInfo.requiredDroplets = (int)droplets_level_drops_table [thisSkillInfo.level.dropletsLevel];
				thisSkillInfo.level.requiredConinsDurationLvl = (int)droplets_level_coin_table [thisSkillInfo.level.dropletsLevel];
			}
			return true;
		} else {
			return false;
		}
	}
}
