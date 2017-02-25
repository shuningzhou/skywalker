using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFriend : Quest {

	// Use this for initialization
	static List<int> rewardsData = new List<int> {
		20,
		60,
		100,
		160,
		200
	};

	static List<int> conditionData = new List<int> {
		1,
		3,
		5,
		8,
		10
	};

	void Awake()
	{
		rewards = rewardsData;
		conditions = conditionData;
		load ();
	}
}
