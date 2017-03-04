using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStar : Quest {

	static List<int> rewardsData = new List<int> {
		50,
		90,
		160,
		220,
		330
	};

	static List<int> conditionData = new List<int> {
		3,
		8,
		18,
		36,
		52
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
			return "Score 3 stars " + conditions [level - 1].ToString () + " times";
		}

		string des = "Score 3 stars " + conditions [level].ToString () + " times";
		return des;
	}

}
