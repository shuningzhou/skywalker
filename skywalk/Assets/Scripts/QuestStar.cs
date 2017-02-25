using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStar : Quest {

	static List<int> rewardsData = new List<int> {
		10,
		20,
		40,
		80,
		100
	};

	static List<int> conditionData = new List<int> {
		3,
		6,
		12,
		24,
		48
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
