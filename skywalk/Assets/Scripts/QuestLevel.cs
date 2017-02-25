using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLevel : Quest {

	static List<int> rewardsData = new List<int> {
		10,
		20,
		40,
		80,
		100
	};

	static List<int> conditionData = new List<int> {
		5,
		10,
		20,
		40,
		50
	};

	void Awake()
	{
		rewards = rewardsData;
		conditions = conditionData;
		load ();
	}

	override public bool meetCondition()
	{
		if (level == conditions.Count) 
		{
			return false;
		}

		int levelCondition = conditionData [level];

		int userProgressLevel = Level.getUerProgressLevel ();

		if (userProgressLevel >= levelCondition) {
			return true;
		} else {
			return false;
		}
	}

	override public string getCurrentDescription()
	{
		if (level == conditions.Count) 
		{
			return "Clear level " + conditions [level - 1].ToString ();
		}

		string des = "Clear level " + conditions [level].ToString ();
		return des;
	}
}
