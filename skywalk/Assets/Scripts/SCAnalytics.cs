using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class SCAnalytics : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void logEvent()
	{
		
	}

	public static void logGameFinishedEvent(float score, int level, bool failed)
	{
		if (failed) {
			Analytics.CustomEvent("Player Failed", new Dictionary<string, object>
				{
					{ "Score", score.ToString("0.00") },
					{ "Level", level.ToString() }
				});
		} else {
			Analytics.CustomEvent("Player Won", new Dictionary<string, object>
				{
					{ "Score", score.ToString("0.00") },
					{ "Level", level.ToString() }
				});
		}
	}

	public static void logDyeEvent(int level, string type, string kind)
	{
		Analytics.CustomEvent("Dye upgrade", new Dictionary<string, object>
			{
				{ "Type", type },
				{ "Level", level.ToString() },
				{ "kind", kind}
			});
	}


	public static void logChestTransaction()
	{
		Analytics.Transaction("chest", 10.0m, "USD", null, null);
	}

	public static void logUserAttributes()
	{
		Analytics.SetUserGender(Gender.Unknown);
		Analytics.SetUserBirthYear(2000);
	}

}
