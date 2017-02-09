using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level{

	public int level;
	public float startWidth;
	public float endWidth;
	public float degradeRate;
	public float length;
	public float initialRotateSpeed;
	public float gapFrequency;
	public float curvature;
	public float cameraSpeed;
	public float dropDelay;
	public float gemScatterness;
	public float dropletScatterness;
	public float coinScatterness;

	public Level(int level, float startWidth, float endWidth, float degradeRate, float length, float initialRotateSpeed, float gapFrequency, float curvature, float cameraSpeed, float dropDelay, float gemScatterness, float dropletScatterness, float coinScatterness)
	{
		this.level = level;
		this.startWidth = startWidth;
		this.endWidth = endWidth;
		this.degradeRate = degradeRate;
		this.length = length;
		this.initialRotateSpeed = initialRotateSpeed;
		this.gapFrequency = gapFrequency;
		this.curvature = curvature;
		this.cameraSpeed = cameraSpeed;
		this.dropDelay = dropDelay;
		this.gemScatterness = gemScatterness;
		this.dropletScatterness = dropletScatterness;
		this.coinScatterness = coinScatterness;
	}

	string levelIdString()
	{
		string idString = "level-" + this.level.ToString ();
		return idString;
	}

	public void saveLevelRating(int rating)
	{
		PlayerPrefs.SetInt (levelIdString(), rating);
		PlayerPrefs.Save();
	}

	public int getLevelRating()
	{
		return PlayerPrefs.GetInt(levelIdString(), 3);
	}

	public static int getUerProgressLevel()
	{
		int progress = PlayerPrefs.GetInt("USERPROGRESS", 61);
		return progress;
	}

	public static void setUserProgressLevel(int value)
	{
		PlayerPrefs.SetInt ("USERPROGRESS", value);
		PlayerPrefs.Save();
	}
}
