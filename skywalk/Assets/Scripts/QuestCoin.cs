using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCoin : Quest {

	static List<int> rewardsData = new List<int> {
		10,
		20,
		40,
		80,
		100
	};

	static List<int> conditionData = new List<int> {
		5,
		20,
		40,
		80,
		100
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
