using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level{

	public int level;

	//road
	public float startWidth;
	public float endWidth;
	public float degradeRate;

	public int length;
	public float curvature;
	public float gapFrequency;
	public float dropDelay;

	public bool turnRight;

	//character
	public float initialRotateSpeed;
	public float rotateSpeedChange;
	public float maxRotateSpeed;

	//camera
	public float cameraSpeed;

	//collectable
	public float gemScatterness;
	public float dropletScatterness;
	public float coinScatterness;
	public float hasteDropRate;
	public float growthDropRate;
	public float levitationDropRate;
	public float magnetDropRate;
	public float coinDropRate;

	public Level(	int level, 
					float startWidth, 
					float endWidth, 
					float degradeRate, 
					int length, 
					float initialRotateSpeed, 
					float rotateSpeedChange, 
					float maxRotateSpeed, 
					float gapFrequency, 
					float curvature, 
					float cameraSpeed, 
					float dropDelay, 
					bool turnRight,
					float gemScatterness, 
					float dropletScatterness, 
					float coinScatterness,
					float hasteDropRate,
					float growthDropRate,
					float levitationDropRate,
					float magnetDropRate,
		            float coinDropRate
								)
	{
		this.level = level;
		this.startWidth = startWidth;
		this.endWidth = endWidth;
		this.degradeRate = degradeRate;
		this.length = length;
		this.initialRotateSpeed = initialRotateSpeed;
		this.rotateSpeedChange = rotateSpeedChange;
		this.maxRotateSpeed = maxRotateSpeed;
		this.gapFrequency = gapFrequency;
		this.curvature = curvature;
		this.cameraSpeed = cameraSpeed;
		this.dropDelay = dropDelay;
		this.gemScatterness = gemScatterness;
		this.dropletScatterness = dropletScatterness;
		this.coinScatterness = coinScatterness;
		this.turnRight = turnRight;
		this.hasteDropRate = hasteDropRate;
		this.growthDropRate = growthDropRate;
		this.levitationDropRate = levitationDropRate;
		this.magnetDropRate = magnetDropRate;
		this.coinDropRate = coinDropRate;
	}

	string levelIdString()
	{
		string idString = "level-" + this.level.ToString ();
		return idString;
	}

	public void saveLevelRating(int rating)
	{
		if (getLevelRating () > rating) {
			return;
		}

		PlayerPrefs.SetInt (levelIdString(), rating);
		PlayerPrefs.Save();
	}

	public int getLevelRating()
	{
		return PlayerPrefs.GetInt(levelIdString(), 0);
	}

	public static int getUerProgressLevel()
	{
		int progress = PlayerPrefs.GetInt("USERPROGRESS", 0);
		return progress;
	}

	public static void setUserProgressLevel(int value)
	{
		if (getUerProgressLevel () > value) 
		{
			return;
		}

		PlayerPrefs.SetInt ("USERPROGRESS", value);
		PlayerPrefs.Save();
	}
}
