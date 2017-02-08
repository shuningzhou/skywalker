using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

	public enum skillID {
		RoadWiden = 0,
		AutoColect = 1,
		StepOverAir = 2,
		IncreaseSize = 3,
	};//the enum value is the hardcoded skill getting order

	public struct skillInfo
	{
		public int level;
		public string description;
		public skillID id;
		public int coinsToUpgrade;
		public bool playerOrGame;
	};

	public struct skillTimer
	{
		public float startTime;
		public float lastTime;
		public float endTime;
	};

//	public static Skill Instance = null;
	public skillInfo thisSkillInfo = new skillInfo();

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

	public virtual int SkillLevelUpgrade(skillID id){
		if (thisSkillInfo.id == id) {
			if (thisSkillInfo.level == 0) {
				// If skill is not unlocked, we don't upgrade it
			} else {
				thisSkillInfo.level++;
			}
			return thisSkillInfo.level;
		} else {
			// id is invalid
			return -1;
		}
	}

	public virtual bool IsSkillUnlocked(){
		if (thisSkillInfo.level > 0) {
			return true;
		} else {
			return false;
		}
	}

	public virtual bool UnlockSkill(skillID id){
		if (thisSkillInfo.id == id) {
			if (thisSkillInfo.level == 0) {
				thisSkillInfo.level++;
			}
			return true;
		} else {
			return false;
		}
	}
}
