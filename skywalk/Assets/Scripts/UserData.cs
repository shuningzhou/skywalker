using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserData : MonoBehaviour {
	
	public static string PREF_COIN_COLLECTED = "collected";
	public static string PREF_LAST_DISTANCE = "last_distance";
	public static string PREF_DISTANCE = "distance";
	public static string PREF_DATE = "date";
	public static string PREF_TUTORIAL_DONE = "tutorial_done";
	public static string PREF_USER_NAME = "user_name";

	public static float getLocalBestRecordValue()
	{
		return PlayerPrefs.GetFloat(PREF_DISTANCE, 0);
	}

	public static string getLocalBestRecordDate()
	{
		return PlayerPrefs.GetString(PREF_DATE, "No Record");
	}

	public static float getLastDistance()
	{
		return PlayerPrefs.GetFloat(PREF_LAST_DISTANCE, 0);
	}

	public static string getUserName()
	{
		return PlayerPrefs.GetString(PREF_USER_NAME, "");
	}

	public static int getCoinsCount()
	{
		return PlayerPrefs.GetInt(PREF_COIN_COLLECTED, 0);
	}

	public static int getTutorialDone()
	{
		return PlayerPrefs.GetInt(PREF_TUTORIAL_DONE, 0);
	}

	public static void saveLocalBestRecordValue(float value)
	{
		PlayerPrefs.SetFloat (PREF_DISTANCE, value);
		PlayerPrefs.Save();
	}

	public static void saveLastDistance(float value)
	{
		PlayerPrefs.SetFloat (PREF_LAST_DISTANCE, value);
		PlayerPrefs.Save();
	}

	public static void saveLocalBestRecordDate(string value)
	{
		PlayerPrefs.SetString (PREF_DATE, value);
		PlayerPrefs.Save();
	}

	public static void saveUserName(string value)
	{
		PlayerPrefs.SetString (PREF_USER_NAME, value);
		PlayerPrefs.Save();
	}

	public static void saveCoinsCount(int value)
	{
		PlayerPrefs.SetInt (PREF_COIN_COLLECTED, value);
		PlayerPrefs.Save();
	}

	public static void tutorialDone()
	{
		PlayerPrefs.SetInt (PREF_TUTORIAL_DONE, 1);
		PlayerPrefs.Save();
	}

	public static void addCoinsCount(int value)
	{
		int count = getCoinsCount();
		count = count + value;
		saveCoinsCount (count);
	}

	public static void updateBestDistance(float distance)
	{
		float best = getLocalBestRecordValue();
		if (best < distance) {
			saveLocalBestRecordValue (distance);
			saveLocalBestRecordDate (DateTime.Now.ToString ());
		}
	}
}
