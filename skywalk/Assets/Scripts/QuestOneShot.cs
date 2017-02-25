using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOneShot : Quest {
	
	static List<int> rewardsData = new List<int> {
		10,
		20,
		40,
		80,
		100
	};

	static List<int> conditionData = new List<int> {
		5,
		8,
		12,
		15,
		20
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
			return "Clear " + conditions [level - 1].ToString () + " levels without using Revive";
		}

		string des = "Clear " + conditions [level].ToString () + " levels without using Revive";
		return des;
	}
}
