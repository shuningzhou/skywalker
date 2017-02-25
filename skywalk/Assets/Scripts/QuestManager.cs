using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

	public static QuestManager sharedManager = null;

	public delegate void BadgeChanged();
	public static event BadgeChanged badgeChanged;

	public QuestCoin questCoin;
	public QuestLevel questLevel;
	public QuestOneShot questOneShot;
	public QuestStar questStar;

	public bool showBadge = false;

	void Awake()
	{
		if (sharedManager == null) 
		{
			sharedManager = this;

		} else if (sharedManager != this) {
			Destroy(gameObject);
			return;
		}

		this.transform.parent = null;

		DontDestroyOnLoad(gameObject);


		Debug.Log ("Quest manager awake");
	}

	public void checkQuestConditions()
	{
		showBadge = false;

		if (questCoin.meetCondition ())
		{
			showBadge = true;
		}

		if (questLevel.meetCondition ())
		{
			showBadge = true;
		}

		if (questOneShot.meetCondition ())
		{
			showBadge = true;
		}

		if (questStar.meetCondition ())
		{
			showBadge = true;
		}

		if (badgeChanged != null) {
			badgeChanged ();
		}
	}
}
