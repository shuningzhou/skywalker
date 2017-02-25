using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : SOPanel {

	public QuestView levelQuestView;
	public QuestView coinQuestView;
	public QuestView starQuestView;
	public QuestView recordQuestView;

	// Use this for initialization
	void Awake () {
		levelQuestView.quest = QuestManager.sharedManager.questLevel;
		coinQuestView.quest = QuestManager.sharedManager.questCoin;
		starQuestView.quest = QuestManager.sharedManager.questStar;
		recordQuestView.quest = QuestManager.sharedManager.questOneShot;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
