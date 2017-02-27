using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillID {
	Unknow = -1,
	Haste = 0,
	Levitation = 1,
	Growth = 2,
	Magnet = 3,
	Conjure = 4,
};

public class SkillInfo
{
	public SkillID skillID;
	public int durationlevel;
	public int dropletLevel;
	public string description;
	public int isLocked;

	public SkillInfo(SkillID skillID)
	{
		this.skillID = skillID;
	}

	public void save()
	{
		PlayerPrefs.SetInt (prefDurationLevelID(), this.durationlevel);
		PlayerPrefs.SetInt (prefDropletLevelID(), this.dropletLevel);
		PlayerPrefs.SetInt (prefIsLockedID(), this.isLocked);
		PlayerPrefs.Save();
	}

	public void load()
	{
		this.durationlevel = PlayerPrefs.GetInt(prefDurationLevelID(), 0);
		this.dropletLevel = PlayerPrefs.GetInt(prefDropletLevelID(), 0);
		this.isLocked = PlayerPrefs.GetInt (prefIsLockedID(), 1);
	}

	string prefDurationLevelID()
	{
		string prefID = "skill-data-duration-level-" + this.skillID.ToString ();
		return prefID;
	}

	string prefDropletLevelID()
	{
		string prefID = "skill-data-Droplet-level-" + this.skillID.ToString ();
		return prefID;
	}

	string prefIsLockedID()
	{
		string prefID = "skill-data-is-locked-" + this.skillID.ToString ();
		return prefID;
	}
};

public class Skill : MonoBehaviour {

	public struct coin_num {
		public int coin, num;
		public coin_num(int x, int y) 
		{
			this.coin = x;
			this.num = y;
		}
	}

	public SkillID skillID = SkillID.Unknow;

	public SkillInfo info;

	public virtual void SetGameObjectFlag (GameObject myobject){
	}

	public virtual void ClearGameObjectFlag(GameObject myobject){
	}

	public virtual List<coin_num> getSkillDropletLevelData(){
		return null;
	}

	public virtual List<coin_num> getSkillDurationLevelData(){
		return null;
	}

	public virtual string getSkillDescription()
	{
		return null;
	}
		
	public void load()
	{
		info = new SkillInfo (this.skillID);
		info.load ();
	}

	public void Activate(GameObject myobject)
	{
		SoundManager.Instance.PlayOneShot(SoundManager.Instance.haste);
		SetGameObjectFlag (myobject);
	}

	public void Deactivate(GameObject myobject){
		ClearGameObjectFlag (myobject);
	}

	public float duration()
	{
		return (float)(getSkillDurationLevelData() [info.durationlevel].num);
	}

	public float nextDuration()
	{
		if (durationMaxed()) {
			return duration ();
		}

		return (float)(getSkillDurationLevelData() [info.durationlevel + 1].num);
	}

	public int dropletRequired()
	{
		return getSkillDropletLevelData() [info.dropletLevel].num;
	}

	public int nextDropletRequired()
	{
		if (dropletMaxed ()) {
			return dropletRequired ();
		}

		return getSkillDropletLevelData() [info.dropletLevel + 1].num;
	}

	public int durationUpgradeCost()
	{
		return getSkillDurationLevelData() [info.durationlevel].coin;
	}

	public int dropletUpgradeCost()
	{
		return getSkillDropletLevelData() [info.dropletLevel].coin;
	}

	public void upgradeDuration()
	{
		info.durationlevel = info.durationlevel + 1;
		info.save ();
	}

	public void upgradeDroplets()
	{
		info.dropletLevel = info.dropletLevel + 1;
		info.save ();
	}

	public void unlockSkill()
	{
		info.isLocked = 0;
		info.save ();
	}

	public string currentDropletDes()
	{
		return "Requires " + dropletRequired().ToString () + " Droplets";
	}

	public string nextDropletDes()
	{
		return "Requires " + nextDropletRequired().ToString () + " Droplets";
	}

	public string currentDurationDes()
	{
		return "Duration " + duration ().ToString ("0.0") + " seconds";
	}

	public string nextDurationDes()
	{
		return "Duration " + nextDuration ().ToString ("0.0") + " seconds";
	}

	public bool durationMaxed()
	{
		int levels = getSkillDurationLevelData ().Count - 1;
		if (info.durationlevel == levels) {
			return true;
		}

		return false;
	}

	public bool dropletMaxed()
	{
		int levels = getSkillDropletLevelData ().Count - 1;
		if (info.dropletLevel == levels) {
			return true;
		}

		return false;
	}
}
