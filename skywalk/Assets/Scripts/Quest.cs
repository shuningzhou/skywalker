using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestID {
	Unknow = -1,
	LevelQuest = 0,
	CoinQuest = 1,
	StarQuest = 2,
	OneShotQuest = 3,
	FriendQuest = 4,
};

public class Quest : MonoBehaviour {

	public QuestID questID = QuestID.Unknow;
	public int level = 0;
	public List<int> rewards;
	public List<int> conditions;

	public void save()
	{
		PlayerPrefs.SetInt (prefQuestLevelID(), level);
		PlayerPrefs.Save();
	}

	public void load()
	{
		level = PlayerPrefs.GetInt(prefQuestLevelID(), 0);
	}

	string prefQuestLevelID()
	{
		string prefID = "Quest-data-level-" + this.questID.ToString ();
		return prefID;
	}

	public int getCurrentReward()
	{
		if (level == rewards.Count) 
		{
			return 0;
		}

		return rewards [level];
	}

	public virtual bool meetCondition()
	{
		if (level == conditions.Count) 
		{
			return false;
		}

		int condition = conditions [level];

		int userProgress = getCollected ();

		if (userProgress >= condition) {
			return true;
		} else {
			return false;
		}
	}

	public void upgradeLevel()
	{
		if (level < rewards.Count) 
		{
			level = level + 1;
			save ();
		}
	}

	public void collectedCondition()
	{
		int count = PlayerPrefs.GetInt("Quest-data-collected-" + this.questID.ToString (), 0);
		PlayerPrefs.SetInt ("Quest-data-collected-" + this.questID.ToString (), count + 1);
		PlayerPrefs.Save();
	}

	public int getCollected()
	{
		int count = PlayerPrefs.GetInt("Quest-data-collected-" + this.questID.ToString (), 0);
		return count;
	}

	public virtual string getCurrentDescription()
	{
		return "";
	}
}
