using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

	enum skillID {
		RoadWiden = 0,
		AutoColect = 1,
		StepOverAir = 2,
		IncreaseSize = 3,
	};//the enum value is the hardcoded skill getting order

	struct skillInfo{
		int level;
		string description;
		skillID id;
		int coinsToUpgrade;
		bool playerOrGame;
	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Activate(GameObject myobject){
		
	}
}
