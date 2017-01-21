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

	public static void logGameOverEvent(float distance, int collected)
	{
		Analytics.CustomEvent("GameOver", new Dictionary<string, object>
			{
				{ "Distance", distance.ToString("0.0") },
				{ "Collected", collected.ToString() }
			});
	}

	public static void logTransaction()
	{
		Analytics.Transaction("12345abcde", 0.99m, "USD", null, null);
	}

	public static void logUserAttributes()
	{
		Analytics.SetUserGender(Gender.Unknown);
		Analytics.SetUserBirthYear(2000);
	}

}
