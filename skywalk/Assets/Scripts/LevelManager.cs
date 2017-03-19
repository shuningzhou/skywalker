using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager sharedManager = null;
	public List<Level> levels = new List<Level> ();
	public Level currentLevel;

	void Awake()
	{
		if (sharedManager == null) 
		{
			int firstLaunch = UserData.getFirstLaunch ();

			if (firstLaunch == 0) {
				UserData.addCoinsCount (20);
				UserData.saveFirstLaunch (1);
			}

			sharedManager = this;
			Application.targetFrameRate = 60;
			levelData ();

		} else if (sharedManager != this) {
			Destroy(gameObject);
			return;
		}

		this.transform.parent = null;

		DontDestroyOnLoad(gameObject);


		Debug.Log ("Level manager awake");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Level getCurrentLevel()
	{
		return currentLevel;
	}

	public void setCurrentLevel(Level cl)
	{
		currentLevel = cl;
	}

	public void playNextLevel()
	{
		int index = levels.IndexOf (currentLevel);
		index = index + 1;
		if (index <= levels.Count) {
			currentLevel = levels [index];
			playCurrentLevel ();
		}
	}

	public void playCurrentLevel()
	{
		SceneManager.LoadScene ("main", LoadSceneMode.Single);
	}

	public void currentLevelFinishedAndRetured()
	{
		SceneManager.LoadScene ("Home", LoadSceneMode.Single);
	}

	public void currentLevelFailed()
	{
		SceneManager.LoadScene ("Home", LoadSceneMode.Single);
	}


//	public Level(	int level, 
//		float startWidth, 
//		float endWidth, 
//		float degradeRate, 
//		int length, 
//		float initialRotateSpeed, 
//		float rotateSpeedChange, 
//		float maxRotateSpeed, 
//		float gapFrequency, 
//		float curvature, 
//		float cameraSpeed, 
//		float dropDelay, 
//		bool turnRight,
//		float gemScatterness, 
//		float dropletScatterness, 
//		float coinScatterness)

	public void levelData()
	{
		float dropDelaySeed = 0.9f;
		//                       level sWidth eWidth dRate len   iRS   sc  maxRS  gap    curv cam    dropDelay                     turn   gem   drop      coin   haste    growth    levitation,       mag,     coinDrop
		Level level1 =   new Level (1,   6f,    5.5f,  1f,  5,   150f, 5f,  250f, 0,     0.7f, 0,    1f,                           true,  0.0f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level2 =   new Level (2,   6f,    4.5f,  1f,  7,   150f, 5f,  250f, 0,     0.7f, 0,    Mathf.Pow(dropDelaySeed, 1),  true,  0.1f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level3 =   new Level (3,   5.5f,  4f,    1f,  8,   150f, 5f,  250f, 0,     0.7f, 0,    Mathf.Pow(dropDelaySeed, 2),  false, 0.3f,   0.1f,   0.3f,  0,        0,         0,              0.2f,      0.01f);
		Level level4 =   new Level (4,   5.5f,  4f,    1f,  8,   150f, 5f,  250f, 0,     0.7f, 0.4f, Mathf.Pow(dropDelaySeed, 3),  true,  0.1f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level5 =   new Level (5,   5f,    4f,    1f,  8,   150f, 5f,  250f, 0,     0.7f, 0.5f, Mathf.Pow(dropDelaySeed, 4),  false, 0.1f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level6 =   new Level (6,   2.5f,  2f,    1f,  8,   150f, 5f,  250f, 0,     0.7f, 0.6f, Mathf.Pow(dropDelaySeed, 5),  true,  0.1f,   0.0f,   0.1f,  0,        0,         0.3f,           0f,        0.01f);
		Level level7 =   new Level (7,   5f,    4f,    1f,  8,   150f, 5f,  250f, 0,     0.7f, 0.7f, Mathf.Pow(dropDelaySeed, 6),  false, 0.1f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level8 =   new Level (8,   4.5f,  3.5f,  1f,  8,   150f, 5f,  250f, 0,     0.7f, 0.8f, Mathf.Pow(dropDelaySeed, 7),  true,  0.1f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level9 =   new Level (9,   4.4f,  3.5f,  1f,  8,   150f, 5f,  250f, 0,     0.7f, 0.9f, Mathf.Pow(dropDelaySeed, 8),  false, 0.1f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level10 =  new Level (10,  4.3f,  3.5f,  1f,  8,   150f, 5f,  250f, 0,     0.7f, 1.0f, Mathf.Pow(dropDelaySeed, 9),  true,  0.1f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);

		Level level11 =  new Level (11,  4.2f,  3.5f,  1f,  8,  160f, 5f,  270f, 0.01f, 0.7f, 1.1f, Mathf.Pow(dropDelaySeed, 10), true,  0.2f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level12 =  new Level (12,  4.0f,  3.5f,  2f,  8,  160f, 5f,  270f, 0.02f, 0.7f, 1.1f, Mathf.Pow(dropDelaySeed, 11), false, 0.2f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level13 =  new Level (13,  3.9f,  3.5f,  2f,  8,  160f, 5f,  270f, 0.02f, 0.7f, 1.2f, Mathf.Pow(dropDelaySeed, 12), true,  0.2f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level14 =  new Level (14,  3.8f,  3.5f,  2f,  8,  160f, 5f,  270f, 0.02f, 0.7f, 1.2f, Mathf.Pow(dropDelaySeed, 13), false, 0.2f,   0.1f,   0.1f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level15 =  new Level (15,  4.7f,  4.4f,  2f,  15,  160f, 5f,  270f, 0.15f,  0.7f, 1.3f, Mathf.Pow(dropDelaySeed, 20), true,  0.2f,   0.1f,   0.1f,  0,        0.25f,      0f,             0f,       0.01f);
		Level level16 =  new Level (16,  3.6f,  3.3f,  2f,  8,  160f, 5f,  270f, 0.01f,  0.7f, 1.3f, Mathf.Pow(dropDelaySeed, 15), false, 0.2f,   0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level17 =  new Level (17,  3.5f,  3.0f,  2f,  8,  160f, 5f,  270f, 0.01f,  0.7f, 1.4f, Mathf.Pow(dropDelaySeed, 15), false, 0.2f,   0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level18 =  new Level (18,  3f,    3f,    2f,  8,  160f, 5f,  270f, 0.01f,  0.7f, 1.5f, Mathf.Pow(dropDelaySeed, 15), true,  0.2f,   0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level19 =  new Level (19,  3f,    3f,    2f,  8,  160f, 5f,  270f, 0.01f,  0.7f, 2.0f, Mathf.Pow(dropDelaySeed, 15), true,  0.2f,   0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level20 =  new Level (20,  3f,    3f,    2f,  6,  160f, 5f,  270f, 0.2f,  0.7f, 2.0f, Mathf.Pow(dropDelaySeed, 8), false, 0.0f,   0.0f,   0.0f,  0.00f,    0.00f,     0.00f,          0.00f,     0.07f);

		Level level21 =  new Level (21,  3f,    3f,    1f,  9,  170f, 10f, 280f, 0.02f,  0.7f, 2.0f, Mathf.Pow(dropDelaySeed, 15), false, 0.25f,  0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level22 =  new Level (22,  3f,    3f,    1f,  9,  170f, 10f, 280f, 0.02f,  0.9f, 2.0f, Mathf.Pow(dropDelaySeed, 15), true,  0.25f,  0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level23 =  new Level (23,  3f,    3f,    1f,  9,  170f, 10f, 280f, 0.02f,  0.9f, 2.0f, Mathf.Pow(dropDelaySeed, 15), false, 0.25f,  0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level24 =  new Level (24,  3f,    3f,    1f,  10,  170f, 10f, 280f, 0.03f,  0.9f, 2.5f, Mathf.Pow(dropDelaySeed, 15), false, 0.25f,  0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level25 =  new Level (25,  5f,    4f,    1f,  10,  170f, 10f, 280f, 0.03f,  0.9f, 15f, Mathf.Pow(dropDelaySeed, 15), true,  0.0f,  0.0f,   0.0f,  0.00f,    0.15f,     0.0f,          0.00f,     0.01f);
		Level level26 =  new Level (26,  3f,    2.9f,  1f,  10,  170f, 10f, 280f, 0.03f,  0.9f, 2.5f, Mathf.Pow(dropDelaySeed, 15), true,  0.25f,  0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level27 =  new Level (27,  3f,    2.9f,  1f,  10,  170f, 10f, 280f, 0.03f,  0.9f, 2.5f, Mathf.Pow(dropDelaySeed, 15), true,  0.25f,  0.1f,   0.2f,  0.05f,    0.02f,     0.05f,          0.05f,     0.01f);
		Level level28 =  new Level (28,  3f,    2.9f,  1f,  10,  170f, 10f, 280f, 0.03f,  0.9f, 2.5f, Mathf.Pow(dropDelaySeed, 15), true,  0.25f,  0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level29 =  new Level (29,  3f,    2.9f,  1f,  10,  170f, 10f, 280f, 0.03f,  0.9f, 2.5f, Mathf.Pow(dropDelaySeed, 15), false, 0.25f,  0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level30 =  new Level (30,  3f,    2.9f,  1f,  10,  200f, 15f, 300f, 0.03f,  1.2f, 2.5f, Mathf.Pow(dropDelaySeed, 15), true,  0.25f,  0.1f,   0.2f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);

		Level level31 =  new Level (31,  3f,    2.9f,  1f,  10,  180f, 15f, 290f, 0.03f,  0.9f, 3.0f, Mathf.Pow(dropDelaySeed, 15), false, 0.3f,   0.1f,   0.25f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level32 =  new Level (32,  3f,    2.9f,  1f,  10,  180f, 15f, 290f, 0.03f,  0.9f, 3.0f, Mathf.Pow(dropDelaySeed, 15), true,  0.3f,   0.1f,   0.25f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level33 =  new Level (33,  3f,    2.9f,  1f,  13,  180f, 15f, 290f, 0.03f,  0.9f, 3.0f, Mathf.Pow(dropDelaySeed, 15), false, 0.3f,   0.1f,   0.25f,  0.05f,    0.02f,     0.05f,          0.05f,     0.02f);
		Level level34 =  new Level (34,  3f,    2.9f,  1f,  13,  180f, 15f, 290f, 0.03f,  0.9f, 3.0f, Mathf.Pow(dropDelaySeed, 10), true,  0.3f,   0.1f,   0.25f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level35 =  new Level (35,  3.5f,    2.8f,  1f,  5,  100f, 15f, 160f, 0.2f,  1.4f, 13.0f, Mathf.Pow(dropDelaySeed, 15), false, 0.3f,  0.1f,   0.25f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level36 =  new Level (36,  3f,    2.8f,  1f,  13,  180f, 15f, 290f, 0.03f,  1.0f, 3.0f, Mathf.Pow(dropDelaySeed, 15), true,  0.3f,  0.1f,   0.25f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level37 =  new Level (37,  3f,    2.7f,  1f,  13,  180f, 15f, 290f, 0.03f,  1.0f, 3.5f, Mathf.Pow(dropDelaySeed, 15), false, 0.3f,  0.1f,   0.25f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level38 =  new Level (38,  3f,    2.6f,  1f,  13,  180f, 15f, 290f, 0.03f,  1.0f, 3.5f, Mathf.Pow(dropDelaySeed, 15), true,  0.3f,  0.1f,   0.25f,  0.05f,    0.02f,     0.05f,          0.05f,     0.02f);
		Level level39 =  new Level (39,  3f,    2.5f,  1f,  13,  180f, 15f, 290f, 0.03f,  1.0f, 3.5f, Mathf.Pow(dropDelaySeed, 15), false, 0.3f,  0.1f,   0.25f,  0.05f,    0.01f,     0.05f,          0.05f,     0.02f);
		Level level40 =  new Level (40,  3.9f,  3.5f,  1f,  12,  180f, 15f, 290f, 0.0f,   1.0f, 3.5f, Mathf.Pow(dropDelaySeed, 20), true,  0.0f,  0.0f,   0.0f,   0.2f,     0f,        0.05f,             0f,     0.1f);

		Level level41 =  new Level (41,  2.8f,  2.5f,  1f,  13,  200f, 15f, 290f, 0.03f,  1.0f, 4.0f, Mathf.Pow(dropDelaySeed, 16), true,  0.3f,  0.1f,   0.30f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level42 =  new Level (42,  2.8f,  2.5f,  1f,  13,  200f, 15f, 290f, 0.03f,  1.0f, 4.0f, Mathf.Pow(dropDelaySeed, 16), false, 0.3f,  0.1f,   0.30f,  0.05f,    0.05f,     0.05f,          0.05f,     0.01f);
		Level level43 =  new Level (43,  2.8f,  2.5f,  1f,  13,  200f, 15f, 290f, 0.03f,  1.1f, 4.0f, Mathf.Pow(dropDelaySeed, 16), true,  0.3f,  0.1f,   0.30f,  0.03f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level44 =  new Level (44,  2.8f,  2.5f,  1f,  13,  200f, 15f, 290f, 0.03f,  1.1f, 4.0f, Mathf.Pow(dropDelaySeed, 16), true,  0.3f,  0.1f,   0.30f,  0.03f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level45 =  new Level (45,  3f,    2.9f,  1f,  10,  200f, 15f, 300f, 0.03f,  1.2f, 15.0f, Mathf.Pow(dropDelaySeed, 15), true, 0.0f,  0.0f,   0.0f,  0.00f,    0.00f,     0.00f,          0.0f,     0.02f);
		Level level46 =  new Level (46,  2.8f,  2.5f,  1f,  13,  200f, 15f, 300f, 0.03f,  1.1f, 4.0f, Mathf.Pow(dropDelaySeed, 16), false, 0.3f,  0.1f,   0.30f,  0.03f,    0.02f,     0.05f,          0.05f,     0.01f);
		Level level47 =  new Level (47,  2.8f,  2.5f,  1f,  12,  200f, 15f, 300f, 0.03f,  1.1f, 4.5f, Mathf.Pow(dropDelaySeed, 16), false, 0.3f,  0.1f,   0.30f,  0.03f,    0.02f,     0.05f,          0.05f,     0.02f);
		Level level48 =  new Level (48,  2.8f,  2.5f,  1f,  12,  200f, 15f, 300f, 0.03f,  1.1f, 4.5f, Mathf.Pow(dropDelaySeed, 16), true,  0.3f,  0.1f,   0.30f,  0.03f,    0.02f,     0.05f,          0.05f,     0.02f);
		Level level49 =  new Level (49,  2.8f,  2.5f,  1f,  12,  200f, 15f, 300f, 0.03f,  1.1f, 4.5f, Mathf.Pow(dropDelaySeed, 16), true,  0.3f,  0.1f,   0.30f,  0.03f,    0.02f,     0.05f,          0.05f,     0.01f);
		Level level50 =  new Level (50,  3.8f,  3.5f,  1f,  13,  200f, 15f, 300f,    0f,  1.1f, 4.5f, Mathf.Pow(dropDelaySeed, 25), false, 0.0f,  0.0f,   0.35f,  0.03f,    0.05f,     0.35f,          0.05f,     0.01f);

		Level level51 =  new Level (51,  3.2f,  2.5f,  2f,  10,  100f, 15f, 130f, 0.2f,  0.5f, 0.0f, Mathf.Pow(dropDelaySeed, 5), true,  0.0f,  0.0f,   0.0f,  0.05f,    0.00f,     0.00f,          0.00f,     0.02f);
		Level level52 =  new Level (52,  2.8f,  2.5f,  2f,  13,  200f, 15f, 300f, 0.04f,  1.2f, 5.0f, Mathf.Pow(dropDelaySeed, 17), true,  0.35f,  0.1f,   0.35f,  0.03f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level53 =  new Level (53,  2.8f,  2.5f,  2f,  13,  200f, 15f, 300f, 0.04f,  1.2f, 5.0f, Mathf.Pow(dropDelaySeed, 17), false, 0.35f,  0.1f,   0.35f,  0.03f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level54 =  new Level (54,  2.8f,  2.5f,  2f,  13,  200f, 15f, 300f, 0.04f,  1.2f, 5.0f, Mathf.Pow(dropDelaySeed, 17), true,  0.35f,  0.1f,   0.35f,  0.03f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level55 =  new Level (55,  3.1f,  3.0f,  3f,  10,  200f, 15f, 300f, 0.3f,   1.2f, 5.0f, Mathf.Pow(dropDelaySeed, 12), false, 0.1f,   0.1f,   0.1f,   0.00f,    0.0f,      0.0f,           0.0f,      0.02f);
		Level level56 =  new Level (56,  2.8f,  2.5f,  3f,  13,  200f, 15f, 310f, 0.04f,  1.2f, 5.6f, Mathf.Pow(dropDelaySeed, 18), true,  0.35f,  0.1f,   0.35f,  0.05f,    0.05f,     0.05f,          0.05f,     0.02f);
		Level level57 =  new Level (57,  2.7f,  2.5f,  3f,  13,  200f, 15f, 310f, 0.05f,  1.3f, 5.7f, Mathf.Pow(dropDelaySeed, 18), false, 0.35f,  0.1f,   0.35f,  0.05f,    0.05f,     0.05f,          0.05f,     0.03f);
		Level level58 =  new Level (58,  2.7f,  2.5f,  3f,  13,  200f, 20f, 320f, 0.05f,  1.3f, 5.8f, Mathf.Pow(dropDelaySeed, 18), true,  0.35f,  0.1f,   0.35f,  0.03f,    0.05f,     0.05f,          0.05f,     0.03f);
		Level level59 =  new Level (59,  2.7f,  2.4f,  4f,  13,  200f, 20f, 320f, 0.05f,  1.3f, 5.9f, Mathf.Pow(dropDelaySeed, 18.5f), true,  0.35f,  0.1f,   0.35f,  0.03f,    0.05f,     0.05f,          0.05f,     0.03f);
		Level level60 =  new Level (60,  2.7f,  2.4f,  4f,  10,  200f, 20f, 320f, 0.00f,  1.3f, 20.0f, Mathf.Pow(dropDelaySeed, 12), false, 0.4f,  0.0f,   0.0f,  0.00f,    0.00f,     0.00f,          0.2f,     0.03f);

		Level level61 =  new Level (61,  2.7f,  2.4f,  4f,  13,  230f, 25f, 330f, 0.06f,  1.3f, 6.1f, Mathf.Pow(dropDelaySeed, 19), true,  0.35f,  0.1f,   0.35f,  0.05f,    0.05f,     0.05f,          0.05f,     0.03f);
		Level level62 =  new Level (62,  2.5f,  2.2f,  4f,  13,  230f, 25f, 340f, 0.06f,  1.4f, 6.2f,  Mathf.Pow(dropDelaySeed, 19), false, 0.35f,  0.1f,   0.35f,  0.05f,    0.05f,     0.05f,          0.05f,     0.03f);

		this.levels.Add (level1);
		this.levels.Add (level2);
		this.levels.Add (level3);
		this.levels.Add (level4);
		this.levels.Add (level5);
		this.levels.Add (level6);
		this.levels.Add (level7);
		this.levels.Add (level8);
		this.levels.Add (level9);
		this.levels.Add (level10);

		this.levels.Add (level11);
		this.levels.Add (level12);
		this.levels.Add (level13);
		this.levels.Add (level14);
		this.levels.Add (level15);
		this.levels.Add (level16);
		this.levels.Add (level17);
		this.levels.Add (level18);
		this.levels.Add (level19);
		this.levels.Add (level20);

		this.levels.Add (level21);
		this.levels.Add (level22);
		this.levels.Add (level23);
		this.levels.Add (level24);
		this.levels.Add (level25);
		this.levels.Add (level26);
		this.levels.Add (level27);
		this.levels.Add (level28);
		this.levels.Add (level29);
		this.levels.Add (level30);

		this.levels.Add (level31);
		this.levels.Add (level32);
		this.levels.Add (level33);
		this.levels.Add (level34);
		this.levels.Add (level35);
		this.levels.Add (level36);
		this.levels.Add (level37);
		this.levels.Add (level38);
		this.levels.Add (level39);
		this.levels.Add (level40);

		this.levels.Add (level41);
		this.levels.Add (level42);
		this.levels.Add (level43);
		this.levels.Add (level44);
		this.levels.Add (level45);
		this.levels.Add (level46);
		this.levels.Add (level47);
		this.levels.Add (level48);
		this.levels.Add (level49);
		this.levels.Add (level50);

		this.levels.Add (level51);
		this.levels.Add (level52);
		this.levels.Add (level53);
		this.levels.Add (level54);
		this.levels.Add (level55);
		this.levels.Add (level56);
		this.levels.Add (level57);
		this.levels.Add (level58);
		this.levels.Add (level59);
		this.levels.Add (level60);

		this.levels.Add (level61);
		this.levels.Add (level62);
	}
}
