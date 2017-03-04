using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCoin : Quest {

	static List<int> rewardsData = new List<int> {
		30,
		50,
		80,
		100,
		140
	};

	static List<int> conditionData = new List<int> {
		5,
		25,
		50,
		100,
		150
	};

	void Awake()
	{
		rewards = rewardsData;
		conditions = conditionData;
		load ();
	}

	override public string getCurrentDescription()
	{
		if (level == conditions.Count) 
		{
			return "Collect " + conditions [level - 1].ToString () + " coins";
		}

		string des = "Collect " + conditions [level].ToString () + " coins";
		return des;
	}
}
